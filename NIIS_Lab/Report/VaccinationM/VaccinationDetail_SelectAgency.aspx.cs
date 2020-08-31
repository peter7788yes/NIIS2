using System;

public partial class Report_VaccinationM_VaccinationDetail_SelectAgency : BasePage
{

    public Report_VaccinationM_VaccinationDetail_SelectAgency()
    {
        base.AddPower("/Report/VaccinationM/VaccinationDetail.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }
}