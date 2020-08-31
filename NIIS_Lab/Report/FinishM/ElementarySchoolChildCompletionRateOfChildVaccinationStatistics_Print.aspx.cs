using System;

public partial class Report_FinishM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics_Print : BasePage
{
    public Report_FinishM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics_Print()
    {
        base.AddPower("/Report/FinishM/VaccinationM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
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