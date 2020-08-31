using System;
using System.Collections.Generic;

public partial class DocumentManagementM_DocumentMaintain : BasePage
{
    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();
    public DocumentManagementM_DocumentMaintain()
    {
        list = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AddPower = base.GetPower(list[0]);
        SearchPower = base.GetPower(list[1]);

        base.AllowHttpMethod("GET");
        base.DisableTop(false);

    }
}