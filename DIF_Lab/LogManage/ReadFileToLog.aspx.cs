using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions; 

public partial class LogManage_ReadFileToLog : System.Web.UI.Page
{
    DataTable dtItem = new DataTable();
    string TransMsg = "", GroupTransMsg="";
    int CheckFileID = 0;
    int TotalNeedLogItem = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        dtItem = (DataTable)DBUtil.DBOp("ConnDB", " SELECT [LogItemID],[LogItemName],[ItemFieldLen],[ItemFieldName],[LogItemCName],[Operation],[LogItemFieldKeySeq],[ItemFieldCount],[ApplyDateSeq],[ApplyTimeSeq],bActive,FileNameFormat,LogTableName,ConfirmFileNameFormat,bMust  FROM [dbo].[L_LogItem] ", new string[] { }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        TotalNeedLogItem = dtItem.Select("bMust=1").Count();


        AppendMessage("---開始執行時間---");
        try
        {
            ReadFolder();

           AppendMessage("---結束執行時間---");
        }
        catch (Exception ex)
        { 
            AppendMessage("Catch - 1 :  " + ex.Message + ex.StackTrace);
            AppendMessage("---結束執行時間---");
            DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckFileCount] ([FileDate] ,[MustCount] ,[ActuallyCount] ,CheckStatus,TransferMsg) VALUES (null,{1},0,2,{0})  ", new string[] { TransMsg, TotalNeedLogItem.ToString () }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        }
       


        //Response.Write(TransMsg);
    }
    public class FileDateAndItemName
    {
        public FileDateAndItemName()
        { }
        public FileDateAndItemName(string filedate, string itemname)
        {
            FileDate = filedate;
            ItemName = itemname;
        }
        public string FileDate { get; set; }
        public string ItemName { get; set; }

    }


    protected void AppendMessage(string Msg)
    {
        TransMsg += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + Msg + "\n";
    }

