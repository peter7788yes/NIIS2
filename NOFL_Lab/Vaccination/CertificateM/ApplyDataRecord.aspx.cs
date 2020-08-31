using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_CertificateM_ApplyDataRecord : BasePage
{

    public string AgeString = "";
    public string uJson = "''";
    public int CaseUserID = 0;

    public Vaccination_CertificateM_ApplyDataRecord()
    {
        base.AddPower("/Vaccination/CertificateM/PrintCertificate.aspx", MyPowerEnum.新增);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request.Form["i"], out CaseUserID);

        if (CaseUserID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetCaseUserByID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }


        CaseUserVM VM = new CaseUserVM();
        EntityS.FillModel(VM, ds.Tables[0]);
        uJson = JsonConvert.SerializeObject(VM);

        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        
    }
}