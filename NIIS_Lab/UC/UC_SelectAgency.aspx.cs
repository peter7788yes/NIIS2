using System;
using System.Collections.Generic;
using System.Web;

public partial class UC_SelectAgency : BasePage
{
    public UC_SelectAgency()
    {
        base.BreakCheckPower = true;
    }

    public string PageUrl = "";
    public string EncryptPageUrl = "";
    //public string AddOrgLevel4 = "";
    public bool hasFilter = false;
    public int agencyState = 0;
    public bool HasSearchPower = false;
    public bool HasViewPower = false;
    public string tbData = "";
    public string loginOrgID = "0";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        //Response.Write(Request.HttpMethod);
        if (Request.HttpMethod.Equals("POST"))
        {
            PageUrl = QueryStringEncryptToolS.Decrypt(base.GetString("p"));
            EncryptPageUrl = QueryStringEncryptToolS.Encrypt(PageUrl);

            bool.TryParse(GetString("hf"), out hasFilter);

            //AddOrgLevel4 = GetString("addlv4");

            if (hasFilter) loginOrgID = AuthServer.GetLoginUser().OrgID.ToString();

            agencyState = GetNumber<int>("as");

            HasViewPower = CheckPower(PageUrl, MyPowerEnum.瀏覽);
            if (HasViewPower == false)
            {
                throw new HttpException(404, "Not found");
            }
            HasSearchPower = CheckPower(PageUrl, MyPowerEnum.查詢);

            PageCL CL = new PageCL();
            tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
                                             new Dictionary<string, object>()
                                             {
                                              { "@AgencyCounty", 0 },
                                              { "@AgencyTown", 0 },
                                              { "@AgencyVillage", 0 },
                                              { "@AgencyState", 0 },
                                              { "@AgencyName", "" },
                                              { "@pgNow", 1 },
                                              { "@pgSize", 10 }
                                            });
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

}