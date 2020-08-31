using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WayneEntity;

public partial class System_AccountM_AccountCheckOP : BasePage
{
    public System_AccountM_AccountCheckOP()
    {
        base.AddPower("/System/AccountM/AccountCheck.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        string AccountOrName = GetString("an");
        string OrgName = GetString("on");
        int CheckState = GetNumber<int>("cs");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        DataSet ds = MSDB.GetDataSet("ConnUser", "dbo.usp_AccountM_xGetUserListByMany"
                                          , new Dictionary<string, object>()
                                          {
                                                { "@AccountOrName", AccountOrName },
                                                { "@OrgName", OrgName },
                                                { "@CheckState", CheckState },
                                                { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                                { "@pgSize", pgSize == 0 ? 10 : pgSize },
                                          });

        List<UserInfoVM> list = new List<UserInfoVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        var IDs = list.Select(item => item.ID);
        string IDsString = string.Join(",", IDs);

        DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_AccountM_xGetUserRolesByIDs"
                                       , new Dictionary<string, object>()
                                       {
                                                { "@IDs", IDsString.Length == 0 ? "0" : IDsString }
                                       });

        List<UserRoleVM> VMs = new List<UserRoleVM>();
        EntityS.FillModel(VMs, dt);

        foreach (var item in list)
        {
            foreach (var VM in VMs)
            {
                if(item.ID==VM.UserID)
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