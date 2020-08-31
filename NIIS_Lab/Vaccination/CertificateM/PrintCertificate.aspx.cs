using System;
using ASP;

public partial class Vaccination_CertificateM_PrintCertificate : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        ASP.masterpage_custom_decoratedmasterpage_master masterPage = this.Master as ASP.masterpage_custom_decoratedmasterpage_master;
        masterPage.AutoIncludeCSS = false;
        masterPage.AutoIncludeJS = false;
    }
}