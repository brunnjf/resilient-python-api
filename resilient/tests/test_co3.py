from __future__ import print_function
import pytest
import doctest
import time
import types
import resilient


class TestCo3:
    """Basic API tests"""

    def test_doctest(self):
        """Run doctest on all modules, even if you didn't --doctest-modules"""
        for item in resilient.__dict__.values():
            if type(item) is types.ModuleType:
                doctest.testmod(item, verbose=True, raise_on_error=True)

    def test_connect_no_verify(self, co3_args):
        """ Successful connection with no Cert Verification """
        url = "https://{0}:{1}".format(co3_args.host, co3_args.port or 443)
        client = resilient.SimpleClient(org_name=co3_args.org,
                                        base_url=url,
                                        verify=False)
        assert client
        userinfo = client.connect(co3_args.email, co3_args.password)
        assert userinfo

    def test_connect_proxy(self, co3_args):
        """ Successful connection with no Cert Verification """
        url = "https://{0}:{1}".format(co3_args.host, co3_args.port or 443)
        client = resilient.SimpleClient(org_name=co3_args.org,
                                        base_url=url,
                                        verify=False,
                                        proxies=co3_args.proxy)
        assert client
        user_info = client.connect(co3_args.email, co3_args.password)
        assert user_info


class TestCo3Patch:
    """Tests for patch, create_patch, and get_patch methods"""
    def _connect(self, co3_args):
        uri = "https://{0}:{1}".format(co3_args.host, co3_args.port or 443)
        client = resilient.SimpleClient(org_name=co3_args.org,
                                        base_url=uri,
                                        verify=False)
        assert client
        user_info = client.connect(co3_args.email, co3_args.password)
        assert user_info
        return client

    def _create_incident(self, client, incident_dict):
        """create a test incident to use for patching"""
        incident = {"name": __name__,
                    "discovered_date": int(time.time() * 1000)
        }
        incident.update(incident_dict)
        inc = client.post("/incidents", incident)
        return inc

    @pytest.mark.parametrize("overwrite_conflict", (True, False))
    def test_patch_conflict(self, co3_args, overwrite_conflict):
        """ do incident patch that results in conflict """
        client = self._connect(co3_args)
        inc = self._create_incident(client, {"name": "test"})
        uri = "/incidents/%d" % inc['id']
        # Create a conflict
        inc["name"] = "the wrong value"
        inc["vers"] -= 1 # Force it to check old_value
        patch = resilient.Patch(inc)
        patch.add_value("name", "test updated")

        if overwrite_conflict:
            # If overwrite_conflict is specified then patch will return.
            response = client.patch(uri, patch, overwrite_conflict=overwrite_conflict)

            assert resilient.PatchStatus(response.json()).is_success() is overwrite_conflict
        else:
            # Not overwriting conflict, so an exception will be thrown.
            with pytest.raises(resilient.PatchConflictException) as exception_info:
                client.patch(uri, patch, overwrite_conflict=overwrite_conflict)

            # Gather the patch_status value from the exception for additional verification.
            patch_status = exception_info.value.patch_status

            fail_msg = "could not be applied due to a conflicting edit by another user.  The following field(s) were in conflict:  name."
            assert fail_msg in patch_status.get_message()

            assert patch_status.get_conflict_fields() == ["name"]
            assert patch_status.get_your_original_value("name") == "the wrong value"
            assert patch_status.get_actual_current_value("name") == "test"

        inc = client.get("/incidents/%d" % inc['id'])

        if overwrite_conflict:
            assert inc['name'] == "test updated"
        else:
            assert inc['name'] == "test"

    def test_patch_no_conflict(self, co3_args):
        """ do incident_patch with no conflict """
        client = self._connect(co3_args)
        inc = self._create_incident(client, {"name": "test"})
        uri = "/incidents/%d" % inc['id']
        patch = resilient.Patch(inc)
        patch.add_value("name", "test updated")
        response = client.patch(uri, patch, overwrite_conflict=False)
        assert resilient.PatchStatus(response.json()).is_success()
        inc = client.get("/incidents/%d" % inc['id'])
        assert inc['name'] == "test updated"

    def test_patch_null_old_value(self, co3_args):
        client = self._connect(co3_args)

        inc = self._create_incident(client, {"name": "test", "description": None})

        patch = resilient.Patch(inc)

        patch.add_value("description", "new value")

        uri = "/incidents/%d" % inc['id']

        response = client.patch(uri, patch)

        inc = client.get("/incidents/%d" % inc['id'])

        assert inc["description"] == "new value"

    def test_patch_invalid_callback(self, co3_args):
        """
        If a callback returns True but didn't modify the passed in patch in any way, that'd be a problem.
        So make sure we throw an exception in that case.
        """
        client = self._connect(co3_args)

        inc = self._create_incident(client, {"name": "test"})

        uri = "/incidents/%d" % inc['id']

        # Create a conflict
        inc["name"] = "the wrong value"
        inc["vers"] -= 1 # Force it to check old_value

        patch = resilient.Patch(inc)

        patch.add_value("name", "test updated")

        def mycb(response, patch_status, patch):
            # Return True but don't modify the patch.
            return True

        with pytest.raises(ValueError) as exception_info:
            client.patch_with_callback(uri, patch, mycb)

        assert "invoked callback did not change the patch object, but returned True" in str(exception_info.value)

    def test_no_change(self, co3_args):
        client = self._connect(co3_args)

        inc = self._create_incident(client, {"name": "test"})

        uri = "/incidents/%d" % inc['id']

        # Create a conflict
        inc["name"] = "the wrong value"
        inc["vers"] -= 1 # Force it to check old_value

        patch = resilient.Patch(inc)

        patch.add_value("name", "test updated")

        def mycb(response, patch_status, patch):
            raise resilient.NoChange

        response = client.patch_with_callback(uri, patch, mycb)

        assert response
        assert response.status_code == 200

        patch_status = resilient.PatchStatus(response.json())

        assert not patch_status.is_success()
        assert patch_status.get_conflict_fields() == ["name"]

    def test_conflict_with_handler(self, co3_args):
        client = self._connect(co3_args)

        inc = self._create_incident(client, {"name": "test"})

        uri = "/incidents/%d" % inc['id']

        # Create a conflict
        inc["name"] = "the wrong value"
        inc["vers"] -= 1 # Force it to check old_value

        patch = resilient.Patch(inc)

        patch.add_value("name", "test updated")

        def mycb(response, patch_status, patch):
            patch.exchange_conflicting_value(patch_status, "name", "test updated take 2")

        response = client.patch_with_callback(uri, patch, mycb)

        assert response
        assert response.status_code == 200

        assert "test updated take 2" == client.get(uri)["name"]
