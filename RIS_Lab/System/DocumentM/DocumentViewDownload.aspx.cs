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

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<DocumentInfoVM>(), "ConnDB", "dbo.usp_DocumentM_xGetDocViewList",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                          { "@pgNow",  1 },
        //                                          { "@pgSize",  10 }
        //                                });

    }
}