using System;
using System.Web.UI;

public partial class ApplyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {
                Login page = (Login)PreviousPage;
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