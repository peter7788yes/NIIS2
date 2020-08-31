using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemSettingsM_SendMessageAccountSet_SendMessageAccountSet : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_SystemSettingsM_SendMessageAccountSet_SendMessageAccountSet()
    {
        list = base.AddPower("/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx", MyPowerEnum.修改,MyPowerEnum.查詢);
    }
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    public bool Check { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
        OrgID = string.Format("{0}", user.OrgID);

        int SelectOrgID=0;

        int.TryParse(Request.QueryString["ID"],out SelectOrgID);

        if (SelectOrgID == 0)
        {
            SelectOrgID = user.OrgID;
            OrgName = SystemOrg.GetName(SelectOrgID);
        }
        else
        {
            OrgName = SystemOrg.GetName(SelectOrgID);
        }

        if (user.OrgID == SelectOrgID)
        {
            Check = true;
        }
        else
        {
            Check = false;
        }
    }
}