using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemSettingsM_SwitchAccountSet_SwitchAccountSet : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_SystemSettingsM_SwitchAccountSet_SwitchAccountSet()
    {
        SearchPower = base.AddPower("/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSet.aspx", MyPowerEnum.查詢);
    }
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
        OrgID = string.Format("{0}", user.OrgID);
    }
}