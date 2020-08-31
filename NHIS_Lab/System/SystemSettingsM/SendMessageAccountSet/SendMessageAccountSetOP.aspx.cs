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

public partial class System_SystemSettingsM_SendMessageAccountSet_SendMessageAccountSetOP : BasePage
{
    public System_SystemSettingsM_SendMessageAccountSet_SendMessageAccountSetOP()
    {
        base.AddPower("/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int OrgID = 0;

        int.TryParse(Request.Form["OrgID"], out OrgID);

        if (OrgID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
        }
        EncryptDES enc =new EncryptDES();

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

        AnyDataVM rtn = new AnyDataVM();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Rows[0]["MsgPassWord"] = enc.ToDecryptDES(ds.Tables[0].Rows[0]["MsgPassWord"].ToString(), Convert.ToString(WebConfigurationManager.AppSettings["encryptKey"]));
        }
        rtn.message = ds.Tables[0];
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}