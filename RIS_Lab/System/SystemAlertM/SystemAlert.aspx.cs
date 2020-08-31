using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemAlertM_SystemAlert : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_SystemAlertM_SystemAlert()
    {
        SearchPower = base.AddPower("/System/SystemAlertM/SystemAlert.aspx", MyPowerEnum.查詢);
    }
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    public string AlertType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
        OrgID = string.Format("{0}", user.OrgID);

        if (SystemCode.dict.ContainsKey("SystemAlertM_AlertType"))
            AlertType = JsonConvert.SerializeObject(SystemCode.dict["SystemAlertM_AlertType"].Where(item => item.EnumName != null));
    }
}