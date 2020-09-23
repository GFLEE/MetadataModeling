using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MetadataModeling.Common;
using MetadataModeling.Base.Database.Xml;
using System.IO;
using MetadataModeling.Base.Enums;

namespace MetadataModeling.Base.Database
{
    public class DmContext
    {
        public DmContext()
        {
            Renew();
        }

        public void Renew()
        {
            RootXml = new RootXml();
            Init(RootXml);
        }


        public void InitRootXmlNull(RootXml root)
        {
            if (root.Groups == null)
            {
                root.Groups = new List<TableGroupXml>();
            }
            if (root.Tables == null)
            {
                root.Tables = new List<TableXml>();
            }
            if (root.Columns == null)
            {
                root.Columns = new List<ColumnXml>();
            }
            if (root.ACode1s == null)
            {
                root.ACode1s = new List<ACodeXml>();
            }
            if (root.DefaultColumns == null)
            {
                root.DefaultColumns = new List<ColumnXml>();
            }
            if (root.DefaultColumnEx == null)
            {
                root.DefaultColumnEx = new DefaultColumnExXml()
                {
                    TableRequiredColumns = new List<string>(),
                    TableSuggestedColumns = new List<string>()
                };
            }
        }
        /// <summary>
        /// 获取表必备列
        /// </summary>
        /// <param name="columnID"></param>
        /// <returns></returns>
        public bool GetIsTableRequiredColumn(string columnID)
        {
            return GetIsTableEx(columnID, RootXml.DefaultColumnEx.TableRequiredColumns);
        }
        /// <summary>
        /// 设置表必备列
        /// </summary>
        /// <param name="columnID"></param>
        /// <param name="isRequired"></param>
        public void SetIsTableRequiredColumn(string columnID, bool isRequired)
        {
            SetIsTableEx(columnID, RootXml.DefaultColumnEx.TableRequiredColumns, isRequired);
        }


        /// <summary>
        /// 获取表建议列
        /// </summary>
        /// <param name="columnID"></param>
        /// <returns></returns>
        public bool GetIsTableSuggestedColumn(string columnID)
        {
            return GetIsTableEx(columnID, RootXml.DefaultColumnEx.TableSuggestedColumns);
        }
        /// <summary>
        /// 设置表建议列
        /// </summary> 
        /// <param name="columnID"></param>
        /// <param name="isRequired"></param>
        public void SetIsTableSuggestedColumn(string columnID, bool isRequired)
        {
            SetIsTableEx(columnID, RootXml.DefaultColumnEx.TableSuggestedColumns, isRequired);
        }

        private bool GetIsTableEx(string columnID, List<string> ids)
        {
            return ids.Contains(columnID);

        }
        private void SetIsTableEx(string columnID, List<string> ids, bool flag)
        {
            var isExist = ids.Contains(columnID);
            if (flag && !isExist)
            {
                ids.Add(columnID);
            }
            else if (!flag && isExist)
            {
                ids.Remove(columnID);
            }

        }

        /// <summary>
        /// 从文件加载（反序列化）
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFromFile(string filePath)
        {
            var rootXml = SerializeUtility.DeserializeFromFile(filePath, typeof(RootXml));
            RootXml = (RootXml)rootXml;
            Init((RootXml)rootXml);
        }
        /// <summary>
        /// 从字符串中加载（反序列化）
        /// </summary>
        /// <param name="content"></param>
        public void LoadFromString(string content)
        {
            var rootXml = SerializeUtility.Deserialize(content, typeof(RootXml));
            RootXml = (RootXml)rootXml;
            Init((RootXml)rootXml);
        }
        /// <summary>
        /// 从流中加载（反序列化）
        /// </summary>
        /// <param name="bytes"></param>
        public void LoadFromBytes(byte[] bytes)
        {
            var rootXml = SerializeUtility.Deserialize(bytes, typeof(RootXml));
            RootXml = (RootXml)rootXml;
            Init((RootXml)rootXml);
        }
        public List<DmTableGroup> Groups { get; set; }
        public List<DmTable> Tables { get; private set; }
        public List<DmColumn> Columns { get; private set; }
        public List<DmACode> ACode1s { get; private set; }
        public List<DmDefaultColumn> DefaultColumns { get; private set; }
        public DmDefaultColumnEx DefaultColumnEx { get; private set; }


