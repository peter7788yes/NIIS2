using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_PowerM_RolePowerSetting_AddPower : BasePage
{

    public string MyTreeData = "";

    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public string orgLevelEnumString { get; set; }

    public System_PowerM_RolePowerSetting_AddPower()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (Page.PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {
                System_PowerM_RolePowerSetting_Add page = (System_PowerM_RolePowerSetting_Add)PreviousPage;
                RoleName = page.RoleName;
                RoleDescription = page.RoleDescription;
                OrgLevelEnum orgLevelEnum = page.orgLevelEnum;
                orgLevelEnumString = Enum.GetName(typeof(OrgLevelEnum), page.orgLevelEnum);

                if (RoleName.Length==0)
                {
                    Response.Redirect("~/System/PowerM/RolePowerSetting_Add.aspx");
                }
                else
                {
                    MyTreeData = GetMenu();
                }

            }
        }
        else
        {
            Response.Redirect("~/System/PowerM/RolePowerSetting_Add.aspx");
        }
           
    }

    private HttpCookie SetCookie(string name, string value, DateTime Expires)
    {
        //產生一個Cookie
        HttpCookie cookie = new HttpCookie("RoleName");
        //設定單值
        cookie.Value = Server.UrlEncode(RoleName);
        //設定過期日
        cookie.Expires = DateTime.Now.AddDays(1);

        return cookie;
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