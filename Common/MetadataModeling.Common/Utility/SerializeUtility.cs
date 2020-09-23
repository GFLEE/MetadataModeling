using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace MetadataModeling.Common
{
    /// <summary>
    /// Xml序列化工具
    /// </summary>
    public class SerializeUtility
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Serialize(obj, null);
        }

        private static string Serialize(object obj, Type[] knowTypes)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                IndentChars = "\t",
            };

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    XmlWriter writer = XmlWriter.Create(ms, xmlWriterSettings);
                    DataContractSerializer serializer = new DataContractSerializer(obj.GetType(), knowTypes);
                    serializer.WriteObject(writer, obj);
                    writer.Flush();
                    writer.Close();
                    ms.Position = 0;
                    return reader.ReadToEnd();
                } 
            }


        }

        public static byte[] SerializeToArray(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(ms, obj);
                var result = ms.ToArray();
                return result;
            }

        }

        public static object DeserializeFromFile(string filePath, Type type)
        {
            DataContractSerializer deserializer = new DataContractSerializer(type);
            using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return deserializer.ReadObject(fs);
            }
        }

        public static void SerializeToFile(string filePath, object obj, Type[] knownTypes)
        {
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType(), knownTypes);
            using (Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                var settings = new XmlWriterSettings() { Indent = true };
                using (XmlWriter xmlWriter = XmlWriter.Create(fs, settings))
                {
                    serializer.WriteObject(xmlWriter, obj);
                }
            }

        }

        public static object Deserialize(byte[] bytes, Type type)
        {
            using (Stream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(type);
                return deserializer.ReadObject(stream);

            }
        }

        public static object Deserialize(string xml, Type toType)
        {
            return Deserialize(xml, toType, null);
        }

        private static object Deserialize(string xml, Type toType, Type[] knowTypes)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType, knowTypes);
                return deserializer.ReadObject(stream);
            }


        }


    }
}