        public RootXml RootXml { get; private set; }


        /// <summary>
        /// 初始化RootXml
        /// </summary>
        /// <param name="rootXml"></param>
        public void Init(RootXml root)
        {
            InitRootXmlNull(root);

            Groups = new List<DmTableGroup>();
            Tables = new List<DmTable>();
            Columns = new List<DmColumn>();
            ACode1s = new List<DmACode>();
            DefaultColumns = new List<DmDefaultColumn>();

            root.Groups.ForEach(g =>
            {
                var dmg = new DmTableGroup(this, g);
                dmg.Init();
                this.Groups.Add(dmg);
            });

            root.ACode1s.ForEach(a =>
            {
                var dmg = new DmACode(this, a);
                ACode1s.Add(dmg);
            });

            root.DefaultColumns.ForEach(a =>
            {
                var dm = new DmDefaultColumn(this, a);
                DefaultColumns.Add(dm);
            });
            DefaultColumnEx = new DmDefaultColumnEx(this, root.DefaultColumnEx);
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveToFile(string filePath)
        {
            SerializeUtility.SerializeToFile(filePath, RootXml, null);
        }

        /// <summary>
        /// (序列化)RootXml保存为string
        /// </summary>
        /// <returns></returns>
        public string GetSaveString()
        {
            var result = SerializeUtility.Serialize(RootXml);
            return result;
        }
        /// <summary>
        /// (序列化)RootXml保存为Bytes
        /// </summary>
        /// <returns></returns>
        public byte[] GetSaveBytes()
        {
            var result = SerializeUtility.SerializeToArray(RootXml);
            return result;
        }

        /// <summary>
        /// 创建新Group
        /// </summary>
        /// <returns></returns>
        public DmTableGroup NewGroup()
        {
            var tgXml = new TableGroupXml();
            tgXml.TableGroupID = CommonUtility.NewID();
            tgXml.Code = "Group";
            tgXml.Name = "Group";
            //添加新对象到序列化集合中
            this.RootXml.Groups.Add(tgXml);

            var dm = new DmTableGroup(this, tgXml);
            this.Groups.Add(dm);
            return dm;
        }

        /// <summary>
        /// 返回新的默认列
        /// </summary>
        /// <returns></returns>
        public DmDefaultColumn NewDmDefaultColumn()
        {
            var columnXml = new ColumnXml();

            columnXml.TableID = "DEFAULTCOLUMN";
            columnXml.ColumnID = CommonUtility.NewID();
            columnXml.Code = "DefaultColumn";
            columnXml.Name = "DefaultColumn";
            //添加新对象到序列化集合中
            RootXml.DefaultColumns.Add(columnXml);

            var dmColumn = new DmDefaultColumn(this, columnXml);
            DefaultColumns.Add(dmColumn);
            return dmColumn;
        }

        /// <summary>
        /// 添加新ACode
        /// </summary>
        /// <returns></returns>
        public DmACode NewACode()
        {
            var aCodeXml = new ACodeXml();
            aCodeXml.ACodeID = CommonUtility.NewID();
            aCodeXml.Code = "ACode";
            aCodeXml.Name = "ACode";
            //添加新对象到序列化集合中
            RootXml.ACode1s.Add(aCodeXml);

            var dmAcode = new DmACode(this, aCodeXml);
            ACode1s.Add(dmAcode);
            return dmAcode;
        }

        /// <summary>
        /// 移动表到Group
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="tableGroup">新的Group</param>
        public void MoveTable(DmTable table, DmTableGroup tableGroup)
        {
            table.TableGroup.RemoveTable(table, false);
            tableGroup.AddTable(table);
        }

        /// <summary>
        /// 向表添加列
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public DmColumn AddColumn(DmTable table, DmColumn column)
        {
            var cloneColumn = column.XmlItemDeepClone();
            return table.AddColumn(cloneColumn);
        }

        /// <summary>
        /// 移除ACode
        /// </summary>
        /// <param name="aCode"></param>
        public void RemoveACode(DmACode aCode)
        {
            var msg = aCode.CheckConstraint();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                throw new NotImplementedException(msg);

            }
            //序列化过程中删除
            ACode1s.Remove(aCode);
            //从存储集合对象中删除当前记录
            RootXml.ACode1s.Remove(aCode.XmlItem);

        }
        /// <summary>
        /// 移除默认列
        /// </summary>
        /// <param name="column"></param>
        public void RemoveDefaultColumn(DmDefaultColumn column)
        {
            //序列化过程中删除
            DefaultColumns.Remove(column);
            //从存储集合对象中删除当前记录
            RootXml.DefaultColumns.Remove(column.XmlItem);
        }