    protected void GroupAppendMessage(string Msg)
    {
        GroupTransMsg += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + Msg + "\n";
    }
    protected void ReadFolder ()
{ 
    List<FileDateAndItemIDs> FileDateAndItemIDsList = new List<FileDateAndItemIDs>(); 
          
    IEnumerable<IGrouping<string, FileDateAndItemIDs>> FileDateGroup = FileDateAndItemIDsList.GroupBy(f => f.FileDate);
    string strFolderPath = Server.MapPath("/LogSample/");
    string[] LogFiles = new string[]{}; 
   
      AppendMessage("檢查一下Folder" + strFolderPath);
 
    try
    {
       LogFiles = Directory.GetFiles(strFolderPath); 
    }
    catch (Exception ex)
    {
        AppendMessage("Catch - 2 :  " + ex.Message); 
    }

    AppendMessage("一共有" + LogFiles.Length .ToString ()+"個檔案");

    if (LogFiles.Length > 0)
    {
        #region MyRegion 

        AppendMessage("來看一下有哪些檔案");
        try
        {
            foreach (string f in LogFiles)
            { 
               
                try
                { 
                    string strFilePath = f;
                    string strFileName = Path.GetFileName(f);
                    AppendMessage(strFileName);

                    //這段很奇怪XD 可以再改一下
                    FileDateAndItemIDs ofi = CheckFileNeed(strFileName);
                    ofi.FileFullPath = strFilePath;

                    if (ofi.ItemIDs.Count() > 0) FileDateAndItemIDsList.Add(ofi);
                }
                catch (Exception ex2)
                {
                    AppendMessage("Catch 檔案格式有誤:" + f +"(" + ex2.Message +")"); 
                }
                
            }
        }
        catch (Exception ex)
        {
            AppendMessage("Catch - 3 :" + ex.Message); 
        }


        //整理一下列出我認得的file
        AppendMessage("列出認得的檔案 " + FileDateAndItemIDsList.Count .ToString ()+" 個");
        foreach (FileDateAndItemIDs fi in FileDateAndItemIDsList) AppendMessage( fi.FileName );
        
        AppendMessage("把檔案依日期Group 列出來 ");

        FileDateGroup  = FileDateAndItemIDsList.GroupBy(f => f.FileDate);
        foreach (IGrouping<string, FileDateAndItemIDs> FileDate in FileDateGroup )
        {
            GroupTransMsg = "";
            GroupAppendMessage(FileDate.Key + " 有 " + FileDate.Count() + " 個");  
            List<FileDateAndItemIDs> SameFileDateFileDateAndItemIDsList = FileDateAndItemIDsList.Where(fi => fi.FileDate == FileDate.Key && fi.IsMainItem==true).ToList();
            GroupAppendMessage("主要介接項目" + SameFileDateFileDateAndItemIDsList.Count().ToString() + "個");
            foreach (FileDateAndItemIDs fi in SameFileDateFileDateAndItemIDsList) GroupAppendMessage(fi.FileName);

            //主要介接項目
            if (SameFileDateFileDateAndItemIDsList.Count > 0)
            {
                int CheckFileID = Convert.ToInt32(DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckFileCount] ([FileDate] ,[MustCount] ,[ActuallyCount],CheckStatus ) VALUES ({0},{1},{2},{3} ) ;select @@identity ", new string[] { FileDate.Key, TotalNeedLogItem.ToString(), SameFileDateFileDateAndItemIDsList.Count().ToString(), (TotalNeedLogItem != SameFileDateFileDateAndItemIDsList.Count()? "2" :"1") }, NSDBUtil.CmdOpType.ExecuteScalar));
                 
                try
                {
                     HandleFile(SameFileDateFileDateAndItemIDsList, CheckFileID, FileDate.Key);

                  }
                catch (Exception ex)
                { 
                    GroupAppendMessage("Catch - 4 :" + ex.Message);
                }

                try
                {
                    GroupAppendMessage("開始轉置"); 
                    ExecByOrder(CheckFileID);
                }
                catch (Exception ex)
                {
                    GroupAppendMessage("Catch - ExecByOrder :" + ex.Message);
                }






                DBUtil.DBOp("ConnDB", "UPDATE [dbo].[L_LogCheckFileCount] set [TransferMsg] = {0},FinishDate=getdate() where id={1} ", new string[] { TransMsg + GroupTransMsg, CheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);

            }

        }

        /**
        AppendMessage("把檔案依日期Group 列出來");


        foreach (IGrouping<string, FileDateAndItemName> FileDate in FileDateGroup)
        {
            Response.Write(FileDate.Key + ":" + FileDate.Count() + "  ");
            AppendMessage(FileDate.Key + " 有 " + FileDate.Count() +" 個");
            //YOBHP 才 寫log
            string[] SameFileDateLogFile = LogFiles.Where(f => f.Contains(FileDate.Key.Replace("/", "")) && f.Contains("YOBHP")).ToArray();
            if (SameFileDateLogFile.Length > 0)
            {

                int CheckFileID = Convert.ToInt32(DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckFileCount] ([FileDate] ,[MustCount] ,[ActuallyCount] ) VALUES ({0},{1},{2} ) ;select @@identity ", new string[] { FileDate.Key, "26", FileDate.Count().ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
                Response.Write("CheckFileID=" + CheckFileID.ToString() + "<br/>");

                foreach (string f in SameFileDateLogFile)
                {
                    Response.Write("  SameFileDateLogFile=" + f + "<br/>");
                }

                HandleFile(SameFileDateLogFile, CheckFileID);

                DBUtil.DBOp("ConnDB", "UPDATE [dbo].[L_LogCheckFileCount] set [TransferMsg] = {0} where id={1} ", new string[] { TransMsg, CheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);
            }
        }



        **/






        #endregion
    }
    else
    {
        DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckFileCount] ([FileDate] ,[MustCount] ,[ActuallyCount] ,CheckStatus,TransferMsg) VALUES (null,{1},0,0,{0})  ", new string[] { TransMsg, TotalNeedLogItem.ToString () }, NSDBUtil.CmdOpType.ExecuteNonQuery);
             
    }
}

     protected void ExecByOrder  (int CheckFileID)
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", @"
        SELECT ExecOrder,ID,FileName  FROM [dbo].[L_LogCheckItemFile] 
        inner join L_LogItem on L_LogItem.LogItemID=[FileItem] and MainID={0}
        ORDER BY L_LogItem.ExecOrder Asc ", new string[] { CheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
       
         foreach (DataRow r in dt.Rows)
        { 
            GroupAppendMessage( r["FileName"].ToString());
            //一個Log讀完就 log to case
            DBUtil.DBOp("ConnDB", " exec [dbo].[usp_LogManage_UpdateCaseFromLogWithApplyDate]    {0}  ", new string[] { r["ID"].ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);

        }


     }

