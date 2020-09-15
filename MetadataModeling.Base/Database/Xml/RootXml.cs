using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 对根目录的描述
    /// </summary>
    [DataContract]
    public class RootXml
    {
        /// <summary>
        /// Groups 所有TableGroupXml的集合
        /// </summary>
        [DataMember]
        public List<TableGroupXml> Groups { get; set; }
        /// <summary>
        /// Tables 所有表的集合
        /// </summary>
        [DataMember]
        public List<TableXml> Tables { get; set; }
        /// <summary>
        /// Columns 所有列的集合
        /// </summary>
        [DataMember]
        public List<ColumnXml> Columns { get; set; }
        /// <summary>
        /// 分析代码1集合（列用）
        /// </summary>
        [DataMember]
        public List<ACodeXml> ACode1s { get; set; }
        /// <summary>
        /// 系统默认列集合
        /// </summary>
        [DataMember]
        public List<ColumnXml> DefaultColumns { get; set; }
        /// <summary>
        /// 系统默认扩展列定义
        /// </summary>
        [DataMember]
        public DefaultColumnExXml DefaultColumnEx { get; set; } 
        /// <summary>
        /// 枚举文件名
        /// </summary>
        [DataMember]
        public string EnumFileName { get; set; }

        /// <summary>
        /// 实体对象应该继承的接口项目
        /// </summary>
        [DataMember]
        public List<string> DevEntityInterfaceNames { get; set; }
         
    }
}