        /// <summary>
        /// 删除 组 
        /// </summary>
        /// <param name="g"></param>
        public void RemoveGroup(DmTableGroup g)
        {
            if (g.Tables.Count == 0)
            {
                return;
            }
            //从分组中删除表
            g.Tables.ForEach(t => { g.RemoveTable(t); });
            //从存储集合中删除当前记录
            RootXml.Groups.Remove(g.XmlItem);
            //删除当前存储组对象
            this.Groups.Remove(g);
            //序列化过程中删除记录
            Groups.Remove(g);
        }


        public void RemoveGroup(string gID)
        {
            var dmG = Groups.FirstOrDefault(p => p.ID == gID);
            if (dmG == null)
            {
                return;
            }
            RemoveGroup(dmG);
        }

        /// <summary>
        /// 返回所有枚举
        /// </summary>
        /// <returns></returns>
        public List<Enums.Xml.EnumXml> GetEnumXmls()
        {
            if (string.IsNullOrWhiteSpace(RootXml.EnumFileName) || string.IsNullOrWhiteSpace(FilePath))
            {
                return new List<Enums.Xml.EnumXml>();
            }
            var dir = Path.GetDirectoryName(FilePath);
            var enumFileName = Path.Combine(dir, RootXml.EnumFileName);
            var enumContext = new EnumContext();
            enumContext.LoadFromFile(enumFileName);
            var enumCodes = enumContext.GetEnums();
            return enumCodes;
        }

        /// <summary>
        /// 加载或保存文件之后的路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 获取当前目录下所有.xml文件
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileNames()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                return new List<string>();

            }
            var dir = Path.GetDirectoryName(FilePath);
            var files = Directory.GetFiles(dir, "*.xml", SearchOption.TopDirectoryOnly);
            var result = new List<string>();
            foreach (var file in files)
            {
                if (file.ToLower() != FilePath.ToLower())
                {
                    result.Add(Path.GetFileName(file));
                }
            }
            return result;
        }


        public void Create(string parentID, List<CopyData> datas)
        {
            var objs = new List<object>();
            objs = datas.Select(p =>
            {
                var type = Type.GetType(p.Key);
                var obj = SerializeUtility.Deserialize(Convert.FromBase64String(p.Value), type);
                return obj;
            }).ToList();

            var xmltgs = objs.OfType<TableGroupXml>().ToList();
            var xmlts = objs.OfType<TableXml>().ToList();
            var xmlcs = objs.OfType<ColumnXml>().ToList();

            //包含分组
            if (xmltgs.Count > 0)
            {
                foreach (var xmltg in xmltgs)
                {
                    var oldID = xmltg.TableGroupID;
                    var tg = Create(xmltg);
                }
            }

        }

        public void Create(string oldParentTgID, DmTableGroup dtg, List<TableXml> xmlts, List<ColumnXml> xmlcs)
        {
            var items = xmlts;
            if (!string.IsNullOrWhiteSpace(oldParentTgID))
            {

            }
            foreach (var xmlt in xmlts.Where(p => p.TableGroupID == oldParentTgID))
            {
                //var item = 
            }
        }


        public DmTableGroup Create(TableGroupXml tgXml)
        {
            tgXml.TableGroupID = CommonUtility.NewID();

            //添加新对象到可序列化集合中 
            this.RootXml.Groups.Add(tgXml);

            var dm = new DmTableGroup(this, tgXml);
            this.Groups.Add(dm);
            return dm;
        }




    }
}
