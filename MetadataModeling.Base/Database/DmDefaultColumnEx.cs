using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using MetadataModeling.Base.Database.Xml;
using MetadataModeling.Common;

namespace MetadataModeling.Base.Database
{
    public class DmDefaultColumnEx
    {
        public const string CstTableRequiredColumns = "TableRequiredColumns";
        public const string CstTableSuggestedColumns = "TableSuggestedColumns";
        public DmDefaultColumnEx(DmContext context, DefaultColumnExXml xmlItem)
        {
            this.XmlItem = xmlItem;
            this.Context = context;

            Kvs = new List<DmKv>();
            Kvs.Add(new DmKv("TableRequiredColumns", "表必备列")); ;
            Kvs.Add(new DmKv("TableSuggestedColumns", "表建议列")); ;

        }

        /// <summary>
        /// Xml存储对象
        /// </summary>
        public DefaultColumnExXml XmlItem { get; protected set; }
        public DmContext Context { get; protected set; }
        public List<DmKv> Kvs { get; protected set; }

        public List<string> GetColumnKeys(string columnID)
        {
            var result = new List<string>();
            foreach (var kv in Kvs)
            {
                var list = GetListByKey(kv.K);
                if (list.Contains(columnID))
                {
                    result.Add(kv.K);
                }
            }
            return result;
        }


        public IReadOnlyList<string> TableRequiredColumns
        {
            get
            {
                return XmlItem.TableRequiredColumns;
            }
        }
        public IReadOnlyList<string> TableSuggestedColumns
        {
            get
            {
                return XmlItem.TableSuggestedColumns;
            }
        }

        public List<string> GetListByKey(string key)
        {
            var p = typeof(DefaultColumnExXml).GetRuntimeProperty(key);
            var result = p.GetValue(XmlItem) as List<string>;
            return result;
        }

        public void SetColumnKeys(string columnID, List<string> keys)
        {
            foreach (var kv in Kvs)
            {
                var list = GetListByKey(kv.K);
                if (keys.Contains(kv.K))
                {
                    if (!list.Contains(columnID))
                    {
                        list.Add(columnID);
                    }

                }
                else
                {
                    if (list.Contains(columnID))
                    {
                        list.Remove(columnID);
                    }

                }
            }

        }

        public List<DmDefaultColumn> GetColumnByKeys(List<string> keys)
        {
            var list = new List<string>();
            Kvs.ForEach(p =>
            {
                if (keys.Contains(p.K))
                {
                    list.AddRange(GetColumnIDsByKey(p.K));
                }
            });

            var query = from c in Context.DefaultColumns
                        join cid in list.Distinct() on c.ID equals cid
                        select c;
            return query.ToList();
        }


        public List<string> GetColumnIDsByKey(string key)
        {
            return GetListByKey(key).ToList();
        }


        public void SetColumnIDsByKey(string key, List<string> columnIDs)
        {
            var list = GetListByKey(key);
            list.Clear();
            list.AddRange(columnIDs);
        }



    }
}
