using System;
using System.Collections.Generic;

public partial class DocumentManagementM_DocumentViewDownload : BasePage
{

    List<MyPowerVM> list = new List<MyPowerVM>();

    public DocumentManagementM_DocumentViewDownload()
    {
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("GET");
        base.DisableTop(false);
      
    }
}