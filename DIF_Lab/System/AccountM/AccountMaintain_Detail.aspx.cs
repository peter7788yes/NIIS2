using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;
using Newtonsoft.Json;

public partial class System_AccountM_AccountMaintain_Detail : BasePage
{

    public AccountDetailVM VM = default(AccountDetailVM);
    public string ApplyDate = "";
    public int UserID = 0;
    public string nowLogin = "";
    public System_AccountM_AccountMaintain_Detail()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            Dictionary<string, DateTime> dict = new Dictionary<string, DateTime>();
            dict.Add("nowLogin", AuthServer.GetLoginUser().LoginDate);
            nowLogin = JsonConvert.SerializeObject(dict);

            UserID = GetNumber<int>("i");

            DataTable dt = GetDataTable("ConnUser", "dbo.usp_AccountM_xGetAccountDetailByID"
                                  , new Dictionary<string, object>()
                                  {
                                                { "@UserID", UserID }
                                  });
            

            VM = new AccountDetailVM();
            EntityS.FillModel(VM, dt);
            ApplyDate= VM.ApplyDate.ToShortTaiwanDate();

        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}