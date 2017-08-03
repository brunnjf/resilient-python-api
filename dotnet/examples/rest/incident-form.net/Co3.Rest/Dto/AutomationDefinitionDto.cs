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
    ///  NOTE:  The initial implementation of this was as an abstract class with an AutomationDefinitionSetFieldDTO subclass
    ///  like this:
    /// 
    ///  @XmlRootElement
    ///  @JsonTypeInfo(use = JsonTypeInfo.Id.NAME, include = JsonTypeInfo.As.EXISTING_PROPERTY, property = "type")
    ///  @JsonSubTypes({
    ///  @JsonSubTypes.Type(value = AutomationSetFieldDTO.class, name = AutomationDefinitionType.SET_FIELD_STRING),
    ///  @JsonSubTypes.Type(value = AutomationCreateTaskDTO.class, name = AutomationDefinitionType.CREATE_TASK_STRING)
    ///  })
    /// 
    ///  This requires an upgrade to at least Jackson 2.5.0 where EXISTING_PROPERTY is added. That was doable.
    /// 
    ///  However the current version of enunciate 2.5 that we are using did not generate code that brought over
    ///  the @JsonSubTypes annotation information.  This means that clients using the generated code could not
    ///  deserialize the polymorphic classes properly.  It would have been possible to customize our enunciated custom
    ///  code generator.  However after talking with Allen R. we are not going to do that right now.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AutomationDefinitionDto 
    {
        /// <summary>
        /// The type of the action automation. See AutomationDefinitionType.
        /// </summary>
        [JsonProperty("type")]
        public AutomationDefinitionType Type { get; set; }

        /// <summary>
        /// The object handle for the type of the field to modify.  This is only applicable when AutomationDefinitionType is
        /// MODIFY_FIELD.
        /// </summary>
        [JsonProperty("type_id")]
        public ObjectHandle TypeId { get; set; }

        /// <summary>
        /// The object handle for the field to modify.  This is only applicable when AutomationDefinitionType is
        /// MODIFY_FIELD.
        /// </summary>
        [JsonProperty("field")]
        public ObjectHandle Field { get; set; }

        /// <summary>
        /// The operation to perform on the field.  This is only applicable when AutomationDefinitionType is MODIFY_FIELD.
        /// </summary>
        [JsonProperty("operation")]
        public FieldValModOperation ModificationOperation { get; set; }

        /// <summary>
        /// The tasks to be set.  The tasks are set by the order in the list. This is only valid
        /// when type is AutomationDefinitionType.CREATE_TASK
        /// </summary>
        [JsonProperty("tasks_to_create")]
        public List<ObjectHandle> TasksToCreate { get; set; }

        [JsonProperty("scripts_to_run")]
        public ObjectHandle ScriptToRun { get; set; }

        /// <summary>
        /// The value used when performing an operation on a field.  This is only applicable when AutomationDefinitionType
        /// is MODIFY_FIELD.
        /// </summary>
        [JsonProperty("value")]
        public object FieldValue { get; set; }
    }
}