    protected FileDateAndItemIDs CheckFileNeed(string strFileName)
    {
        //20140205_YOBHPA01.txt

        FileDateAndItemIDs di = new FileDateAndItemIDs();

        string strFileNameFormat = strFileName.Substring(9, 5);  //YOBHP
        string strLogItemName = strFileName.Substring(14, 3);     //A01
        string strLogItemDate = strFileName.Substring(0, 4) + "/" + strFileName.Substring(4, 2) + "/" + strFileName.Substring(6, 2);
        di.FileDate = strLogItemDate;
       
        

        di.ItemIDs = dtItem.Select("bActive=1 and [LogItemName]='" + strLogItemName + "' and  [FileNameFormat]='" + strFileNameFormat + "'   ").Select(r => r["LogItemID"].ToString()).ToArray();
        if (di.ItemIDs.Count() == 0)
        {
            di.ItemIDs = dtItem.Select("bActive=1 and [LogItemName]='" + strLogItemName + "' and   [ConfirmFileNameFormat]='" + strFileNameFormat + "'  ").Select(r => r["LogItemID"].ToString()).ToArray();
            if (di.ItemIDs.Count() > 0)  di.IsConfirmItem = true;
             


        }
        else
        {
           string strConfirmFormat  =  dtItem.Select(" bActive=1 and [LogItemName]='" + strLogItemName + "' and   [FileNameFormat]='" + strFileNameFormat + "'  ").Select(r => r["ConfirmFileNameFormat"].ToString()).First();
            di.ConfirmFileName= strFileName.Replace(strFileNameFormat, strConfirmFormat);
         
            di.IsMainItem = true; 
        }
      
        di.FileName = strFileName;
         
        return di;

    }
    protected void HandleFile(List<FileDateAndItemIDs> LogFiles, int CheckFileID,string filedate)
    {
        Response.Write("-----------  HandleFile  start---------------<br/>");
       
        GroupAppendMessage("匯入LogTable " + filedate + ""); 

        foreach (FileDateAndItemIDs f in LogFiles)
        {

            GroupAppendMessage( f.FileName );
            
            foreach (string FileItemID in f.ItemIDs)
            {
                GroupAppendMessage("Item - " + FileItemID + " ");
                try
                {
                    if (FileItemID != "")
                    {
                          
                         SqlBulkCopy(f.FileFullPath, FileItemID, f.FileDate, f.FileName, CheckFileID);
                     }
                }
                catch (Exception ex){

                    GroupAppendMessage("Catch - 6 :" + ex.Message);
                }

            }

            //Response.Write(string.Format(" exec [dbo].[usp_LogManage_UpdateCaseFromLogWithApplyDate]    {0}  ,{1}  <br/>", FileItemID, LogID.ToString()));
            //@LogItemID  ,@CheckFileLogID

            //資料來源的型別 String 指定值無法轉換成指定目標資料行的型別 nvarchar
            //欄位不夠長


        }

    }



