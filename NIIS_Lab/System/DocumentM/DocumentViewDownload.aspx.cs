using System;
using System.Collections.Generic;

public partial class DocumentManagementM_DocumentViewDownload : BasePage
{
    List<MyPowerVM> list = new List<MyPowerVM>();
    public string tbData = "";

    public DocumentManagementM_DocumentViewDownload()
    {
       
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
     
    }
}