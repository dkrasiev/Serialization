using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Serialization
{
    static class DataSerializer
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // Вот эта строка Вам поможет с кодировкой
            WriteIndented = true
        };

        public static bool BinarySerialize(object data, string filePath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath)) File.Delete(filePath);

            using (FileStream fs = File.Create(filePath))
            {
                try
                {
                    bf.Serialize(fs, data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool XmlSerialize(object data, string filePath)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
                if (File.Exists(filePath)) File.Delete(filePath);

                TextWriter writer = new StreamWriter(filePath);
                xmlSerializer.Serialize(writer, data);
                writer.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool JsonSerialize(object data, string filePath)
        {
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);

                TextWriter writer = new StreamWriter(filePath);
                writer.Write(JsonSerializer.Serialize(data, jsonOptions));
                writer.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
