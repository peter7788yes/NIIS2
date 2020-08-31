using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace WayneTools
{
    public class GZippedJsonT
    {
        public void Run()
        {
            //SharedClasses.Student student1 = new SharedClasses.Student();

            // Serializes the object to a JSON string.
            //string jsonStr = JsonConvert.SerializeObject(student1, Formatting.Indented);

            // GZip compress.
            //byte[] zipped = Compress(jsonStr);
            //Console.WriteLine("GZipped JSON serialized length: " + zipped.Length);

            // GZip decompress.
            //string unZippedJsonStr = Decompress(zipped);

            // Deserialize the JSON string to an object.
            //Type aType = Type.GetType("SharedClasses.Student");
            //Student stu = (Student)JsonConvert.DeserializeObject(unZippedJsonStr, aType);

            //Console.WriteLine(stu.Addr.City);
        }

        public byte[] Compress(string jsonStr)
        {
            MemoryStream zippedStream = new MemoryStream();
            using (GZipStream gzip = new GZipStream(zippedStream, CompressionMode.Compress))
            {
                BinaryFormatter bfmt = new BinaryFormatter();
                bfmt.Serialize(gzip, jsonStr);
                gzip.Flush();
            }

            byte[] zippedBuf = zippedStream.ToArray();
            return zippedBuf;
        }

        public string Decompress(byte[] zippedJson)
        {
            string jsonStr;

            MemoryStream zippedStream = new MemoryStream(zippedJson);
            using (GZipStream gzip = new GZipStream(zippedStream, CompressionMode.Decompress))
            {
                BinaryFormatter bfmt = new BinaryFormatter();
                jsonStr = (string)bfmt.Deserialize(gzip);
            }

            return jsonStr;
        }
    }
}