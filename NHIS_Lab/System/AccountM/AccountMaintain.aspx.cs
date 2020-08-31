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

public partial class System_AccountM_AccountMaintain : BasePage
{
    public string MyRoleData = "[]";
    public string MyEnableData = "[]";
    public string MyCheckData = "[]";
    public string MyLogoutData = "[]";

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();

    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));


    public System_AccountM_AccountMaintain()
    {
        PowerList=base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        MyRoleData = JsonConvert.SerializeObject(SystemRole.list.Where(item => item.RoleName != null));

        if (SystemCode.dict.ContainsKey("AccountM_EnableState"))
            MyEnableData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_EnableState"].Where(item => item.EnumName != null));

        
        if (SystemCode.dict.ContainsKey("AccountM_CheckState"))
            MyCheckData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_CheckState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_LogoutPeriod"))
            MyLogoutData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_LogoutPeriod"].Where(item => item.EnumName != null));
    }
}