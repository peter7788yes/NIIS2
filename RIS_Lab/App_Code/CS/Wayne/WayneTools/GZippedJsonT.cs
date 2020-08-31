using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace WayneTools
{
    public class GZippedJsonT
    {
        public void Run()
        {
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