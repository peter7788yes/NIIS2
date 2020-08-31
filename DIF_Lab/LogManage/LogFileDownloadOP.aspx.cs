using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using System.IO;

public partial class LogManage_LogFileDownloadOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        int f; 

        int.TryParse(Request["f"], out f);
        string fileName = DBUtil.DBOp("ConnDB"," SELECT  [FileName]  FROM  [L_LogCheckItemFile] where ID={0}", new string[] { f.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar).ToString();
        string filePath = Server.MapPath("/LogSample/") + fileName;//路徑

        //以字符流的形式下載文件
        FileStream fs = new FileStream(filePath, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        Response.ContentType = "application/octet-stream";
        //通知瀏覽器下載文件而不是打開
        Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}