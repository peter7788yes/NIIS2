using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_ElectronBulletinM_NewsPublished_Delete_NewsPublishedFileOP : BasePage
{
    public System_ElectronBulletinM_NewsPublished_Delete_NewsPublishedFileOP()
    {
        base.AddPower("/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx", MyPowerEnum.刪除);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int FileID = 0;
        int Success = 0;

        int.TryParse(Request.Form["FileID"], out FileID);

        if (FileID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xDeleteInfoDataFile", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileID", FileID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();
                Success = (int)sp.Value;

            }
        }

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("Success", Success);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}