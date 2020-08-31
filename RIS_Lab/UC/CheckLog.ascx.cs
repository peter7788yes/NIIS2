using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public partial class UC_CheckLog : BaseUserControl
{
    public string TableName { set; get; }
    public Dictionary<string, object> WhereDict  { set; get; }

    public string jsonString = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        PassLogVM VM = new PassLogVM();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        VM.TableName = TableName;
        VM.WhereDict = WhereDict;
        string Json = JsonConvert.SerializeObject(VM);
        //jsonString = Json;
        jsonString = QueryStringEncryptToolS.Encrypt(Json);
    }
}