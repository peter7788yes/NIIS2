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
        //base.ActionPowerDict.Add("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.新增);
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
        string DocTitle;
        PublishStateEnum publishState;
        int pgNow;
        int pgSize;
        
        DocTitle = Request.Form["d"] ?? "";
        Enum.TryParse(Request.Form["p"], out publishState);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_DocumentM_xGetDocList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocTitle", DocTitle);
                cmd.Parameters.AddWithValue("@PublishState", publishState);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

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