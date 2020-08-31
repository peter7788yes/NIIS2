using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

/// <summary>
/// Class1 的摘要描述
/// </summary>
public class MemoryT
{
    public  long GetObjectSize(object obj)
    {
        long size = 0;
        //object o = new object();
        using (Stream sr = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(sr, obj);
            size = sr.Length;
        }

        return size;
    }

    /// <summary>
    /// Calculates the lenght in bytes of an object 
    /// and returns the size 
    /// </summary>
    /// <param name="TestObject"></param>
    /// <returns></returns>
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