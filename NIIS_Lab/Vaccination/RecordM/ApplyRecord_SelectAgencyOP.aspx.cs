using System;
using System.Collections.Generic;

public partial class VaccinationM_ApplyRecord_SelectAgencyOP : BasePage
{
    public VaccinationM_ApplyRecord_SelectAgencyOP()
    {
        base.AddPower("/Vaccination/ParameterM/RegisterData.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int AgencyCounty = GetNumber<int>("ac");
        int AgencyTown = GetNumber<int>("at");
        int AgencyVillage = GetNumber<int>("av");
        int AgencyState = GetNumber<int>("as");
        string AgencyName = GetString("an");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
                                         new Dictionary<string, object>()
                                         {
                                              { "@AgencyCounty", AgencyCounty },
                                              { "@AgencyTown", AgencyTown },
                                              { "@AgencyVillage", AgencyVillage },
                                              { "@AgencyState", AgencyState },
                                              { "@AgencyName", AgencyName },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}