using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace Serialization
{
    internal class DataSerializer
    {
        public void BinarySerialize(object data, string filePath)
        {
            BinaryFormatter bf = new();
            if (File.Exists(filePath)) File.Delete(filePath);

            using (FileStream fs = File.Create(filePath))
            {
                bf.Serialize(fs, data);
            }
        }

        public void XmlSerialize(object data, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Object));
            if (File.Exists(filePath)) File.Delete(filePath);

            TextWriter writer = new StreamWriter(filePath);
            xmlSerializer.Serialize(writer, data);
            writer.Close();
        }

        public void JsonSerialize(object data, string filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            TextWriter writer = new StreamWriter(filePath);
            writer.Write(data);
            writer.Close();
        }
    }
}
