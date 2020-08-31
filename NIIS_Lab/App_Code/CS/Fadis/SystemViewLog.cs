using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// SystemViewLog 的摘要描述
/// </summary>
public class SystemViewLog
{
    public enum DataType
    {
        個案基本資料 = 1,
        個人訪查資料 = 2,
        預注資料 = 3,
        常規催種通知 = 4,
        入境催種通知 =5,
        PCV催種通知 = 6
    }

    /// <summary>
    /// 瀏覽/下載 log紀錄
    /// </summary>
    /// <param name="datatype">資料類型</param>
    /// <param name="ViewDate">瀏覽/下載時間</param>
    /// <param name="UserID">操作者ID</param>
    /// <param name="DataID">個案ID / 催種資料ID</param>
    /// <param name="FileID">催種通知檔案ID，若為個案基本資料、個人訪查資料、預注資料則填0</param>
    /// <param name="Cnt">催種通知數量，若為個案基本資料、個人訪查資料、預注資料則填0</param>
    public static void AddLog(DataType datatype, DateTime ViewDate, String UserID, String DataID, int FileID, int Cnt)
    {
        if((int)datatype < 4) { 
            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_SystemViewLog_xAddLog",
                new Dictionary<string, object>()
                {
                    { "@ViewDate", ViewDate.ToString("yyyy/MM/dd HH:mm:ss") },
                    { "@UserID", UserID },
                    { "@DataType", (int)datatype },
                    { "@DataID", DataID },
                    { "@FileID", DBNull.Value },
                    { "@Cnt", DBNull.Value },
                });
        } else
        {
            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_SystemViewLog_xAddLog",
            new Dictionary<string, object>()
            {
                    { "@ViewDate", ViewDate.ToString("yyyy/MM/dd HH:mm:ss") },
                    { "@UserID", UserID },
                    { "@DataType", (int)datatype },
                    { "@DataID", DataID },
                    { "@FileID", FileID },
                    { "@Cnt", Cnt },
            });
        }
    }
}