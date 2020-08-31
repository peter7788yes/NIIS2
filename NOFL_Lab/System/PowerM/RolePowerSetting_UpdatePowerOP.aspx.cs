using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_PowerM_RolePowerSetting_UpdatePowerOP : BasePage
{
    public System_PowerM_RolePowerSetting_UpdatePowerOP()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
       

        List<RolePowerSetting_AddPowerVM> list = new List<RolePowerSetting_AddPowerVM>();
        list = JsonConvert.DeserializeObject<List<RolePowerSetting_AddPowerVM>>(Request.Form["v"]??"");
        foreach (var item in list)
        {
            var x = item.id;
        }

        Response.Write(1);
        Response.End();
    }
}