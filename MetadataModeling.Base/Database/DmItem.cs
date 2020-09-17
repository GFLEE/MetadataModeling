using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    /// <summary>
    /// Dm基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DmItem<T>
    {
        /// <summary>
        /// Xml存储对象
        /// </summary>
        public T XmlItem { get; protected set; }
        public DmContext Context { get; protected set; }

        public abstract string ID { get; }
        public abstract string Name { get; set; }


        public virtual T XmlItemDeepClone()
        {
            var bys = SerializeUtility.SerializeToArray(XmlItem);
            var result = SerializeUtility.Deserialize(bys, typeof(T));
            return (T)result;
        }

        /// <summary>
        /// 获取当前对象克隆字符串
        /// </summary>
        /// <returns></returns>
        public string GetDeepCloneString()
        {
            var bys = SerializeUtility.SerializeToArray(XmlItem);
            return Convert.ToBase64String(bys);

        }

    }
}
