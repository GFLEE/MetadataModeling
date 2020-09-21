using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    public class DmDefaultColumn : DmColumn
    {
        public DmDefaultColumn(DmContext context, ColumnXml xmlItem) : base(context, xmlItem)
        {

        }

        public override ColumnXml XmlItemDeepClone()
        {
            var item = base.XmlItemDeepClone();
            item.ColumnID = CommonUtility.NewID();
            item.TableID = "";
            //设置是从当前列进行引用的
            item.DefaultColumnID = this.ID;
            return item;
        }
        /// <summary>
        /// 获取使用当前列的所有表的集合
        /// </summary>
        /// <returns></returns>
        public List<DmTable> GetTables()
        {
            var query = from table in Context.Tables
                        join column in Context.Columns on table.XmlItem.TableID equals column.XmlItem.TableID
                        where column.XmlItem.DefaultColumnID == ID
                        orderby table.XmlItem.Code
                        select table;
            var list = query.ToList();
            return list;
        }


        public void UpdateData()
        {
            //与当前默认列相关联的列的集合

            var query = from column in Context.Columns
                        where column.XmlItem.DefaultColumnID == this.ID
                        orderby column.XmlItem.Code
                        select column.XmlItem;
            var list = query.ToList();
            Type typeColumnXml = typeof(ColumnXml);

            //不需要更新的列的集合
            List<string> ignorNams = new List<string>()
            {
                "ColumnID",
                "TableID",
                "RefFkColumnID",
                "DefaultColumnID",
                "Name",
                "LabelName",
                "LabelShortName",
                "LabelToolTips",
                "Description"
            };

            var updateProperties = typeColumnXml.GetProperties()
                .Where(prop => !ignorNams.Contains(prop.Name) && prop.CanWrite)
                .ToList();
            foreach (var item in list)
            {
                foreach (var prop in updateProperties)
                {
                    var value = prop.GetValue(XmlItem);
                    prop.SetValue(item, value);
                }
            }



        }




    }
}
