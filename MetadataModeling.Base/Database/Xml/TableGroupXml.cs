using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 对数据库TableGroup的描述
    /// </summary>
    [DataContract]
    public class TableGroupXml
    {
        /// <summary>
        /// TableGroupXml（数据库）主键
        /// </summary>
        [DataMember]
        public string TableGroupID { get; set; }

        /// <summary>
        /// TableGroupXml（数据库）中文名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// TableGroupXml（数据库）英文名
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 顺序(主键应该永远在第一位)
        /// </summary>
        [DataMember]
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 生成的命名空间
        /// </summary>
        [DataMember]
        public string DevNamespaceKey { get; set; }
        /// <summary>
        /// Context名称
        /// </summary>
        [DataMember]
        public string DevDbContextName { get; set; }
        /// <summary>
        /// 引用其他TableGroup的集合，用于生成Data时添加命名空间
        /// </summary>
        [DataMember]
        public string[] DevRefOtherTableGroupIDs { get; set; }
        /// <summary>
        /// 实体对象应该继承项目的选项
        /// </summary>
        [DataMember]
        public List<string> DevEntityInterfaceNames { get; set; }
    }
}
