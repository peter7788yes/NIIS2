using System;
using System.Collections.Generic;
using System.Web;

public partial class UC_SelectAgencyOP : BasePage
{
    public UC_SelectAgencyOP()
    {
        base.BreakCheckPower = true;
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        var PageUrl = QueryStringEncryptToolS.Decrypt(base.GetString("p"));

        bool hasFilter = false;
        bool.TryParse(base.GetString("hf"), out hasFilter);

        var HasViewPower = CheckPower(PageUrl, MyPowerEnum.瀏覽);
        if (HasViewPower == false)
        {
            throw new HttpException(404, "Not found");
        }

        string AgencyName = GetString("an");
        //string AddOrgLevel4 = GetString("addlv4");
        int AgencyCounty = GetNumber<int>("ac");
        int AgencyTown = GetNumber<int>("at");
        int AgencyVillage = GetNumber<int>("av");
        int AgencyState = GetNumber<int>("as");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        PageCL CL = new PageCL();
        string tbData;
        if (hasFilter)
        {
            tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByOrgID",
                                         new Dictionary<string, object>()
                                         {
                                              { "@AgencyCounty", AgencyCounty },
                                              { "@AgencyTown", AgencyTown },
                                              { "@AgencyVillage", AgencyVillage },
                                              { "@AgencyState", AgencyState },
                                              { "@AgencyName", AgencyName },
                                              //{ "@AddOrgLevel4", AddOrgLevel4 },
                                              { "@OrgID", AuthServer.GetLoginUser().OrgID.ToString() },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        }
        else
        {
            tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
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
        }
        
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}