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
        list = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        AddPower = base.GetPower(list[0]);
        SearchPower = base.GetPower(list[1]);

        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<DocumentInfoVM>(), "ConnDB", "dbo.usp_DocumentM_xGetDocList",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                            { "@DocTitle", "" },
        //                                            { "@PublishState", 0 },
        //                                            { "@pgNow", 1 },
        //                                            { "@pgSize", 10 }
        //                                });

    }
}