using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Serialization
{
    internal class DataSerializer
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // Вот эта строка Вам поможет с кодировкой
            WriteIndented = true
        };

        public void BinarySerialize(object data, string filePath)
        {
            BinaryFormatter bf = new();
            if (File.Exists(filePath)) File.Delete(filePath);

            using (FileStream fs = File.Create(filePath))
            {
                bf.Serialize(fs, data);
            }
        }

        public void XmlSerialize(Type dataType, object data, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(dataType);
            if (File.Exists(filePath)) File.Delete(filePath);

            TextWriter writer = new StreamWriter(filePath);
            xmlSerializer.Serialize(writer, data);
            writer.Close();
        }

        public void JsonSerialize(Type dataType, object data, string filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            TextWriter writer = new StreamWriter(filePath);
            writer.Write(JsonSerializer.Serialize(data, jsonOptions));
            writer.Close();
        }
    }
}
