using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 分析代码，用于补充描述
    /// </summary>
    [DataContract]
    public class ACodeXml
    {
        [DataMember]
        public string ACodeID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        [DataMember] 
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
    }
}
