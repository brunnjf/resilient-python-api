#!/usr/bin/env python
# -*- coding: utf-8 -*-
# (c) Copyright IBM Corp. 2010, 2020. All Rights Reserved.

""" Implementation of `resilient-sdk dev` """

import logging
import os
import re
from resilient import ensure_unicode
from resilient_sdk.cmds.base_cmd import BaseCmd
from resilient_sdk.util.sdk_exception import SDKException
from resilient_sdk.util.resilient_objects import ResilientObjMap
from resilient_sdk.util import package_file_helpers as package_helpers
from resilient_sdk.util import sdk_helpers
from resilient_sdk.cmds.codegen import CmdCodegen

# Get the same logger object that is used in app.py
LOG = logging.getLogger("resilient_sdk_log")


class CmdDev(BaseCmd):
    """TODO Docstring"""

    CMD_NAME = "dev"
    CMD_HELP = "Unsupported functionality used to help develop an app"
    CMD_USAGE = """
    $ resilient-sdk dev --set-version 36.0.0"""
    CMD_DESCRIPTION = "WARNING: Use the functionality of 'dev' at your own risk"

    def setup(self):

        # Set SDKException command_ran
        SDKException.command_ran = self.CMD_NAME

        # Define dev usage and description
        self.parser.usage = self.CMD_USAGE
        self.parser.description = self.CMD_DESCRIPTION

        # Add any positional or optional arguments here
        self.parser.add_argument("-p", "--package",
                                 type=ensure_unicode,
                                 required=True,
                                 help="(required) Name of new or path to existing package")

        self.parser.add_argument("--set-version",
                                 type=ensure_unicode,
                                 help="Set minimum Resilient Platform version")

    def execute_command(self, args):
        LOG.debug("called: CmdDev.execute_command()")

        if args.set_version:
            if not args.package:
                raise SDKException("'-p' must be specified when using '--set-version'")

            SDKException.command_ran = "{0} {1}".format(self.CMD_NAME, "--set-version")
            self._set_version(args)

        else:
            self.parser.print_help()

    @staticmethod
    def _set_version(args):

        new_version = args.set_version

        if not sdk_helpers.is_valid_version_syntax(new_version):
            raise SDKException("{0} is not a valid version".format(new_version))

        new_version_int = list(map(int, (re.findall(r"\d+", new_version))))

        # Get absolute path_to_src
        path_to_src = os.path.abspath(args.package)

        # Get path to setup.py file
        path_setup_py_file = os.path.join(path_to_src, package_helpers.BASE_NAME_SETUP_PY)

        # Parse the setup.py file
        setup_py_attributes = package_helpers.parse_setup_py(path_setup_py_file, package_helpers.SUPPORTED_SETUP_PY_ATTRIBUTE_NAMES)

        package_name = setup_py_attributes.get("name", "")

        LOG.info("Setting Resilient Platform version for %s to %s", package_name, new_version)

        # Get the customize file location.
        path_customize_py = package_helpers.get_configuration_py_file_path("customize", setup_py_attributes)

        # Get customize.py ImportDefinition
        customize_py_import_definition = package_helpers.get_import_definition_from_customize_py(path_customize_py)

        old_version = customize_py_import_definition["server_version"]["version"]

        LOG.info("Old Version: %s", old_version)
        LOG.info("New Version: %s", new_version)

        # Set the new version
        customize_py_import_definition["server_version"]["version"] = new_version
        customize_py_import_definition["server_version"]["major"] = new_version_int[0]
        customize_py_import_definition["server_version"]["minor"] = new_version_int[1]
        customize_py_import_definition["server_version"]["build_number"] = new_version_int[2]

        LOG.info("Loading old customize.py file")

        # Load the customize.py module
        customize_py_module = package_helpers.load_customize_py_module(path_customize_py, warn=False)

        # Get the 'old_params' from customize.py
        old_params = customize_py_module.codegen_reload_data()

        # Rename the old customize.py with .bak
        path_customize_py_bak = sdk_helpers.rename_to_bak_file(path_customize_py)
        try:

            # Map command line arg name to dict key returned by codegen_reload_data() in customize.py
            mapping_tuples = [
                ("messagedestination", "message_destinations"),
                ("function", "functions"),
                ("workflow", "workflows"),
                ("rule", "actions"),
                ("field", "incident_fields"),
                ("artifacttype", "incident_artifact_types"),
                ("datatable", "datatables"),
                ("task", "automatic_tasks"),
                ("script", "scripts")
            ]

            # Merge old_params with new params specified on command line
            args = CmdCodegen.merge_codegen_params(old_params, args, mapping_tuples)

            jinja_data = sdk_helpers.get_from_export(customize_py_import_definition,
                                                     message_destinations=args.messagedestination,
                                                     functions=args.function,
                                                     workflows=args.workflow,
                                                     rules=args.rule,
                                                     fields=args.field,
                                                     artifact_types=args.artifacttype,
                                                     datatables=args.datatable,
                                                     tasks=args.task,
                                                     scripts=args.script)

            jinja_data["export_data"] = sdk_helpers.minify_export(customize_py_import_definition,
                                                                  message_destinations=sdk_helpers.get_object_api_names(ResilientObjMap.MESSAGE_DESTINATIONS, jinja_data.get("message_destinations")),
                                                                  functions=sdk_helpers.get_object_api_names(ResilientObjMap.FUNCTIONS, jinja_data.get("functions")),
                                                                  workflows=sdk_helpers.get_object_api_names(ResilientObjMap.WORKFLOWS, jinja_data.get("workflows")),
                                                                  rules=sdk_helpers.get_object_api_names(ResilientObjMap.RULES, jinja_data.get("rules")),
                                                                  fields=jinja_data.get("all_fields"),
                                                                  artifact_types=sdk_helpers.get_object_api_names(ResilientObjMap.INCIDENT_ARTIFACT_TYPES, jinja_data.get("artifact_types")),
                                                                  datatables=sdk_helpers.get_object_api_names(ResilientObjMap.DATATABLES, jinja_data.get("datatables")),
                                                                  tasks=sdk_helpers.get_object_api_names(ResilientObjMap.TASKS, jinja_data.get("tasks")),
                                                                  phases=sdk_helpers.get_object_api_names(ResilientObjMap.PHASES, jinja_data.get("phases")),
                                                                  scripts=sdk_helpers.get_object_api_names(ResilientObjMap.SCRIPTS, jinja_data.get("scripts")))

            # Add package_name to jinja_data
            jinja_data["package_name"] = package_name

            # Add version
            jinja_data["version"] = setup_py_attributes.get("version", "1.0.0")

            # Instansiate Jinja2 Environment with path to Jinja2 templates
            jinja_env = sdk_helpers.setup_jinja_env("data/codegen/templates/package_template/package/util")
            jinja_template = jinja_env.get_template("customize.py.jinja2")

            LOG.info("Writing new customize.py file")

            # Render & write jinja2 template
            jinja_rendered_text = jinja_template.render(jinja_data)
            sdk_helpers.write_file(path_customize_py, jinja_rendered_text)

            LOG.info("'dev --set-version' complete for '%s'", package_name)

        except Exception as err:
            LOG.error(u"Error running resilient-sdk dev --set-version\n\nERROR:%s", err)

        # This is required in finally block as user may kill using keyboard interrupt
        finally:
            # If an error occurred, customize.py does not exist, rename the backup file to original
            if not os.path.isfile(path_customize_py):
                LOG.info(u"An error occurred. Renaming customize.py.bak to customize.py")
                sdk_helpers.rename_file(path_customize_py_bak, "customize.py")

