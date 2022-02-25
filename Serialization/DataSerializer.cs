using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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

        public void XMLSerialize(object data, string filePath)
        {

        }

        public void JSONSerialize(object data, string filePath)
        {

        }
    }
}
