using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MemoryT
{
    public  long GetObjectSize(object obj)
    {
        long size = 0;
        using (Stream sr = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(sr, obj);
            size = sr.Length;
        }

        return size;
    }

    public long GetObjectSize2(object TestObject)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        byte[] Array;
        bf.Serialize(ms, TestObject);
        Array = ms.ToArray();
        return Array.Length;
    }
}