/*
 * Resilient Systems, Inc. ("Resilient") is willing to license software
 * or access to software to the company or entity that will be using or
 * accessing the software and documentation and that you represent as
 * an employee or authorized agent ("you" or "your") only on the condition
 * that you accept all of the terms of this license agreement.
 *
 * The software and documentation within Resilient's Development Kit are
 * copyrighted by and contain confidential information of Resilient. By
 * accessing and/or using this software and documentation, you agree that
 * while you may make derivative works of them, you:
 *
 * 1)  will not use the software and documentation or any derivative
 *     works for anything but your internal business purposes in
 *     conjunction your licensed used of Resilient's software, nor
 * 2)  provide or disclose the software and documentation or any
 *     derivative works to any third party.
 *
 * THIS SOFTWARE AND DOCUMENTATION IS PROVIDED "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL RESILIENT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

// <auto-generated>
// IBM Resilient REST API version 28.1
//
// Generated by <a href="http://enunciate.webcohesion.com">Enunciate</a>.
// </auto-generated>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Co3.Rest.Dto
{
    /// <summary>
    ///  <p>This type contains constant information for the server.  This information is
    ///  useful in translating names that the user sees to IDs that other REST API endpoints accept.
    ///  For example, the <a href="json_IncidentDTO.html#reslink_crimestatus_id">IncidentDTO</a> has a property called "crimestatus_id".
    ///  The valid values are stored in <a href="json_ConstDTO.html#reslink_crime_statuses">ConstDTO&apos;s</a> "crime_statuses" property.</p>
    /// 
    ///  <p>This information generally only changes for new releases of Resilient.</p>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ConstDto 
    {
        /// <summary>
        /// A map of the possible NIST attack vector values.  The key portion of the map contains the IDs and
        /// the value portion contains a <a href="json_NISTAttackVectorDTO.html">NISTAttackVectorDTO</a>.
        /// 
        /// These values come from NIST 800-61 (see <a href="http://csrc.nist.gov/publications/nistpubs/800-61rev2/SP800-61rev2.pdf">http://csrc.nist.gov/publications/nistpubs/800-61rev2/SP800-61rev2.pdf</a>).
        /// 
        /// See also:
        /// <a href="json_IncidentDTO.html#reslink_nist_attack_vectors">incidentDTO (nist_attack_vectors property)</a>
        /// </summary>
        [JsonProperty("nist_attack_vectors")]
        public Dictionary<int, NistAttackVectorDto> NistAttackVectors { get; set; }

        /// <summary>
        /// An array of the possible period type values.  These values are used when retrieving data from
        /// the <a href="resource_OrgStatisticsREST.html">OrgStatisticsREST</a> resource (see the
        /// incidents_by_type_over_time and new_and_open_incidents endpoints).
        /// </summary>
        [JsonProperty("period_types")]
        public List<string> PeriodTypes { get; set; }

        /// <summary>
        /// An array of the default wizard items and incident tabs represented by
        /// <a href="json_PredefinedViewItemDTO.html">PredefinedViewItemDTO</a> elements.
        /// </summary>
        [JsonProperty("predefined_items")]
        public List<PredefinedViewItemDto> PredefinedItems { get; set; }

        /// <summary>
        /// A map of possible task statuses.  The key portion of the map is the task status
        /// code.  The value portion is a <a href="json_TaskStatusDTO.html">taskStatusDTO</a>.
        /// See also:
        /// <a href="json_TaskDTO.html#reslink_status">taskDTO (status property)</a>
        /// </summary>
        [JsonProperty("task_statuses")]
        public Dictionary<string, TaskStatusDto> TaskStatuses { get; set; }

        /// <summary>
        /// A map of possible US state values.  The key portion of the map is the ID of the state
        /// The value portion of the map is a <a href="json_StateDTO.html">stateDTO</a>.
        /// Note that the ID for a given state is the same as it's geo ID.  See the
        /// "geos" property.
        /// </summary>
        [JsonProperty("states")]
        public Dictionary<int, StateDto> States { get; set; }

        /// <summary>
        /// A map of possible incident statuses.  The key portion of the map contains the status code and
        /// the value portion is a <a href="json_IncidentStatusDTO.html">incidentStatusDTO</a>.
        /// See also:
        /// <a href="json_PartialIncidentDTO.html#reslink_plan_status">partialIncidentDTO (plan_status property)</a>
        /// </summary>
        [JsonProperty("incident_statuses")]
        public Dictionary<string, IncidentStatusDto> IncidentStatuses { get; set; }

        /// <summary>
        /// A map of possible harm status values.  The key portion of the map contains the ID
        /// of the harm status.  The value portion contains a <a href="json_HarmStatusDTO.html">harmStatusDTO</a>
        /// See also:
        /// <a href="json_IncidentPIIDTO.html#reslink_harmstatus_id">incidentPIIDTO (harmstatus_id property)</a>
        /// </summary>
        [JsonProperty("harm_statuses")]
        public Dictionary<int, HarmStatusDto> HarmStatuses { get; set; }

        /// <summary>
        /// A map of possible crime status values.  The key portion of the map contains the ID of the
        /// crime status.  The value portion contains a <a href="json_CrimeStatusDTO.html">crimeStatusDTO</a>.
        /// See also:
        /// <a href="json_IncidentDTO.html#reslink_crimestatus_id">incidentDTO (crimestatus_id property)</a>
        /// </summary>
        [JsonProperty("crime_statuses")]
        public Dictionary<int, CrimeStatusDto> CrimeStatuses { get; set; }

        /// <summary>
        /// A map of possible regulator values.  The key portion of the map contains the ID of the
        /// regulator.  The value portion contains a <a href="json_RegulatorDTO.html">regulatorDTO</a>.
        /// See also: <a href="json_RegulatorsDTO.html">regulatorsDTO</a>.
        /// </summary>
        [JsonProperty("regulators")]
        public Dictionary<int, RegulatorDto> Regulators { get; set; }

        /// <summary>
        /// A map of possible industry values.  The key portion of the map contains the ID of the
        /// industry.  The value portion contains an <a href="json_IndustryDTO.html">industryDTO</a>.
        /// </summary>
        [JsonProperty("industries")]
        public Dictionary<int, IndustryDto> Industries { get; set; }

        /// <summary>
        /// A map of industries to regulators.  These are regulators that apply for
        /// the specified industry.  The key of this map is the industry ID and the
        /// value is the regulator ID.  See also "regulators" and "industries" properties.
        /// </summary>
        [JsonProperty("industry_regulators_map")]
        public Dictionary<int, List<int>> IndustryRegulatorsMap { get; set; }

        /// <summary>
        /// A map of possible timeframe values.  The key portion of the map contains the
        /// IDs and the value portion contains a <a href="json_TimeframeDTO.html">timeframeDTO</a>.
        /// </summary>
        [JsonProperty("timeframes")]
        public Dictionary<int, TimeframeDto> Timeframes { get; set; }

        /// <summary>
        /// A map of possible data type values.  The key portion of the map contains the data
        /// type ID and the value portion contains a <a href="json_DataTypeDTO.html">dataTypeDTO</a>.
        /// See also:  <a href="json_FullIncidentDataDTO.html#reslink_dtm">fullIncidentDataDTO (dtm property)</a>
        /// </summary>
        [JsonProperty("data_types")]
        public Dictionary<int, DataTypeDto> DataTypes { get; set; }


        /// <summary>
        /// A map of possible "geo" values.  A geo is short for geographical region.  It
        /// can be a region (such as Europe), country (such as United States) or a
        /// state/province (such as Massachusetts or Manitoba).  The ID portion of the
        /// map is the geo ID.  The value portion is a <a href="json_GeoDTO.html">geoDTO</a>.
        /// 
        /// Geo IDs are specified in the <a href="json_IncidentCountsDTO.html">incidentCountsDTO</a>
        /// to indicate how many records have been lost for a particular state/province.
        /// 
        /// Geo IDs are also used to specify the state and country properties of the
        /// <a href="json_IncidentDTO.html">incidentDTO</a>.
        /// </summary>
        [JsonProperty("geos")]
        public Dictionary<int, GeoDto> Geos { get; set; }

        /// <summary>
        /// <p>A map of possible data formats.  The following are examples of data formats:
        /// <ul>
        /// <li>Paper</li>
        /// <li>Electronic</li>
        /// <li>Verbal</li>
        /// </ul>
        /// <br/>
        /// The key portion of the map is the data format ID.  The value contains a
        /// <a href="json_DataFormatTypeDTO.html">dataFormatTypeDTO</a>.
        /// 
        /// See also:  <a href="json_IncidentPIIDTO.html#reslink_data_format">incidentPIIDTO (data_format property)</a>.
        /// </summary>
        [JsonProperty("data_formats")]
        public Dictionary<int, DataFormatTypeDto> DataFormats { get; set; }

        /// <summary>
        /// A list of possible built-in artifact types.  See <a href="json_IncidentArtifactTypeDTO.html">incidentArtifactTypeDTO</a>.
        /// 
        /// <strong>NOTE:</strong> Starting with version 28 this returns only the standard built-in system artifact types.
        /// This will be deprecated in version 29.
        /// 
        /// Using this data structure the incidentArtifactTypeDTO has only the following fields filled in:
        /// 
        /// id
        /// 
        /// name
        /// 
        /// desc
        /// 
        /// reg_exp
        /// 
        /// multi_aware
        /// 
        /// file
        /// 
        /// Instead of this use <a href="resource_OrgREST.html#resource_OrgREST_getOrg_GET">ORGRest GET</a> to get the
        /// entire list of artifact types from
        /// <a href="json_FullOrgDTO.html">fullOrgDTO.incident_artifact_types</a> to get the full
        /// list of artifacts, both system and custom.
        /// 
        /// For create, update, delete operations on artifact types use
        /// <a href="resource_OrgIncidentArtifactTypeREST.html">OrgIncidentArtifactTypeREST</a> resource.
        /// </summary>
        [JsonProperty("artifact_types")]
        [Obsolete("Deprecated in v28 and will be removed in v29")]
        public List<IncidentArtifactTypeDto> ArtifactTypes { get; set; }

        /// <summary>
        /// Information about the actions framework.
        /// </summary>
        [JsonProperty("actions_framework")]
        public ActionsFrameworkInfoDto ActionsFrameworkInfo { get; set; }

        /// <summary>
        /// The maximum allowed attachment size (in megabytes).
        /// </summary>
        [JsonProperty("max_attachment_mb")]
        public int MaxAttachmentMb { get; set; }

        /// <summary>
        /// The maximum number of allowed artifacts to be loaded at same time.
        /// </summary>
        [JsonProperty("max_artifacts")]
        public int MaxArtifacts { get; set; }

        /// <summary>
        /// A mapping of input type name to InputTypeDTO.  This map allows you to get information about input types
        /// in the system (e.g. what "methods" they support for conditions and the valid types to which a type can
        /// be transformed).
        /// </summary>
        [JsonProperty("input_types")]
        public Dictionary<string, InputTypeDto> InputTypes { get; set; }

        /// <summary>
        /// The supported action type IDs and their names.  The key portion of the map is
        /// the ID.  The value portion is the display value.
        /// </summary>
        [JsonProperty("action_types")]
        public Dictionary<int, string> ActionTypes { get; set; }

        /// <summary>
        /// The supported action object type IDs and their names.  The key portion of the map is
        /// the ID.  The value portion is the display value.
        /// </summary>
        [JsonProperty("action_object_types")]
        public Dictionary<int, string> ActionObjectTypes { get; set; }

        [JsonProperty("message_destination_types")]
        public Dictionary<int, string> DestinationTypes { get; set; }

        /// <summary>
        /// Get the constant data for pivoting services.<br/>
        /// <br/>
        /// See <a href="json_PivotConstDTO.html">PivotConstDTO</a>.
        /// </summary>
        [JsonProperty("pivot")]
        public PivotConstDto PivotConstants { get; set; }

        /// <summary>
        /// A list of the supported time units for automatic tasks and system task overrides.
        /// </summary>
        [JsonProperty("time_units")]
        public List<TimeUnitDto> TimeUnits { get; set; }

        /// <summary>
        /// The server version.
        /// </summary>
        [JsonProperty("server_version")]
        public ServerVersionDto ServerVersion { get; set; }

        /// <summary>
        /// The supported scripting languages.
        /// </summary>
        [JsonProperty("script_languages")]
        public Dictionary<string, SupportedLanguageDto> SupportedScriptLanguages { get; set; }
    }
}
