using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Configuration;

public partial class System_AccountM_AccountCheck_Detail_DownloadOP : BasePage
{
    public System_AccountM_AccountCheck_Detail_DownloadOP()
    {
        base.AddPower("/System/AccountM/AccountCheck.aspx", MyPowerEnum.下載);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");

        if (Request.HttpMethod.Equals("POST"))
        {
            int ID = GetNumber<int>("i");

            DownloadVM VM = new DownloadVM(ID);

            var user = AuthServer.GetLoginUser();


            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_FileM_xGetFileInfoByID"
                                         , new Dictionary<string, object>()
                                         {
                                                { "@ID", VM.ID },
                                                { "@RoleID", user.RoleID },
                                         });
           
            if (dt.Rows.Count > 0)
            {
                string Json = JsonConvert.SerializeObject(VM);
                string code = QueryStringEncryptToolS.Encrypt(Json);
                Response.Redirect(WebConfigurationManager.AppSettings["FileServerURL"] + "/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
                Response.End();
            }
            else
            {
                Response.Redirect("~/html/ErrorPage/NoPower.html");
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}