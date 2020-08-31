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

public partial class System_PowerM_RolePowerSetting_Update :BasePage
{
   
    int ID=0;

    public System_PowerM_RolePowerSetting_Update()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["i"], out ID);

        if (ID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
        }

        if (this.IsPostBack==false)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetRoleByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            RolePowerSettingVM VM = new RolePowerSettingVM();
            EntityS.FillModel(VM, dt);

            tbName.Text = VM.RoleName;
            tbDesp.Text = VM.RoleDescription;
            switch (VM.OrgLevel)
            {
                case 1:
                    rb1.Checked = true;
                    break;
                case 2:
                    rb1.Checked = true;
                    break;
                case 3:
                    rb1.Checked = true;
                    break;
                case 4:
                    rb1.Checked = true;
                    break;

            }
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        OrgLevelEnum OrgLevelEnum = default(OrgLevelEnum);
        RadioButton selectLevel = null;
        selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (selectLevel != null)
        {
            Enum.TryParse(selectLevel.Text, out OrgLevelEnum);
        }

        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xUpdateRole", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RoleName", tbName.Text.Trim());
                cmd.Parameters.AddWithValue("@RoleDescription", tbDesp.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgLevel", OrgLevelEnum);
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
            script = "<script>alert('儲存成功');location.href = '/System/PowerM/RolePowerSetting.aspx';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
}