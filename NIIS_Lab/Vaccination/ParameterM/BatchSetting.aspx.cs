using System;

public partial class Vaccination_ParameterM_BatchSetting : BasePage
{
    public string tbData1 { get; set; }
    public string tbData2 { get; set; }

    public Vaccination_ParameterM_BatchSetting()
    {
        base.powerLogicType = PowerLogicType.AND;
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx",MyPowerEnum.查詢,MyPowerEnum.新增,MyPowerEnum.修改,MyPowerEnum.刪除);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/ParameterM/BatchSetting.aspx";
        UC_OpenSelectSingleOrg.callback = "onSelectSingleOrg();";
    }
}