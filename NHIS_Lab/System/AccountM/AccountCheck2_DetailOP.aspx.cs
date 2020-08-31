using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WayneEntity;

public partial class System_AccountM_AccountCheck2_DetailOP : BasePage
{
    
    public System_AccountM_AccountCheck2_DetailOP()
    {
        base.AddPower("/System/AccountM/AccountCheck2.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int AccountCheckID = GetNumber<int>("i");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");


        DataTable dt = GetDataTable("ConnUser", "dbo.usp_AccountM_xGetAccountCheckUserList"
                                         , new Dictionary<string, object>()
                                         {
                                                { "@AccountCheckID", AccountCheckID },
                                                { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                                { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        

        List<AccountCheckVM> list = new List<AccountCheckVM>();
        PageVM rtn = new PageVM();
        EntityS.FillModel(list, dt);
        rtn.message = list;

        var IDs = list.Select(item => item.ID);
        string IDsString = string.Join(",", IDs);


        dt = GetDataTable("ConnUser", "dbo.usp_AccountM_xGetUserRolesByIDs"
                                         , new Dictionary<string, object>()
                                         {
                                                { "@IDs", IDsString.Equals("") ? "0": IDsString}
                                        });


        List<UserRoleVM> VMs = new List<UserRoleVM>();
        EntityS.FillModel(VMs, dt);

        foreach (var item in list)
        {
            foreach (var VM in VMs)
            {
                if (item.ID == VM.UserID)
                {
                    item.RoleIdList.Add(VM.RoleID);
                }
            }
        }


        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}