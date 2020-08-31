using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_DocumentM_DocumentMaintain_DeleteDocFileOP : BasePage
{
    public System_DocumentM_DocumentMaintain_DeleteDocFileOP()
    {
        base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.刪除);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int ID = 0;
        int DocumentInfoID = 0;

        int.TryParse(Request.Form["i"], out ID);
        int.TryParse(Request.Form["d"], out DocumentInfoID);

        if (ID == 0 || DocumentInfoID==0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataSet ds = new DataSet();
        int FileCount = 0;
        int Chk = 0;
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_DocumentM_xDeleteDocFileByID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocumentInfoID", DocumentInfoID);
                cmd.Parameters.AddWithValue("@FileInfoID", ID);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@FileCount", FileCount);
                sp1.Direction = ParameterDirection.Output;
                SqlParameter sp2 = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp2.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                FileCount = (int)sp1.Value;
                Chk = (int)sp2.Value;

            }
        }

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("FileCount", FileCount);
        dict.Add("Chk", Chk);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}