    protected void SqlBulkCopy(string FileFullPath, string ItemID, string FileDate, string FileName, int iCheckFileID)
    {
       
           var lines = File.ReadAllLines(FileFullPath);  
            string FirstLine = lines.First(); 
            string[] strColArray = FirstLine.Split(',');
            int LogItemColCount = dtItem.Select("LogItemID = " + ItemID).First()["ItemFieldLen"].ToString().Split(',').Length;
            string ConfirmFileName = FileFullPath.Replace(dtItem.Select("LogItemID = " + ItemID).First()["FileNameFormat"].ToString(),
                                                          dtItem.Select("LogItemID = " + ItemID).First()["ConfirmFileNameFormat"].ToString());
                 
        var dt = new DataTable();

                  
            for (int index = 0; index < strColArray.Length; index++)
            {
                dt.Columns.Add(new DataColumn("Column" + (index + 1)));
            }

    
         //foreach (var dataRow in lines)
     for(int i =0;i<lines.Length ;i++)
         {
             var dataRow = lines[i];
             
             
           // if (dataRow.Trim() != "")  
             if (dataRow != "@@" && dataRow.Trim() != "")   //連@@都會被新增進去
             {
                //檢查欄位數是否符合itemsetting


                 DataRow r = dt.NewRow();
                 //順便把資料trim一下 
                 r.ItemArray = dataRow.Split(',').Select(data => data.Trim()).ToArray();
                   
                  
                //欄位長度不符
                 if (r.ItemArray.Length != LogItemColCount) GroupAppendMessage("欄位個數不符 - 第"+ (i+1).ToString () +"列");

                 dt.Rows.Add(r);
             }
         }
 
       
          int LogID = 0;
  try{

      LogID = Convert.ToInt32(DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckItemFile] ([FileDate] ,[FileItem] ,[FileName]  ,[ShouldCount] ,[ActuallyCount],MainID) VALUES ({0}, {1},{2},{3} ,{4},{5} ) ;select @@identity ", new string[] { FileDate, ItemID, FileName, GetShouldCount(ConfirmFileName).ToString(), dt.Rows.Count.ToString(), iCheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
       
          if (LogID > 0)
          { 
              dt.Columns.Add(new DataColumn("LogID"));
              dt.Columns.Add(new DataColumn("ErrorMsg"));
               
              foreach (DataRow r in dt.Rows)
              {
                  r["LogID"] = LogID;
                  r["ErrorMsg"] = "";
                  //foreach (DataColumn c in dt.Columns)
                  //    if (r[c.ColumnName].ToString() == "")
                  //    {
                  //        r[c.ColumnName] = "";
                  //    }

              }



              //    foreach (DataColumn c in dt.Columns)   Response.Write(c.ColumnName);

           
              string strLogTableName = Convert.ToString(DBUtil.DBOp("ConnDB", "exec [dbo].[usp_LogManage_CreateLogTable] {0}  ", new string[] {  ItemID }, NSDBUtil.CmdOpType.ExecuteScalar));


              // Then, write the DataTable to the server using SqlBulkCopy
              using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
              using (var sqlBulkCopy = new SqlBulkCopy(sqlConnection))
              {
                  sqlBulkCopy.DestinationTableName = strLogTableName;


                  //固定欄位
                  //ID
                  //CreateDate
                  //bSuc
                  //LogID
                  //ErrorMsg=""

                  foreach (var column in dt.Columns)
                  {
                      sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                  }

                  sqlConnection.Open();
                  sqlBulkCopy.WriteToServer(dt);
                  sqlConnection.Close();
              }
            
          }
          else
          {
              GroupAppendMessage("LogID =0 ");
          }   
          }
          catch (Exception ex)
          {
              GroupAppendMessage("sqlBulkCopy catch : " + ex.Message + ex.StackTrace);
              //Response.Write(ex.Message + ex.StackTrace + "<br/>"); 
          }

         
    

    }

         

