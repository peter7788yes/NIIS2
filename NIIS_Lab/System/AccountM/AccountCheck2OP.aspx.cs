using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_AccountM_AccountCheck2OP : BasePage
{
    public System_AccountM_AccountCheck2OP()
    {
        base.AddPower("/System/AccountM/AccountCheck2.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        string AccountOrName = GetString("on");
        int CheckState = GetNumber<int>("cs");
        int CheckYear = GetNumber<int>("cy");
        int YearSeason = GetNumber<int>("ys");
        int CheckProgress = GetNumber<int>("cp");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<AccountCheckVM>(), "ConnUser", "dbo.usp_AccountM_xGetAccountCheckListByMany",
                                         new Dictionary<string, object>()
                                         {
                                                { "@OrgName", AccountOrName },
                                                { "@CheckYear",CheckYear },
                                                { "@YearSeason",YearSeason },
                                                { "@CheckProgress", CheckProgress },
                                                { "@pgNow",  pgNow == 0 ? 1 : pgNow },
                                                { "@pgSize", pgSize == 0 ? 10 : pgSize },
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}