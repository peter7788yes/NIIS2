using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_ChooseCate : BasePage
{
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

            if (success==false || CaseUserID == 0 || RecordDataID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


            string SystemRecordVaccineCode = "";
            SystemRecordVaccineCode = Request.Form["r"] ?? "";

            bool HasInsertSystemRecordVaccine = false;
            int Chk = 0;
       

            if (CaseUserID > 0 || SystemRecordVaccineCode.Equals("") == false)
            {
                var user = AuthServer.GetLoginUser();
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddRecordData", sc))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                        cmd.Parameters.AddWithValue("@SystemRecordVaccineCode", SystemRecordVaccineCode);
                        cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                        cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                        SqlParameter sp1 = cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                        sp1.Direction = ParameterDirection.Output;
                        SqlParameter sp2 = cmd.Parameters.AddWithValue("@HasInsertSystemRecordVaccine", HasInsertSystemRecordVaccine);
                        sp2.Direction = ParameterDirection.Output;
                        SqlParameter sp3 = cmd.Parameters.AddWithValue("@Chk", Chk);
                        sp3.Direction = ParameterDirection.Output;

                        sc.Open();
                        cmd.ExecuteNonQuery();

                        RecordDataID = (int)sp1.Value;
                        HasInsertSystemRecordVaccine = (bool)sp2.Value;
                        Chk = (int)sp3.Value;
                    }

                    if (HasInsertSystemRecordVaccine == true)
                        SystemRecordVaccine.Update();
                   

                    if(Chk<1 || RecordDataID ==0)
                    {
                        string script = "<script>alert('資料取得失敗');window.close();</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                        return;
                    }

                }
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}