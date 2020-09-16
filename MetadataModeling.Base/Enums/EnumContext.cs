using MetadataModeling.Base.Enums.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using MetadataModeling.Common;
using System.Linq;
using System.IO;

namespace MetadataModeling.Base.Enums
{
    /// <summary>
    /// EnumContext
    /// </summary>
    public class EnumContext
    {
        public RootXml RootXml { get; set; }

        public EnumContext()
        {
            Renew();
        }

        private void Renew()
        {
            RootXml = new RootXml();
            Init(RootXml);

        }

        private void Init(RootXml root)
        {
            if (root.Items == null)
            {
                root.Items = new List<GroupXml>();
                foreach (var g in root.Items)
                {
                    if (g.Items == null)
                    {
                        g.Items = new List<EnumXml>();
                    }
                    foreach (var e in g.Items)
                    {
                        if (e.Items == null)
                        {
                            e.Items = new List<EnumItemXml>();
                        }
                    }
                }
            }
        }

        #region 导入/保存文件
        public void LoadFromFile(string filePath)
        {
            var rootXml = SerializeUtility.DeserializeFromFile(filePath, typeof(RootXml));
            RootXml = (RootXml)rootXml;
            Init((RootXml)rootXml);
        }

        public void SaveToFile(string filePath)
        {
            SerializeUtility.SerializeToFile(filePath, RootXml, null);
        }

        #endregion

        public List<EnumXml> GetEnums()
        {
            return RootXml.Items.SelectMany(p => p.Items).OrderBy(p => p.Code).ToList();
        }
        /// <summary>
        /// 根据数组加载（反序列化）
        /// </summary>
        /// <param name="bytes"></param>
        public void LoadFromBytes(byte[] bytes)
        {
            var rootXml = SerializeUtility.Deserialize(bytes, typeof(RootXml));
            RootXml = (RootXml)rootXml;
            Init(RootXml);
        }
        /// <summary>
        /// 获取当前数据库元数据文件的目录中的其他xml文件
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileNames()
        {
            if (string.IsNullOrEmpty(FilePath))
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

        /// <summary>
        /// 加载或保存后的路径
        /// </summary>
        public string FilePath { get; set; }
    }
}
