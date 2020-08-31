using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class System_DocumentM_DocumentViewDownload_Detail : BasePage
{
    public DocumentInfoVM VM = new DocumentInfoVM();
    public new int ID = 0;
    public List<DocumentFileInfoVM> fileList = new List<DocumentFileInfoVM>();
    public string FileList = "[]";

    public System_DocumentM_DocumentViewDownload_Detail()
    {
        base.AddPower("/System/DocumentM/DocumentViewDownload.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {

            ID = GetNumber<int>("i");

            if (ID == 0)
            {
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


           
            DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_DocumentM_xGetDocByID"
                                          , new Dictionary<string, object>()
                                          {
                                                    { "@DocumentInfoID",  ID }
                                          });
            
            EntityS.FillModel(VM, ds.Tables[0]);
            EntityS.FillModel(fileList, ds.Tables[1]);
            VM.FileList = fileList;
            FileList = JsonConvert.SerializeObject(VM.FileList);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}