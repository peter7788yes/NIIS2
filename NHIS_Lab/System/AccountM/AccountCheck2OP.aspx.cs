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

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        string AccountOrName = GetString("an");
        int CheckState = GetNumber<int>("cs");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");




        DataTable dt = GetDataTable("ConnUser", "dbo.usp_AccountM_xGetAccountCheckListByMany"
                                        , new Dictionary<string, object>()
                                        {
                                                { "@OrgName", AccountOrName },
                                                { "@CheckYear", 0 },
                                                { "@YearSeason", 0 },
                                                { "@CheckProgress", 1 },
                                                { "@pgNow",  pgNow == 0 ? 1 : pgNow },
                                                { "@pgSize", pgSize == 0 ? 10 : pgSize },
                                        });


       

        List<AccountCheckVM> list = new List<AccountCheckVM>();
        PageVM rtn = new PageVM();
        EntityS.FillModel(list, dt);
        rtn.message = list;


        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}