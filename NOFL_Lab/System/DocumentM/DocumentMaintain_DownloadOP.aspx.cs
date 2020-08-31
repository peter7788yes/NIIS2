using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_DocumentMaintain_DownloadOP : BasePage
{

    public System_DocumentMaintain_DownloadOP()
    {
        base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.下載);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int ID = 0;
        int.TryParse(Request["i"], out ID);

        DownloadVM VM = new DownloadVM(ID);

        var user = AuthServer.GetLoginUser();

        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_FileM_xGetFileInfoByID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", VM.ID);
                cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        if (dt.Rows.Count > 0)
        {
            string Json = JsonConvert.SerializeObject(VM);
            string code = QueryStringEncryptToolS.Encrypt(Json);
            Response.Redirect("http://niis_fs.hyweb.com.tw/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
            //Response.Redirect("http://localhost:64351/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
            Response.End();
        }
        else
        {
            Response.Redirect("~/html/ErrorPage/NoPower.html");
        }
    }
}