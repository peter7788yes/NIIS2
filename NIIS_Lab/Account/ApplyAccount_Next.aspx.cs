using System;
using System.Web.UI;

public partial class ApplyAccount_Next : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {
                ApplyAccount page = (ApplyAccount)PreviousPage;
            }

        }
        else if (this.IsPostBack == true)
        {
           
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

}