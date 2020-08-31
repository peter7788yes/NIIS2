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

public partial class System_PowerM_RolePowerSetting_UpdatePower : BasePage
{
    public string MyTreeData = "";
    int ID = 0;
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public string orgLevelEnumString { get; set; }
    
    public System_PowerM_RolePowerSetting_UpdatePower()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
        MyTreeData = GetMenu();

        int.TryParse(Request["i"], out ID);

        if (ID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
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

        RoleName = VM.RoleName;
        RoleDescription = VM.RoleDescription;
        

        switch (VM.OrgLevel)
       {
                case 1:
                    orgLevelEnumString="中央";
                    break;
                case 2:
                    orgLevelEnumString="區管中心";
                    break;
                case 3:
                    orgLevelEnumString="局";
                    break;
                case 4:
                    orgLevelEnumString = "所";
                    break;

     }
       
    }


    private string GetMenu()
    {
        List<ModuleMenuVM> list = new List<ModuleMenuVM>();
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xGetModuleMenu", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", 1);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        EntityS.FillModel(list, dt);
        return JsonConvert.SerializeObject(list);
    }

}