using System;

public partial class Report_VaccinationM_AbnormalVaccinationDetail_Print : BasePage
{
    public Report_VaccinationM_AbnormalVaccinationDetail_Print()
    {
        base.AddPower("/Report/VaccinationM/AbnormalVaccinationDetail.aspx", MyPowerEnum.查詢);
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
            //if (Page.PreviousPage != null)
            //{
            //    if (PreviousPage.IsCrossPagePostBack == true)
            //    {
            //        Report_VaccinationM_AbnormalVaccinationDetail page = (Report_VaccinationM_AbnormalVaccinationDetail)PreviousPage;



            //    }
            //}
            //else if (this.IsPostBack == true)
            //{

            //}
            //else
            //{
            //    Response.Redirect("~/Report/VaccinationM/AbnormalVaccinationDetail.aspx");
            //}
        }
}