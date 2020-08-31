using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Configuration;

public partial class AccountMaintain_Maintain_DownloadOP : BasePage
{
    public AccountMaintain_Maintain_DownloadOP()
    {
        base.AddPower("/System/AccountM/AccountMaintain_Maintain_DownloadOP.aspx", MyPowerEnum.下載);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");

        if (Request.HttpMethod.Equals("POST"))
        {
            int ID = 0;
            int.TryParse(QueryStringEncryptToolS.Decrypt(GetString("i")),out ID);

            DownloadVM VM = new DownloadVM(ID);

            var user = AuthServer.GetLoginUser();

            //DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_FileM_xGetFileInfoByID"
            //                              , new Dictionary<string, object>()
            //                              {
            //                                        { "@ID", VM.ID },
            //                                        { "@OrgID",  user.OrgID }
            //                              });

            //if (dt.Rows.Count > 0)
            //{
                string Json = JsonConvert.SerializeObject(VM);
                string code = QueryStringEncryptToolS.Encrypt(Json);
                Response.Write("<script>window.open('"+ WebConfigurationManager.AppSettings["FileServerURL"] + "/livestorage.aspx?o=" + HttpUtility.UrlEncode(code) + "','_blank');</script>");
                Response.End();
            //}
            //else
            //{
            //    Response.Redirect("~/html/ErrorPage/NoPower.html");
            //}
        }
        else
        {
            Response.Write("");
            Response.End();
        }
      
    }
}