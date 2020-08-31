using System;

public partial class Report_VaccinationM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics_Print : BasePage
{
    public Report_VaccinationM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics_Print()
    {
        base.AddPower("/Report/FinishM/ElementarySchoolChildCompletionRateOfChildVaccinationStatistics.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}