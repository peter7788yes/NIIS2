using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_DocumentManagementM_DocumentMaintainOP : BasePage
{

    public System_DocumentManagementM_DocumentMaintainOP()
    {
        base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //string sss = QueryStringEncryptToolS.EncryptQueryString("1q2w3e4r", "t00","hyweb");
        //Response.Write(sss);
        //sss = QueryStringEncryptToolS.DecryptQueryString(HttpUtility.UrlDecode(sss), "t00", "hyweb");
        //Response.Write("<br/>");
        //Response.Write(sss);
        //Response.End();

        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        string DocTitle = GetString("d");

        PublishStateEnum publishState;
        Enum.TryParse(GetString("p"), out publishState);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_DocumentM_xGetDocList"
                                          , new Dictionary<string, object>()
                                          {
                                                    { "@DocTitle", DocTitle },
                                                    { "@PublishState", publishState },
                                                    { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                                    { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                          });


        List<DocumentInfoVM> InfoList = new List<DocumentInfoVM>();
        //List<DocumentFileInfoVM> fileList = new List<DocumentFileInfoVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(InfoList, ds.Tables[0]);
        //EntityS.FillModel(fileList, ds.Tables[1]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        //foreach(var infoItem in InfoList)
        //{

        //    IEnumerable<DocumentFileInfoVM> files = fileList.Where(item => item.DocumentInfoID == infoItem.ID);

        //    //var info = InfoList.Find(item => item.ID == infoItem.ID);
        //    //info.FileList.AddRange(files);
        //    infoItem.FileList.AddRange(files);

        //}
     

        rtn.message = InfoList;
        //rtn.pgNow = pgNow;
        //rtn.pgSize = pgSize;
        //rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}