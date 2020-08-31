using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_AccountM_AccountMaintain_DetailOP : BasePage
{
    public System_AccountM_AccountMaintain_DetailOP()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int UserID = GetNumber<int>("i");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        DataSet ds = MSDB.GetDataSet("ConnUser", "dbo.usp_AccountM_xGetUserLoginListByUserID"
                                  , new Dictionary<string, object>()
                                  {
                                          { "@UserID", UserID },
                                          { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                          { "@pgSize",  pgSize == 0 ? 10 : pgSize }
                                  });

        List<UserLoginVM> list = new List<UserLoginVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}