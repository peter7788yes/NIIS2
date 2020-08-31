using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class System_DocumentM_DocumentMaintain_DeleteDocFileOP : BasePage
{
    public System_DocumentM_DocumentMaintain_DeleteDocFileOP()
    {
        base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.刪除);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int ID = GetNumber<int>("i");
        int DocumentInfoID = GetNumber<int>("d");

        if (ID == 0 || DocumentInfoID==0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataSet ds = new DataSet();
        int FileCount = 0;
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@FileCount", ID }, { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_DocumentM_xDeleteDocFileByID"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                { "@DocumentInfoID", DocumentInfoID },
                                                { "@FileInfoID", ID }
                                        });
        FileCount = (int)OutDict["@FileCount"];
        Chk = (int)OutDict["@Chk"];

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("FileCount", FileCount);
        dict.Add("Chk", Chk);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}