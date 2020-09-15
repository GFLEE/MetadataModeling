using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 列描述对象
    /// </summary>
    [DataContract]
    public class ColumnXml
    {
        [DataMember]
        public string ColumnID { get; set; }

        /// <summary>
        /// 所属表ID
        /// </summary>
        [DataMember]
        public string TableID { get; set; }
        /// <summary>
        /// 列描述
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        [DataMember]
        public bool IsPK { get; set; }

        /// <summary>
        /// 引用外键列的ID
        /// </summary>
        [DataMember]
        public string RefFkColumnID { get; set; }



    }
}
