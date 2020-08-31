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

public partial class System_PowerM_RolePowerSettingOP : BasePage
{

    public System_PowerM_RolePowerSettingOP()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");
        int RoleCateID = GetNumber<int>("rc");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<RolePowerSettingVM>(), "ConnUser", "dbo.usp_PowerM_xGetRoleListByRoleCateID",
                                         new Dictionary<string, object>()
                                         {
                                                    { "@RoleCateID", RoleCateID },
                                                    { "@pgNow", pgNow == 0 ? 1 : pgNow},
                                                    { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

    }
}