using Newtonsoft.Json;
using System;
using System.Linq;

public partial class System_CodeM_MedicalCode : BasePage
{
    public string BgStateJson = "[]";
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    public System_CodeM_MedicalCode()
    {
        SearchPower = base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("ParameterM_LocationSetting_BusinessState"))
        {
            var codes = SystemCode.dict["ParameterM_LocationSetting_BusinessState"];
            if(codes.Count()>0)
                BgStateJson = JsonConvert.SerializeObject(codes);

        }


        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_CodeM_xGetAgencyListByMany",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                      { "@OrgID", 0 },
        //                                      { "@AgencyCounty", 0  },
        //                                      { "@AgencyTown", 0 },
        //                                      { "@AgencyVillage", 0 },
        //                                      { "@BusinessState", 0 },
        //                                      { "@AgencyName", "" },
        //                                      { "@pgNow", 1 },
        //                                      { "@pgSize", 10 }
        //                                });
    }
}