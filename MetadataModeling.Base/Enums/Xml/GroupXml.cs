using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Enums.Xml
{
    /// <summary>
    /// 枚举Group
    /// </summary>
    [DataContract]
    public class GroupXml
    {  /// <summary>
       /// Code
       /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 组中所有枚举
        /// </summary>
        [DataMember]
        public List<EnumXml> Items { get; set; }

    }
}
