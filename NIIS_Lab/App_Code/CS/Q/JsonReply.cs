using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// JsonReply 的摘要描述
/// </summary> 
public class JsonReply 
{
    public int RetCode; 
    public string Status;
    public string Content;
    public JsonReply( ) 
    {
        RetCode = 0; // default 
        Status = "";
        Content = "";
    }

    public JsonReply(string content)
        : base()
    {
        RetCode = 0; // default 
        Status = "";
        Content = content; 
    }

    public JsonReply(int retCode, string status, string content)
        : base()
    {
        RetCode = retCode;
        Status = status;
        Content = content;
    }
    public string Serialize(bool ident)
    {
        try
        {
            return JsonConvert.SerializeObject(this);
        }
        catch
        {
            return string.Empty;
        }
    }
}

