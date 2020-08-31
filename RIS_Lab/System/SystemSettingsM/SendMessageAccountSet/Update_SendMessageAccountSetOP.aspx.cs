using AlanTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemSettingsM_SendMessageAccountSet_Update_SendMessageAccountSetOP : BasePage
{
    public System_SystemSettingsM_SendMessageAccountSet_Update_SendMessageAccountSetOP()
    {
        base.AddPower("/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int OrgID = 0;
        string MsgAccount;
        string MsgPassWord;
        int MsgStatus;
        int Success = 0;

        int.TryParse(Request.Form["OrgID"], out OrgID);
        MsgAccount = Request.Form["MsgAccount"];
        MsgPassWord = Request.Form["MsgPassWord"];
        int.TryParse(Request.Form["MsgStatus"], out MsgStatus);
        if (OrgID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
        }

        EncryptDES enc = new EncryptDES();

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_StMsgSet_xUpdateStMsgSetData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@MsgAccount", MsgAccount);
                cmd.Parameters.AddWithValue("@MsgPassWord", enc.ToEncryptDES(MsgPassWord, Convert.ToString(WebConfigurationManager.AppSettings["encryptKey"])));
                cmd.Parameters.AddWithValue("@MsgStatus", MsgStatus);
                cmd.Parameters.AddWithValue("@Account", user.ID);
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