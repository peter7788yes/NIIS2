using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_SystemSettingsM_SendMessageAccountSet_Test_SendMessageAccountSet : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));

    public System_SystemSettingsM_SendMessageAccountSet_Test_SendMessageAccountSet()
    {
        ModifyPower = base.AddPower("/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(ModifyPower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
    }
}