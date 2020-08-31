using System;
using System.Collections.Generic;

public partial class CodeM_MedicalCodeOP : BasePage
{
    public CodeM_MedicalCodeOP()
    {
        base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        int OrgID = GetNumber<int>("oid");
        int AgencyCounty = GetNumber<int>("ac");
        int AgencyTown = GetNumber<int>("at");
        int AgencyVillage = GetNumber<int>("av");
        int BusinessState = GetNumber<int>("bs");
        string AgencyName = GetString("an");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_CodeM_xGetAgencyListByMany",
                                         new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID },
                                              { "@AgencyCounty", AgencyCounty  },
                                              { "@AgencyTown", AgencyTown },
                                              { "@AgencyVillage", AgencyVillage },
                                              { "@BusinessState", BusinessState },
                                              { "@AgencyName", AgencyName },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

        
    }
}