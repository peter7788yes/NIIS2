using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseMaintain_DownloadFileOP : BasePage
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        int ID = 0;
        int.TryParse(Request["i"], out ID);

        DownloadVM VM = new DownloadVM(ID);
         
         string Json = JsonConvert.SerializeObject(VM);
         string code = QueryStringEncryptToolS.Encrypt(Json);
         Response.Redirect("http://niis_fs.hyweb.com.tw/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
         Response.End();
       
    }
}