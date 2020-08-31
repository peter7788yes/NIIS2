using System;
using System.Collections.Generic;

public partial class System_DocumentViewDownloadOP : BasePage
{
    public System_DocumentViewDownloadOP()
    {
        base.AddPower("/System/DocumentM/DocumentViewDownload.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        string DocTitle = GetString("i");

        PublishStateEnum publishState;
        Enum.TryParse(GetString("p"), out publishState);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");


        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<DocumentInfoVM>(), "ConnDB", "dbo.usp_DocumentM_xGetDocViewList",
                                         new Dictionary<string, object>()
                                         {
                                                  { "@pgNow",   pgNow == 0 ? 1 : pgNow },
                                                  { "@pgSize",  pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

    }
}