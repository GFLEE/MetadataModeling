using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 默认列扩展属性
    /// </summary>
    [DataContract]
    public class DefaultColumnExXml
    {
        /// <summary>
        /// 表必备列
        /// </summary>
        [DataMember]
        public List<string> TableRequiredColumns { get; set; }

        /// <summary>
        /// 表建议列
        /// </summary>
        [DataMember]
        public List<string> TableSuggestedColumns { get; set; }

    }
}
