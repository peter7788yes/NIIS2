using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemSettingsM_SystemParameters_SystemParameters : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_SystemSettingsM_SystemParameters_SystemParameters()
    {
        list = base.AddPower("/System/SystemSettingsM/SystemParameters/SystemParameters.aspx", MyPowerEnum.修改, MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);
    }
}