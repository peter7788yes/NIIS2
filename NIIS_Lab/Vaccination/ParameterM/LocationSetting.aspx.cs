using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Vaccination_ParameterM_LocationSetting : BasePage
{
    public string AgStateJson = "";
    public string tbData = "";
    public MyPowerVM SearchPower =new MyPowerVM("",default(MyPowerEnum));
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public string DefaultOrgName = "";
    public int DefaultOrgID = 0;

    public Vaccination_ParameterM_LocationSetting()
    {
        PowerList = base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx",MyPowerEnum.查詢, MyPowerEnum.列印);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        SearchPower = base.GetPower(PowerList[0]);
        PrintPower = base.GetPower(PowerList[1]);

        //Page.DataBind();

        var user = AuthServer.GetLoginUser();
        UC_OpenSelectSingleOrg.PageUrl = "/System/AccountM/AccountCheck.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;
        DefaultOrgID = user.OrgID;
        DefaultOrgName = user.OrgName;

        UC_OpenSelectAgency.PageUrl = "/Vaccination/ParameterM/LocationSetting.aspx";
        UC_OpenSelectAgency.hasFilter = true;
        UC_OpenSelectAgency.agencyState = 2;
        //UC_OpenSelectAgency.SetID(user.OrgID);
        //UC_OpenSelectAgency.SetName(user.OrgName);

        //UC_OpenSelectAgency1.PageUrl = "/Vaccination/ParameterM/LocationSetting.aspx";
        //UC_OpenSelectAgency2.PageUrl = "/Vaccination/ParameterM/LocationSetting.aspx";
        //UC_OpenSelectAgency2.Suffix = "2";
        //UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/ParameterM/LocationSetting.aspx";
        //UC_OpenSelectSingleOrg1.PageUrl = "/Vaccination/ParameterM/LocationSetting.aspx";
        //UC_OpenSelectSingleOrg1.Suffix = "2";

        if (SystemCode.dict.ContainsKey("ParameterM_LocationSetting_AgencyState"))
        {
            var codes = SystemCode.dict["ParameterM_LocationSetting_AgencyState"];
            codes.RemoveAll(item => item.EnumName.Length == 0);
            if (codes.Count() > 0)
                AgStateJson = JsonConvert.SerializeObject(codes);

        }

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                      { "@OrgID", 0 },
        //                                      { "@AgencyCounty", 0  },
        //                                      { "@AgencyTown", 0 },
        //                                      { "@AgencyVillage", 0 },
        //                                      { "@AgencyState", 0 },
        //                                      { "@AgencyName", "" },
        //                                      { "@pgNow", 1 },
        //                                      { "@pgSize", 10 }
        //                                });

    }

    //public string test()
    //{
    //    return "<a>test</a>";
    //}
}