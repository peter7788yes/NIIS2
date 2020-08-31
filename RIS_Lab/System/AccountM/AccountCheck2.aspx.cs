using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class System_AccountM_AccountCheck2 : BasePage
{
    public string MyRoleData = "[]";
    public string MyEnableData = "[]";
    public string MyCheckData = "[]";
    public string MyLogoutData = "[]";
    public string MyCheckResultData = "[]";
    public string MyYearSeasonData = "[]";
    public string OrgName = "";
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";

    public System_AccountM_AccountCheck2()
    {
        PowerList=base.AddPower("/System/AccountM/AccountCheck.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        OrgName = AuthServer.GetLoginUser().OrgName;

        MyRoleData = JsonConvert.SerializeObject(SystemRole.list.Where(item => item.RoleName != null));

        if (SystemCode.dict.ContainsKey("AccountM_EnableState"))
            MyEnableData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_EnableState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_CheckState"))
            MyCheckData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_CheckState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_LogoutPeriod"))
            MyLogoutData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_LogoutPeriod"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_AccountCheck2_CheckResult"))
            MyCheckResultData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_AccountCheck2_CheckResult"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_AccountCheck2_YearSeason"))
            MyYearSeasonData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_AccountCheck2_YearSeason"].Where(item => item.EnumName != null));

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<AccountCheckVM>(), "ConnUser", "dbo.usp_AccountM_xGetAccountCheckListByMany",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                        { "@OrgName", "" },
        //                                        { "@CheckYear", 0 },
        //                                        { "@YearSeason", 0 },
        //                                        { "@CheckProgress", 1 },
        //                                        { "@pgNow",  1 },
        //                                        { "@pgSize", 10},
        //                                });
    }
}