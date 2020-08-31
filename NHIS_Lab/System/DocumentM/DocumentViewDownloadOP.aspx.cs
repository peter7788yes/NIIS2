using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_DocumentViewDownloadOP : BasePage
{

    public System_DocumentViewDownloadOP()
    {
        base.AddPower("/System/DocumentM/DocumentViewDownload.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("POST");
        base.DisableTop(false);


        string DocTitle = GetString("i");

        PublishStateEnum publishState;
        Enum.TryParse(GetString("p"), out publishState);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");


        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_DocumentM_xGetDocViewList"
                                          , new Dictionary<string, object>()
                                          {
                                                    { "@pgNow",   pgNow == 0 ? 1 : pgNow },
                                                    { "@pgSize",  pgSize == 0 ? 10 : pgSize }
                                          }); 

        List<DocumentInfoVM> list = new List<DocumentInfoVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}