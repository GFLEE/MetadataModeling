using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;


namespace MetadataModeling.Base.Database
{
    public class DmColumn : DmItem<ColumnXml>, ICopy
    {
        public DmColumn(DmContext context, ColumnXml xmlItem)
        {
            this.XmlItem = xmlItem;
            this.Context = context;
        }

        /// <summary>
        /// 当期列所属的表对象
        /// </summary>
        public DmTable Table { get; set; }

        public override string ID
        {
            get
            {
                return XmlItem.ColumnID;
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

        public override ColumnXml XmlItemDeepClone()
        {
            var item = base.XmlItemDeepClone();
            item.ColumnID = CommonUtility.NewID();
            item.TableID = "";
            return item;
        }

        public List<CopyData> GetCopyDatas()
        {
            var str = GetDeepCloneString();
            var data = new CopyData()
            {
                Key = typeof(ColumnXml).FullName,
                Value = str
            };
            return new List<CopyData>() { data };
        }


        /// <summary>
        /// 检查约束,如果存在约束，列不能删除
        /// </summary>
        /// <returns></returns>
        public string CheckConstraint()
        {
            var columns = GetQuotedColumns();
            string table_name = "";
            if (columns.Count.Equals(0))
            {
                return "";
            }
            columns.ForEach(c =>
            {
                var dmT = this.Context.Tables.FirstOrDefault(p => p.ID.Equals(c.XmlItem.TableID));
                table_name += dmT.Name;
            });
            return string.Format("此表是“{0}”表的外键，不可删除", table_name);

        }

        /// <summary>
        /// 获取引用当前列的列的集合
        /// </summary>
        /// <returns></returns>
        private List<DmColumn> GetQuotedColumns()
        {
            var columns = from column in Context.Columns
                          where column.XmlItem.RefFkColumnID == ID
                          select column;
            var list = columns.ToList();
            return list;
        }




        public string CodeName
        {
            get
            {
                return GetCodeName();
            }
        }

        private string GetCodeName()
        {
            return string.Format("{0}({1})", XmlItem.Code, XmlItem.Name);
        }

        private string GetTableColumnCodeName()
        {
            var table = Context.Tables.First(p => p.ID.Equals(XmlItem.TableID));
            var result = string.Format("[{0}].[{1}]", table.GetCodeName(), GetCodeName());
            return result;
        }
        /// <summary>
        /// 获取系统列
        /// </summary>
        /// <returns></returns>
        public DmDefaultColumn GetDefaultColumn()
        {
            var defaultColumn = Context.DefaultColumns.FirstOrDefault(p => p.ID == XmlItem.DefaultColumnID);
            return defaultColumn;
        }
    }
}
