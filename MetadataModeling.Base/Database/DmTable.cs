using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;
using System.Text.RegularExpressions;

namespace MetadataModeling.Base.Database
{
    public class DmTable : DmItem<TableXml>, ICopy
    {
        public DmTable(DmContext context, TableXml xmlItem)
        {
            this.XmlItem = xmlItem;
            this.Context = context;

            this.Columns = new List<DmColumn>();
        }
        /// <summary>
        /// 列对象
        /// </summary>
        public List<DmColumn> Columns { get; private set; }

        /// <summary>
        /// 当前表所属的Group
        /// </summary>
        public DmTableGroup TableGroup { get; internal set; }


        internal void Init()
        {
            RootXml rootXml = Context.RootXml;

            if (rootXml.Columns == null || rootXml.Columns.Count == 0)
            {
                return;
            }
            var xmlColumns = rootXml.Columns.Where(p => p.TableID == this.XmlItem.TableID).ToList();

            xmlColumns.ForEach(table =>
            {
                var dmColumn = new DmColumn(Context, table);
                dmColumn.Table = this;
                Columns.Add(dmColumn);

                //添加到上下文集合
                Context.Columns.Add(dmColumn);
            });
        }

        public override string ID
        {
            get
            {
                return XmlItem.TableID;
            }
        }


        public string TableGroupID
        {
            get
            {
                return XmlItem.TableGroupID;
            }
            set
            {
                XmlItem.TableGroupID = value;
            }
        }

        public DmColumn NewColumn()
        {
            return NewColumn("Column", "Column");
        }
        /// <summary>
        /// 新列
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DmColumn NewColumn(string code, string name)
        {
            ColumnXml columnXml = new ColumnXml();

            columnXml.TableID = this.ID;
            columnXml.ColumnID = CommonUtility.NewID();
            columnXml.Code = code;
            columnXml.Name = name;

            //添加新对象到序列化的集合中
            Context.RootXml.Columns.Add(columnXml);

            var dmColumn = new DmColumn(Context, columnXml);
            Context.Columns.Add(dmColumn);
            this.Columns.Add(dmColumn);
            dmColumn.Table = this;
            return dmColumn;
        }


        public DmColumn AddColumn(ColumnXml columnXml)
        {
            columnXml.TableID = this.ID;
            Context.RootXml.Columns.Add(columnXml);

            var dmColumn = new DmColumn(Context, columnXml);
            Context.Columns.Add(dmColumn);
            this.Columns.Add(dmColumn);
            dmColumn.Table = this;

            return dmColumn;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(DmColumn column)
        {
            column.Table = this;
            this.Columns.Add(column);
            column.XmlItem.TableID = this.ID;
        }

        public void RemoveColumn(DmColumn column)
        {
            var msg = column.CheckConstraint();
            if (!string.IsNullOrEmpty(msg))
            {
                throw new NotImplementedException(msg);
            }
            ///序列化过程中列删除
            Context.Columns.Remove(column);
            ///从存储集合对象删除当前记录
            Context.RootXml.Columns.Remove(column.XmlItem);
            ///删除当前存储列对象
            this.Columns.Remove(column);

        }
        /// <summary>
        /// 根据columnID删除
        /// </summary>
        /// <param name="columnID"></param>
        public void RemoveColumn(string columnID)
        {
            var dmC = Columns.FirstOrDefault(p => p.ID == columnID);
            if (null == dmC)
            {
                return;
            }

            RemoveColumn(dmC);
        }
        public override string Name
        {
            get
            {
                return XmlItem.Name;
            }
            set { XmlItem.Name = value; }
        }
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <returns></returns>
        public DmTableGroup GetDmTableGroupName()
        {
            var dmTableGroup = Context.Groups.FirstOrDefault(p => p.ID == XmlItem.TableGroupID);
            return dmTableGroup;
        }
        public string CodeName
        {
            get
            {
                return GetCodeName();
            }
        }

        public string GetCodeName()
        {
            return string.Format("{0}({1})", XmlItem.Code, XmlItem.Name);

        }

        /// <summary>
        /// 获取验证信息
        /// </summary>
        /// <param name="isShowTableName"></param>
        /// <returns></returns>
        public List<string> GetValidataMessages(bool isShowTableName = false)
        {
            List<string> validataMessages = new List<string>();

            //验证表中是否有主键
            var column = Columns.FirstOrDefault(p => p.XmlItem.IsPK);
            if (null == column)
            {
                validataMessages.Add("“" + GetCodeName() + "”表中没有主键");
            }
            //验证表中的数据类型是否为None
            Columns.Where(p => p.XmlItem.ColumnType == EuColumnType.None)
                .ToList()
                .ForEach(c =>
                {
                    validataMessages.Add("“" + GetColumnName(c, isShowTableName) + "”列的数据类型为None");
                });
            //列名重复检查
            Columns.GroupBy(p => p.XmlItem.Code)
                .ToList()
                .ForEach(p =>
                {
                    if (p.Count() == 1)
                    {
                        return;
                    }
                    validataMessages.Add("“" + GetCodeName() + "”表中的" + GetColumnName(p.First(), isShowTableName) + "”列名重复");
                });

            return validataMessages;
        }

        public string GetColumnName(DmColumn column, bool isShowTableName)
        {
            if (isShowTableName)
            {
                return column.GetTableColumnCodeName();

            }
            return column.GetCodeName();
        }


        /// <summary>
        /// 同步主键信息
        /// </summary>
        /// <returns>如果是新创建主键，返回新创建主键列对象</returns>
        public DmColumn SynchroPk()
        {
            if (!this.XmlItem.IsAutoPk)
            {
                return null;
            }
            var pkColumn = Columns.FirstOrDefault(p => p.XmlItem.IsPK);
            var code = this.XmlItem.Code + "ID";
            code = Regex.Replace(code, "[^_]*_", "");
            if (pkColumn == null)
            {
                pkColumn = NewColumn(code, this.Name);
                pkColumn.XmlItem.IsPK = true;
                pkColumn.XmlItem.IsNullable = false;
                pkColumn.XmlItem.IsIdentity = true;
                pkColumn.XmlItem.ColumnType = EuColumnType.Int;
                return pkColumn;
            }
            else
            {
                pkColumn.XmlItem.Code = code;
                pkColumn.XmlItem.Name = this.Name;
            }

            return null;
        }


        public List<CopyData> GetCopyDatas()
        {
            var datas = new List<CopyData>();
            foreach (var column in Columns)
            {
                datas.AddRange(column.GetCopyDatas());
            }
            var str = GetDeepCloneString();
            var data = new CopyData()
            {
                Key = typeof(TableXml).FullName,
                Value = str
            };
            datas.Add(data);
            return datas;
        }
    }
}
