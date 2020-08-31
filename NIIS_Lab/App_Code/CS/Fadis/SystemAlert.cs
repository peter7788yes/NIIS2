using System;
using System.Collections.Generic;

/// <summary>
/// SystemAlert 的摘要描述
/// </summary>
public class SystemAlert
{
    public enum AlertType
    {
        疫苗待撥入=1,
        疫苗被退回=2,
        入境通知=3,
        出境通知=4,
        醫事十碼章更新=5,
        個案接種日期異常=6,
        疫苗確認=7,
        個案資料審核結果通知=8,
        天然災害後疫苗回報 =9
    }

    /// <summary>
    /// 新增系統提醒
    /// </summary>
    /// <param name="alerttype">提醒作業類型</param>
    /// <param name="OrgID">單位ID</param>
    /// <param name="CreateDate">依作業類型傳入不同的值，請參考SA文件</param>
    /// <param name="Msg">依作業類型傳入不同的值，請參考SA文件</param>
    /// <param name="QueryString">需帶給處理頁的QueryString</param>
    public static void Add(AlertType alerttype, int OrgID, DateTime CreateDate, string Msg, string QueryString)
    {
        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_SystemAlert_xAdd",
            new Dictionary<string, object>()
            {
                { "@AlertType", (int)alerttype },
                { "@OrgID", OrgID },
                { "@CreateDate", CreateDate.ToString("yyyy/MM/dd HH:mm:ss") },
                { "@Msg", Msg },
                { "@RefID", QueryString },
            });
    }

    /// <summary>
    /// 把alert標示為已處理
    /// </summary>
    /// <param name="AlertID"></param>
    public static void Finished(int AlertID)
    {
        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_SystemAlert_xFinish",
            new Dictionary<string, object>()
            {
                { "@ID", AlertID },
            });
    }

}