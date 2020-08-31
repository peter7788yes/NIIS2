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

public partial class Vaccination_CertificateM_SignSetting : BasePage
{

    UserVM user = new UserVM();
    public Vaccination_CertificateM_SignSetting()
    {
        base.AddPower("/Vaccination/CertificateM/SignSetting.aspx",  MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";


        user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetCertificateSignByOrgID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }


            if(dt.Rows.Count>0)
            {
                CertificateSignVM VM = new CertificateSignVM();
                EntityS.FillModel(VM,dt);
                tbP.Text = VM.PhysicianSignature;
                tbE.Text = VM.EnglishFullTitle;
                tbC.Text = VM.ChineseFullTitle;
                tbD.Text = VM.UnitDirector;
                tbS.Text = VM.UnitStamp;
                tbLocation.Text = VM.OrgName;
                hfLocationID.Value = VM.OrgID.ToString();
            }
        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        int OrgID = 0;
        int.TryParse(hfLocationID.Value, out OrgID);


        if (OrgID == 0)
        {
            string myScript = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", myScript, false);
            return;
        }

        int Chk = 0;
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddOrUpdateCertificateSign", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@PhysicianSignature", tbP.Text.Trim());
                cmd.Parameters.AddWithValue("@UnitDirector", tbD.Text.Trim());
                cmd.Parameters.AddWithValue("@UnitStamp", tbS.Text.Trim());
                cmd.Parameters.AddWithValue("@EnglishFullTitle", tbE.Text.Trim());
                cmd.Parameters.AddWithValue("@ChineseFullTitle", tbC.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}