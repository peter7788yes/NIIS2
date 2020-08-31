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
   
    public int ID=0;

    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public System_PowerM_RolePowerSetting_Update()
    {
        PowerList = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewPower = base.GetPower(PowerList[0]);
        UpdatePower = base.GetPower(PowerList[1]);

        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

       

        if (Request.HttpMethod.Equals("POST"))
        {
          

            if (UpdatePower.HasPower == false)
            {
                tbName.Enabled = false;
                tbDesp.Enabled = false;
                rb1.Enabled = false;
                rb2.Enabled = false;
                rb3.Enabled = false;
                rb4.Enabled = false;
            }

            int.TryParse(Request.Form["i"], out ID);

            if (this.IsPostBack == false)
            {

               
                if (ID == 0)
                {
                    string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                    return;
                }

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
                switch (VM.RoleLevel)
                {
                    case 1:
                        rb1.Checked = true;
                        break;
                    case 2:
                        rb2.Checked = true;
                        break;
                    case 3:
                        rb3.Checked = true;
                        break;
                    case 4:
                        rb4.Checked = true;
                        break;

                }
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int RoleLevel = 0;
        RadioButton selectLevel = null;
        selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        switch (selectLevel.ID)
        {
            case "rb1":
                RoleLevel = 1;
                break;
            case "rb2":
                RoleLevel = 2;
                break;
            case "rb3":
                RoleLevel = 3;
                break;
            case "rb4":
                RoleLevel = 4;
                break;

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
                cmd.Parameters.AddWithValue("@RoleLevel", RoleLevel);
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
            script = "<script>alert('儲存成功');location.href = '/System/PowerM/RolePowerSetting.aspx';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}