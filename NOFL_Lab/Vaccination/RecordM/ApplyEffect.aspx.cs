using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_RecordM_ApplyEffect : BasePage
{

    //public string ApplyEffectJson = "";

    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (Request.HttpMethod.Equals("POST"))
        {

            int.TryParse(Request.Form["c"], out CaseUserID);
            int.TryParse(Request.Form["i"], out RecordDataID);
            VaccineCode = Request.Form["r"] ?? "";
            AppointmentDate = Request.Form["a"] ?? "";
            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            //AppointmentDate = date.ToShortTaiwanDate();

            if (success==false || CaseUserID == 0 || RecordDataID == 0 )
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }



            //DataTable dt = new DataTable();

            //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetApplyEffectByRecordDataID", sc))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            sc.Open();
            //            da.Fill(dt);
            //        }
            //    }
            //}


            //List<ApplyEffectVM> list = new List<ApplyEffectVM>();
            //EntityS.FillModel(list, dt);

            //ApplyEffectJson = JsonConvert.SerializeObject(list);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}