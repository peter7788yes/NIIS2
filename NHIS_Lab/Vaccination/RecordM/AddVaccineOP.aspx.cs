using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_AddVaccineOP : BasePage
{

    public Vaccination_RecordM_AddVaccineOP()
    {
        base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.瀏覽);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        var user = AuthServer.GetLoginUser();


        int CaseUserID = 0;
        string SystemRecordVaccineCode = "";
        int RecordDataID = 0;
        bool HasInsertSystemRecordVaccine = false;
        int Chk = 0;
        int.TryParse(Request.Form["c"], out CaseUserID);
        SystemRecordVaccineCode = Request.Form["r"] ?? "";

        if (CaseUserID > 0 || SystemRecordVaccineCode.Equals("") == false)
        {
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
                OPVM VM = new OPVM();
                VM.message = RecordDataID.ToString();
                VM.chk = Chk;

                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
                Response.End();

            }
        }
    }
}