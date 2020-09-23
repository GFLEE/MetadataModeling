using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    public class DmTableGroup : DmItem<TableGroupXml>, ICopy
    {
        public DmTableGroup(DmContext context, TableGroupXml xmlItem)
        {
            this.Context = context;
            this.XmlItem = xmlItem;
            this.Tables = new List<DmTable>();
        }

        public List<DmTable> Tables { get; set; }

        internal void Init()
        {
            RootXml rootXml = Context.RootXml;

            if (rootXml.Tables == null || rootXml.Tables.Count == 0)
            {
                return;
            }
            var xmlTables = rootXml.Tables.Where(p => p.TableGroupID == this.XmlItem.TableGroupID).ToList();

            xmlTables.ForEach(table =>
            {
                var dmTable = new DmTable(Context, table);
                dmTable.TableGroup = this;
                //添加到当前集合
                Tables.Add(dmTable);
                //初始化DmTable
                dmTable.Init();
                //添加到上下文集合
                Context.Tables.Add(dmTable);
            });
        }

        public override string ID
        {
            get
            {
                return XmlItem.TableGroupID;
            }
        }
        public override string Name
        {
            get
            {
                return XmlItem.Name;
            }
            set { XmlItem.Name = value; }
        }



        public DmTable NewTable()
        {
            var tableXml = new TableXml();
            tableXml.TableGroupID = this.ID;
            tableXml.TableID = CommonUtility.NewID();
            tableXml.Code = "Table";
            tableXml.Name = "Table";
            //添加新对象到可序列化集合中
            Context.RootXml.Tables.Add(tableXml);

            var dmTable = new DmTable(Context, tableXml);
            dmTable.TableGroup = this;

            Context.Tables.Add(dmTable);

            this.Tables.Add(dmTable);

            //添加系统默认和建议的列
            var column1 = Context.DefaultColumnEx.GetColumnByKeys(new List<string>()
            {
                DmDefaultColumnEx.CstTableRequiredColumns,
                DmDefaultColumnEx.CstTableSuggestedColumns
            });
            column1.ForEach(p =>
            {
                var item = p.XmlItemDeepClone();
                dmTable.AddColumn(item);
            });


            return dmTable;
        }



        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="removeChild"></param>
        public void RemoveTable(DmTable table, bool removeChild = true)
        {
            if (table.Columns.Count == 0)
            {
                return;
            }
            if (removeChild)
            {
                //删除表中列
                table.Columns.ToList().ForEach(t => { table.RemoveColumn(t); });
                //从存储集合中删除表
                Context.RootXml.Tables.Remove(table.XmlItem);
                //序列化过程中删除表
                Context.Tables.Remove(table);
            }

            table.TableGroup = null;
            //删除当前表存储对象
            this.Tables.Remove(table);

        }
        public void AddTable(DmTable table)
        {
            table.TableGroup = this;
            this.Tables.Add(table);
            table.TableGroupID = this.ID;
            table.TableGroup = this;
        }


        /// <summary>
        /// 根据 TableID 删除
        /// </summary>
        /// <param name="tableID"></param>
        public void RemoveTable(string tableID)
        {
            var dmT = Tables.FirstOrDefault(p => p.ID == tableID);
            if (dmT == null)
            {
                return;
            }
            RemoveTable(dmT);
        }



        public string GetCodeName()
        {
            return string.Format("{0}({1})", XmlItem.Code, XmlItem.Name);

        }
        /// <summary>
        /// 获取组中重复的列名
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableValidateMessages()
        {
            var validateMessages = new List<string>();

            Tables.GroupBy(p => p.XmlItem.Code)
                .ToList()
                .ForEach(t =>
                {
                    if (t.Count() == 1)
                    {
                        return;
                    }
                    validateMessages.Add("“" + GetCodeName() + "”组中的“" + t.First().GetCodeName() + "表名重复");
                });

            return validateMessages;
        }


        public List<CopyData> GetCopyDatas()
        {
            var datas = new List<CopyData>();
            foreach (var table in Tables)
            {
                datas.AddRange(table.GetCopyDatas());
            }
            var str = GetDeepCloneString();
            var data = new CopyData()
            {
                Key = typeof(TableGroupXml).FullName,
                Value = str
            };
            datas.Add(data);
            return datas;
        }



    }
}
