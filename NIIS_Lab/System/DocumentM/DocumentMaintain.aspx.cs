using System;
using System.Collections.Generic;

public partial class DocumentManagementM_DocumentMaintain : BasePage
{
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    List<MyPowerVM> list = new List<MyPowerVM>();

    public DocumentManagementM_DocumentMaintain()
    {
        list = base.AddPower("/System/DocumentM/DocumentMaintain.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        AddPower = base.GetPower(list[0]);
        SearchPower = base.GetPower(list[1]);
    }
}