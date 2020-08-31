using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// SystemDataLog 的摘要描述
/// </summary>
public class SystemDataLog
{
    private enum DataType
    {
        個案基本資料 = 1,
        個人訪查資料 = 2,
        預注資料 = 3,
        疫苗庫存資料 =4
    }

    public enum StockFunc
    {
        撥入 = 1,
        撥出 = 2,
        領用 = 3,
        毀損 = 4,
        退貨 = 5
    }

    public enum LogType
    {
        修改 = 1,
        刪除 = 2
    }

    /// <summary>
    /// 個案基本資料 異動log 一個欄位一筆
    /// </summary>
    /// <param name="ModifyDate">異動時間</param>
    /// <param name="UserID">操作者ID</param>
    /// <param name="DataID">個案ID(身份證字號)</param>
    /// <param name="DataLog">[修改欄位名稱]-[修改內容]</param>
    public static void AddCaseLog(DateTime ModifyDate, String UserID, String DataID, String DataLog)
    {
        SystemDataLog log = new SystemDataLog();
        log.AddLog(LogType.修改, DataType.個案基本資料, ModifyDate, UserID, DataID, DataLog);
    }

    /// <summary>
    /// 個人訪查資料 異動log
    /// </summary>
    /// <param name="logtype">修改或刪除</param>
    /// <param name="ModifyDate">異動時間</param>
    /// <param name="UserID">操作者ID</param>
    /// <param name="DataID">個案ID(身份證字號)</param>
    /// <param name="DataLog">修改填入：[修改欄位名稱]-[修改內容]。刪除填入：個人訪查資料id</param>
    public static void AddVisitLog(LogType logtype, DateTime ModifyDate, String UserID, String DataID, String DataLog)
    {
        SystemDataLog log = new SystemDataLog();
        log.AddLog(logtype, DataType.個人訪查資料, ModifyDate, UserID, DataID, DataLog);
    }

    /// <summary>
    /// 預注資料 異動log
    /// </summary>
    /// <param name="logtype">修改或刪除</param>
    /// <param name="ModifyDate">異動時間</param>
    /// <param name="UserID">操作者ID</param>
    /// <param name="DataID">個案ID(身份證字號)</param>
    /// <param name="DataLog">修改填入：[修改的疫苗劑次]-[修改內容]。刪除填入：預注資料id</param>
    public static void AddVaccinationLog(LogType logtype, DateTime ModifyDate, String UserID, String DataID, String DataLog)
    {
        SystemDataLog log = new SystemDataLog();
        log.AddLog(logtype, DataType.預注資料, ModifyDate, UserID, DataID, DataLog);
    }

    /// <summary>
    /// 疫苗庫存資料 異動log
    /// </summary>
    /// <param name="logtype">修改或刪除</param>
    /// <param name="func">異動功能</param>
    /// <param name="ModifyDate">異動時間</param>
    /// <param name="UserID">操作者ID</param>
    /// <param name="DataID">疫苗代號(ex:HBIG)</param>
    /// <param name="DataLog">只有修改時需要填：[異動功能]-[修改的疫苗代碼-批號]-[修改欄位]-[修改內容]</param>
    /// <param name="MasterID">只有刪除時需要填：刪除的MasterID</param>
    /// <param name="DetailID">只有刪除時需要填：刪除的DetailID，若是刪Master則填0</param>
    public static void AddStockLog(LogType logtype, StockFunc func, DateTime ModifyDate, String UserID, String DataID, String DataLog, int MasterID, int DetailID)
    {
        SystemDataLog log = new SystemDataLog();
        if (logtype.Equals(LogType.刪除))
        {
            if(DetailID == 0)
                DataLog = ((int)func).ToString() + "_" + MasterID.ToString();
            else
                DataLog = ((int)func).ToString() + "_" + MasterID.ToString() + "_" + DetailID.ToString();
        }
        log.AddLog(logtype, DataType.疫苗庫存資料, ModifyDate, UserID, DataID, DataLog);
    }

    private void AddLog(LogType logtype, DataType datatype, DateTime ModifyDate, String UserID, String DataID, String DataLog)
    {
        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_SystemDataLog_xAddLog",
            new Dictionary<string, object>()
            {
                { "@ModifyDate", ModifyDate.ToString("yyyy/MM/dd HH:mm:ss") },
                { "@UserID", UserID },
                { "@DataType", (int)datatype },
                { "@ModifyType", (int)logtype },
                { "@DataID", DataID },
                { "@DataLog", DataLog },
            });
    }
}