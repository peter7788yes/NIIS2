using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_PowerM_RolePowerSetting_Update :BasePage
{
    public new int ID=0;

    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();

    new bool IsValid = true;

    public System_PowerM_RolePowerSetting_Update()
    {
        PowerList = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);

    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        ViewPower = base.GetPower(PowerList[0]);
        UpdatePower = base.GetPower(PowerList[1]);

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

            ID = GetNumber<int>("i");

            if (this.IsPostBack == false)
            {

               
                if (ID == 0)
                {
                    IsValid = false;
                    string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                    return;
                }

                DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_PowerM_xGetRoleByID"
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@ID", ID }
                                            });
              
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
        if (IsValid == false)
            return;

        int RoleLevel = 0;
        RadioButton selectLevel = null;
        selectLevel = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
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
        string RoleName = PureString(tbName.Text);
        string RoleDescription = PureString(tbDesp.Text);

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_PowerM_xUpdateRole"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ID", ID },
                                                    { "@RoleName", RoleName },
                                                    { "@RoleDescription", RoleDescription },
                                                    { "@RoleLevel", RoleLevel }
                                               
                                        });

        Chk = (int)OutDict["@Chk"];

       

        string script = "";
        if (Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');location.href='/System/PowerM/RolePowerSetting.aspx#" + HttpUtility.HtmlDecode(GetString("hash") ?? "").TrimStart('#') + "';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}