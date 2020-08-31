using System;

public partial class RolePowerSetting : BasePage
{
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    public RolePowerSetting()
    {
        AddPower = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        AddPower = base.GetPower(AddPower);

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<RolePowerSettingVM>(), "ConnUser", "dbo.usp_PowerM_xGetRoleListByRoleCateID",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                            { "@RoleCateID", 0 },
        //                                            { "@pgNow", 1},
        //                                            { "@pgSize", 10 }
        //                                });
     
    }
}