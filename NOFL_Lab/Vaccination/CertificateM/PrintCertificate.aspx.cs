﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_CertificateM_PrintCertificate : BasePage
{

    public Vaccination_CertificateM_PrintCertificate()
    {
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}