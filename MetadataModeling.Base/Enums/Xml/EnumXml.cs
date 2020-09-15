using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Enums.Xml
{
    /// <summary>
    /// Enum 项描述
    /// </summary>
    [DataContract]
    public class EnumXml
    {
        /// <summary>
        /// Code
        /// </summary>
        [DataMember]
        public string Code { get; set; } 
        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public string Description { get; set; } 
        /// <summary>
        /// 枚举项
        /// </summary>
        [DataMember]
        public List<EnumItemXml> Items { get; set; } 
    }
}
