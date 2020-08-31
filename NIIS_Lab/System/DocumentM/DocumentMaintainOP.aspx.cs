using System;
using System.Collections.Generic;

public partial class System_DocumentManagementM_DocumentMaintainOP : BasePage
{
    public System_DocumentManagementM_DocumentMaintainOP()
    {
        base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        string DocTitle = GetString("d");

        PublishStateEnum publishState;
        Enum.TryParse(GetString("p"), out publishState);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");


        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<DocumentInfoVM>(), "ConnDB", "dbo.usp_DocumentM_xGetDocList",
                                         new Dictionary<string, object>()
                                         {
                                                    { "@DocTitle", DocTitle },
                                                    { "@PublishState", publishState },
                                                    { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                                    { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}