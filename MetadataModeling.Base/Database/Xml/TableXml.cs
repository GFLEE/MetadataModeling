using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MetadataModeling.Base.Database.Xml
{
    /// <summary>
    /// 对表Table的描述
    /// </summary>
    [DataContract]
    public class TableXml
    {
        /// <summary>
        /// 表的主键
        /// </summary>
        [DataMember]
        public string TableID { get; set; }

        /// <summary>
        /// 使用TableGroupXml的主键作为Table的外键
        /// </summary>
        [DataMember]
        public string TableGroupID { get; set; }

        /// <summary>
        /// 表中文名
        /// </summary>
        [DataMember]
        public string Name { get; set; }


        /// <summary>
        /// 表英文名
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
        /// 自动生成主键
        /// </summary>
        [DataMember]
        public bool IsAutoPk { get; set; }

        /// <summary>
        /// 需要自动生成服务类
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public bool DevIsCreateService { get; set; }

        /// <summary>
        /// 实体对象应该继承的接口
        /// </summary>
        [DataMember]
        public List<string> DevEntityInterfaceNames { get; set; }

    }
}
