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

        /// <summary>
        /// 长度(字符类型有效)
        /// </summary>
        [DataMember]
        public int Length { get; set; }

        /// <summary>
        /// 允许Null
        /// </summary>
        [DataMember]
        public bool IsNull { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        [DataMember]
        public bool IsRequired { get; set; }


        /// <summary>
        /// 精度(decimal)
        /// </summary>
        [DataMember]
        public int Prec { get; set; }

        /// <summary>
        /// 小数位数(decimal)
        /// </summary>
        [DataMember]
        public int Scale { get; set; }

        /// <summary>
        /// 自增标识(Int)
        /// </summary>
        [DataMember]
        public bool IsIdentity{ get; set; }


        /// <summary>
        /// 顺序(主键应该永远在第一位)
        /// </summary>
        [DataMember]
        public string DisplayIndex { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 字段对应长名称（出现在编辑界面）
        /// </summary>
        [DataMember]
        public string LabelName { get; set; }

        /// <summary>
        /// 字段对应短名称（出现在列头界面）
        /// </summary>
        [DataMember]
        public string LabelShortName { get; set; }


        /// <summary>
        /// 字段补充说明
        /// </summary>
        [DataMember]
        public string LabelTooltips { get; set; }

        /// <summary>
        /// 分析代码
        /// </summary>
        [DataMember]
        public string ACodeID1 { get; set; }

        /// <summary>
        /// 如果当前列引用系统列，此处为引用列信息
        /// </summary>
        [DataMember]
        public string DefaultColumnID { get; set; }

        /// <summary>
        /// 如果字段是枚举，枚举代码
        /// </summary>
        [DataMember]
        public string EnumCode { get; set; }
         
        /// <summary>
        /// 引用其他数据库列的ID
        /// </summary>
        [DataMember]
        public string RefOtherDbFkColumnID { get; set; }
    }
}
