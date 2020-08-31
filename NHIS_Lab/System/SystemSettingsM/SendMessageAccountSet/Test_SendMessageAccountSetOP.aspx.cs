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

public partial class System_SystemSettingsM_SendMessageAccountSet_Test_SendMessageAccountSetOP : BasePage
{
    public System_SystemSettingsM_SendMessageAccountSet_Test_SendMessageAccountSetOP()
    {
        base.AddPower("/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int OrgID = 0;
        string MsgAccount = "";
        string MsgPassWord = "";
        int Success = 0;
        int DataError = 0;
        int CheckAccount = 0;
        int CheckPassWord = 0;

        int.TryParse(Request.Form["OrgID"], out OrgID);
        MsgAccount = Request.Form["MsgAccount"];
        MsgPassWord = Request.Form["MsgPassWord"];

        if (OrgID == 0)
        {
            DataError = 1;
            return;            
        }
        EncryptDES enc = new EncryptDES();

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_StMsgSet_xGetStMsgSetData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            if (MsgAccount == dt.Rows[0]["MsgAccount"].ToString())
            {
                CheckAccount = 1;
            }
            if (MsgPassWord == enc.ToDecryptDES(dt.Rows[0]["MsgPassWord"].ToString(), Convert.ToString(WebConfigurationManager.AppSettings["encryptKey"])))
            {
                CheckPassWord = 1;
            }
        }
        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("DataError", DataError);
        dict.Add("CheckAccount", CheckAccount);
        dict.Add("CheckPassWord", CheckPassWord);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}