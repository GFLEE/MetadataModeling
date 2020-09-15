using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Enums.Xml
{
    /// <summary>
    /// 对枚举根目录的描述
    /// </summary>
    [DataContract]
    public class RootXml
    {
        [DataMember]
        public List<GroupXml> Items { get; set; }
        
    }
}