    protected void HandleFile(string[] LogFiles, int CheckFileID)
    {
        Response.Write("-----------  HandleFile  start---------------<br/>");

        //string strFolderPath = Server.MapPath("/LogSample/");
        //string[] LogFiles = Directory.GetFiles(strFolderPath);

        //20140205_YOBHPA01.txt

        // var lastLine = File.ReadLines("file.txt").Last();


      



        foreach (string f in LogFiles)
        {


            //20140205_YOBHPA01.txt
            string strFilePath = f;
            string strFileName = Path.GetFileName(f);


             GetDateAndItems(strFileName);

            string strFileNameFormat = strFileName.Substring(9, 5);  //YOBHP
            string strLogItemName = strFileName.Substring(14, 3);     //A01
            string strLogItemDate = strFileName.Substring(0, 4) + "/" + strFileName.Substring(4, 2) + "/" + strFileName.Substring(6, 2);
           


            string sourcePath = Path.GetFullPath(f);

            var lines = File.ReadAllLines(sourcePath);
         
            
            string FirstLine = lines.First();
            string[] strColArray = FirstLine.Split(',');
            var dt = new DataTable();

            for (int index = 0; index < strColArray.Length; index++)
            {
                dt.Columns.Add(new DataColumn("Column" + (index + 1)));
            }

            

            foreach (var dataRow in lines)
            {
                if (dataRow != "@@" && dataRow.Trim () != "")   //連@@都會被新增進去
                {
                    DataRow r = dt.NewRow();
                    r.ItemArray = dataRow.Split(',');
                    dt.Rows.Add(r);
                }
            }
            string  FileItemID =  dtItem.Select("bActive=1 and [LogItemName]='" + strLogItemName + "' and [FileNameFormat]='" + strFileNameFormat + "'").First()["LogItemID"].ToString ();
          
            int LogID = Convert.ToInt32(DBUtil.DBOp("ConnDB", "INSERT INTO [dbo].[L_LogCheckItemFile] ([FileDate] ,[FileItem] ,[FileName]  ,[ShouldCount] ,[ActuallyCount],MainID) VALUES ({0}, {1},{2},{3} ,{4},{5} ) ;select @@identity ", new string[] { strLogItemDate,FileItemID, strFileName, "0", dt.Rows.Count.ToString(), CheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
             
            dt.Columns.Add(new DataColumn("LogID"));
            dt.Columns.Add(new DataColumn("ErrorMsg"));
            
            foreach (DataRow r in dt.Rows)
            {
                r["LogID"] = LogID;
                r["ErrorMsg"] = "";
                //foreach (DataColumn c in dt.Columns)
                //    if (r[c.ColumnName].ToString() == "")
                //    {
                //        r[c.ColumnName] = "";
                //    }
            } 
          


        //    foreach (DataColumn c in dt.Columns)   Response.Write(c.ColumnName);


            string strLogTableName  = Convert.ToString(DBUtil.DBOp("ConnDB", "exec [dbo].[usp_LogManage_CreateLogTable] {0}  ", new string[] {   FileItemID  }, NSDBUtil.CmdOpType.ExecuteScalar ));

            try
            {
                // Then, write the DataTable to the server using SqlBulkCopy
                using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
                using (var sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    sqlBulkCopy.DestinationTableName = strLogTableName;
                

                    //固定欄位
                    //ID
                    //CreateDate
                    //bSuc
                    //LogID
                   

                    foreach (var column in dt.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    sqlConnection.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    sqlConnection.Close();
                }
            }
            catch(Exception ex) {
                AppendMessage("sqlBulkCopy catch : " + ex.Message + ex.StackTrace);
                Response.Write(ex.Message+ex.StackTrace  + "<br/>");
            }

            //一個Log讀完就 log to case
         DBUtil.DBOp("ConnDB", " exec [dbo].[usp_LogManage_UpdateCaseFromLogWithApplyDate]    {0}  ,{1} ", new string[] {  FileItemID, LogID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);

         //Response.Write(string.Format(" exec [dbo].[usp_LogManage_UpdateCaseFromLogWithApplyDate]    {0}  ,{1}  <br/>", FileItemID, LogID.ToString()));
               //@LogItemID  ,@CheckFileLogID

            //資料來源的型別 String 指定值無法轉換成指定目標資料行的型別 nvarchar
            //欄位不夠長


        }


       
 


    
    }

    protected FileDateAndItemIDs GetDateAndItems (string FileName)
    {   
        FileDateAndItemIDs di = new FileDateAndItemIDs();
        Regex regex = new Regex(@"\d\d\d\d\d\d\d\d");
        Match match = regex.Match(FileName);
        if (match.Success)
        {  // AppendMessage("Get檔案日期 " +match.Value);
            di.FileDate =match.Value ;
        }

        return di;

    }


    public class FileDateAndItemIDs
    {
        public FileDateAndItemIDs()
        { }
        public FileDateAndItemIDs(string filedate, string[] itemids, string filename, string filefullpath)
        {
            FileDate = filedate;
            ItemIDs = itemids;
            FileName = filename;
            FileFullPath = filename;
        }
        public string FileDate { get; set; }
        public string[] ItemIDs { get; set; }
        public string FileName { get; set; }
        public string FileFullPath { get; set; }
        public bool IsMainItem { get; set; }
        public bool IsConfirmItem { get; set; }
        public string ConfirmFileName { get; set; }
        public string TableName { get; set; }
    }


 protected int GetShouldCount(string FilePath)
    {
        
        int ShouldCount = 0;
        try
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                string FirstLine = lines.First();
                ShouldCount = Convert.ToInt32(FirstLine.Split(',')[FirstLine.Split(',').Length - 1]);
            }
        }
        catch (Exception ex) {
            GroupAppendMessage("GetShouldCount catch  " + ex.Message );
        }
       return ShouldCount;

 
 }
 protected void TxtToDt(string sourcePath)
{
    // First, load the data into a DataTable
var dataTable = new DataTable();
dataTable.Columns.Add(new DataColumn("RawData", typeof (string)));

var lines = File.ReadAllLines(sourcePath);

foreach (var dataRow in lines.Select(rawDataStorage => dataTable.NewRow()["RawData"] = rawDataStorage))
{
    dataTable.Rows.Add(dataRow);
}

// Then, write the DataTable to the server using SqlBulkCopy
using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
using (var sqlBulkCopy = new SqlBulkCopy(sqlConnection))
{
    sqlBulkCopy.DestinationTableName = "RawDataStorage";

    foreach (var column in dataTable.Columns)
    {
        sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
    }

    sqlConnection.Open();
    sqlBulkCopy.WriteToServer(dataTable);
    sqlConnection.Close();
}

}
}