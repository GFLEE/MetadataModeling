using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    public class DmACode : DmItem<ACodeXml>
    {
        public DmACode(DmContext context, ACodeXml xmlItem)
        {
            this.XmlItem = xmlItem;
            this.Context = context;
        }

        public override string ID
        {
            get
            {
                return XmlItem.ACodeID;
            }
        }

        public override string Name
        {
            get
            {
                return XmlItem.Name;
            }
            set
            {
                XmlItem.Name = value;
            }
        }

        /// <summary>
        /// 检查约束
        /// </summary>
        /// <returns></returns>
        public string CheckConstraint()
        {
            var columns = GetQuotedColumns();
            string name = "";
            if (columns.Count.Equals(0))
            {
                return "";
            }
            columns.ForEach(c =>
            {
                name += c.XmlItem.Name;
            });
            return string.Format("被列“{0}”引用，不可删除", name);

        }

        /// <summary>
        /// 获取引用的列集合
        /// </summary>
        /// <returns></returns>
        private List<DmColumn> GetQuotedColumns()
        {
            var columns = from column in Context.Columns
                          where column.XmlItem.ACodeID1 == ID
                          select column;
            var list = columns.ToList();
            return list;

        }


        public string GetCodeName()
        {
            return string.Format("{0}({1})", XmlItem.Code, XmlItem.Name);
        }

    }
}
