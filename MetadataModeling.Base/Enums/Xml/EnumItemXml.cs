using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Enums.Xml
{
    /// <summary>
    /// 枚举Item
    /// </summary>
    [DataContract]
    public class EnumItemXml
    {
        /// <summary>
        /// 编码/英文名
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; } 
    }
}
