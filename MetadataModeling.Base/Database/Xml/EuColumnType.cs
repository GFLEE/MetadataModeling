using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Base.Database.Xml
{
    public enum EuColumnType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,
        /// <summary>
        /// 字符串
        /// </summary>
        NVarchar = 1,
        /// <summary>
        /// 长字符串
        /// </summary>
        NVarcharMax = 2,
        /// <summary>
        /// 整型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal = 4,
        /// <summary>
        /// 二进制
        /// </summary>
        Varbinary = 5,
        /// <summary>
        /// 时间
        /// </summary>
        DateTime = 6,
        /// <summary>
        /// 时间戳
        /// </summary>
        Timestamp = 7,
        /// <summary>
        /// Long整型
        /// </summary>
        Long = 8,
        /// <summary>
        /// 双精度
        /// </summary>
        Double = 9,
        /// <summary>
        /// Varchar
        /// </summary>
        Varchar = 10


    }
}
