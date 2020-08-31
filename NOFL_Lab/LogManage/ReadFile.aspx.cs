using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text;

public partial class LogManage_ReadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      
           
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strFolderPath = Server.MapPath("/LogSample/");

        //20140205_YOBHPA01.txt

        int LogItemID = 1;
        string[] LogFiles = Directory.GetFiles(strFolderPath);

        foreach (string f in LogFiles)
        {
            string strFilePath = f;
            string strFileName = Path.GetFileName(f);
            string strLogItemName = strFileName.Substring(14, 3);
            Response.Write(strLogItemName);
            //Response.End();
            FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);//utf-8格式，下面的是gb2312格式  

            string strLine0 = sr.ReadLine();
            string LogItemLen = "";

            if (strLine0 != null && strLine0 != "")
            {
                // strLine0 = sr.ReadLine();
                //Response.Write(strLine0 + "<br/>" + strLine0.Split(',').Length.ToString());
                //  Response.Write ()

                foreach (string s in strLine0.Split(','))
                    LogItemLen += s.Length.ToString() + ",";


                Response.Write(strLogItemName + " = " + LogItemLen + "<br/>");
                // Response.End();
                //for (int i = 0; i < strLine0.Split(',').Length; i++)
                //{+
                //    if (i==29)
                //    Response.Write(strLine0.Split(',')[i]+strLine0.Split(',')[i].Length.ToString() + ',');
                //}
try{             
			 DB.ExecuteNonQuery(@"INSERT INTO [dbo].[L_LogItem]
           ([LogItemID] ,[LogItemName] ,[ItemFieldLen]) VALUES
           (" + LogItemID.ToString() + ",'" + strLogItemName.ToString() + "','" + LogItemLen.ToString().TrimEnd(',') + "') ", "ConnDB");

}catch {}
                LogItemID++;

            }
            sr.Close();
            fs.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strFolderPath = Server.MapPath("/LogSample/");

        //20140205_YOBHPA01.txt

        int LogItemID = 1;
        string[] LogFiles = Directory.GetFiles(strFolderPath);

        foreach (string f in LogFiles)
        {
            string strFilePath = f;
            string strFileName = Path.GetFileName(f);
            string strLogItemName = strFileName.Substring(14, 3);
            Response.Write(strLogItemName+"<br/>");
            //Response.End();
            FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);//utf-8格式，下面的是gb2312格式  

            string strLine0 = sr.ReadLine();
            string LogItemLen = "";
           
            while (strLine0 != null && strLine0 == "@@")
            {
                // 
                //Response.Write(strLine0 + "<br/>" + strLine0.Split(',').Length.ToString());
                //  Response.Write ()

                foreach (string s in strLine0.Split(','))
                    LogItemLen += s.Length.ToString() + ",";


                strLine0 = sr.ReadLine();
                LogItemID++;

            }
            sr.Close();
            fs.Close();
        }   
        
         //  DB.ExecuteNonQuery(@"INSERT INTO [dbo].[L_LogItem] ([LogItemID] ,[LogItemName] ,[ItemFieldLen]) VALUES   (" + LogItemID.ToString() + ",'" + strLogItemName.ToString() + "','" + LogItemLen.ToString().TrimEnd(',') + "') ", "ConnDB");


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string strFolderPath = Server.MapPath("/LogSample/");

        //20140205_YOBHPA01.txt
         
        string[] LogFiles = Directory.GetFiles(strFolderPath);

        foreach (string f in LogFiles)
        {
            string strFilePath = f;
            string strFileName = Path.GetFileName(f);
            string strLogItemName = strFileName.Substring(14, 3);
            Response.Write(strFilePath + "<br/>");
            Encoding enc = Encoding.Unicode;

            if (strLogItemName.ToLower() == "pdi") enc = Encoding.UTF8;

            string[] lines = File.ReadAllLines(strFilePath, enc);
            var processed = lines.Select(line => line.Replace("@@", ""));
            File.WriteAllLines(strFilePath, processed.ToArray(), Encoding.Unicode);

            SqlCommand cmd = new SqlCommand("dbo.usp_LogManage_InsertRecieveItem");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@TxtFileLoc", strFilePath);
             cmd.Parameters.Add("@TxtFileName", strFileName);
             cmd.Parameters.Add("@CheckFileLogID", 1);
          //   if (strLogItemName.ToLower() != "pdi")
            DB.ExecuteNonQuery(cmd, "ConnDB");
			  //   DB.ExecuteNonQuery(" drop table L_Log_" + strLogItemName, "ConnDB");
             
        }

        //  DB.ExecuteNonQuery(@"INSERT INTO [dbo].[L_LogItem] ([LogItemID] ,[LogItemName] ,[ItemFieldLen]) VALUES   (" + LogItemID.ToString() + ",'" + strLogItemName.ToString() + "','" + LogItemLen.ToString().TrimEnd(',') + "') ", "ConnDB");

    }
	
	
	
	protected void Button4_Click(object sender, EventArgs e)
    {
        string strFolderPath = Server.MapPath("/LogSample/");

        //20140205_YOBHPA01.txt
         
        string[] LogFiles = Directory.GetFiles(strFolderPath);

        foreach (string f in LogFiles)
        {
            string strFilePath = f;
            string strFileName = Path.GetFileName(f);
            string strLogItemName = strFileName.Substring(14, 3);
            Response.Write(strFilePath + "<br/> ");
			   DB.ExecuteNonQuery(" drop table L_Log_" + strLogItemName, "ConnDB");
             
        }

        //  DB.ExecuteNonQuery(@"INSERT INTO [dbo].[L_LogItem] ([LogItemID] ,[LogItemName] ,[ItemFieldLen]) VALUES   (" + LogItemID.ToString() + ",'" + strLogItemName.ToString() + "','" + LogItemLen.ToString().TrimEnd(',') + "') ", "ConnDB");

    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        string strFolderPath = Server.MapPath("/LogSample/");

        //20140205_YOBHPA01.txt

        string[] LogFiles = Directory.GetFiles(strFolderPath);

        string strFilePath = strFolderPath+"20150326_YOBHPPDI.txt";
        string strFileName = Path.GetFileName(strFilePath);
            string strLogItemName = strFileName.Substring(14, 3);
            Response.Write(strFilePath + "<br/>");
        

            string[] lines = File.ReadAllLines(strFilePath, Encoding.UTF8);
            var processed = lines.Select(line =>  line.Replace("@@", ""));
            File.WriteAllLines(strFilePath, processed.ToArray(), Encoding.Unicode);

            SqlCommand cmd = new SqlCommand("dbo.usp_LogManage_InsertRecieveItem");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@TxtFileLoc", strFilePath);
            cmd.Parameters.Add("@TxtFileName", strFileName);
            cmd.Parameters.Add("@CheckFileLogID", 1);
            DB.ExecuteNonQuery(cmd, "ConnDB");
         

        
    }
}