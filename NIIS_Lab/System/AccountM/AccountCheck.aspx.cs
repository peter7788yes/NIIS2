using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class System_AccountM_AccountCheck : BasePage
{
    public string MyRoleData = "[]";
    public string MyEnableData = "[]";
    public string MyCheckData = "[]";
    public string MyLogoutData = "[]";
    public string DefaultOrgName = "";
    public int DefaultOrgID = 0;

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();

    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public System_AccountM_AccountCheck()
    {
        PowerList=base.AddPower("/System/AccountM/AccountCheck.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
       
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        var user = AuthServer.GetLoginUser();
        UC_OpenSelectSingleOrg.PageUrl = "/System/AccountM/AccountCheck.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;
        DefaultOrgID = user.OrgID;
        DefaultOrgName = user.OrgName;

        MyRoleData = JsonConvert.SerializeObject(SystemRole.list.Where(item => item.RoleName != null));

        if (SystemCode.dict.ContainsKey("AccountM_EnableState"))
            MyEnableData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_EnableState"].Where(item => item.EnumName != null));
        
        if (SystemCode.dict.ContainsKey("AccountM_CheckState"))
            MyCheckData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_CheckState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_LogoutPeriod"))
            MyLogoutData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_LogoutPeriod"].Where(item => item.EnumName != null));
    }
}