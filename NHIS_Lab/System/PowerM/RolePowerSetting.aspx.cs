using System;

public partial class RolePowerSetting : BasePage
{
    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));

    public RolePowerSetting()
    {
        AddPower = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AddPower = base.GetPower(AddPower);

        base.AllowHttpMethod("GET");
        base.DisableTop(true);

      
  }
}