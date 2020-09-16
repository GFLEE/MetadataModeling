using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace MetadataModeling.Common
{
    public class SerializeUtility
    {
        /// <summary>
        /// Xml序列化成二进制
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
    }
}
