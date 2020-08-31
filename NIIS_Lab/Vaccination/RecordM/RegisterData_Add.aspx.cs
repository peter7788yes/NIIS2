using System;

public partial class VaccinationM_RegisterData_Add : BasePage
{
    public VaccinationM_RegisterData_Add()
    {
        base.AddPower("/Vaccination/ParameterM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
    }
}