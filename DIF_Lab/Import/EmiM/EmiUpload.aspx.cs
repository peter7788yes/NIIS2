using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Threading;

public partial class Import_EmiM_EmiUpload : BasePage
{
    protected int MasterID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //1.取得檔案
        if (!tbFile.HasFile)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('尚未選取要上傳的檔案!');", true);
            return;
        }
        if (!tbFile.FileName.ToLower().EndsWith(".txt"))
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('檔案限定為TXT!');", true);
            return;
        }

        //2.寫入MasterLog，取得MasterLog ID
        try
        {
            MasterID = AddMasterLog(tbFile.FileName);
        }
        catch (Exception ex)
        {
            Log(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " 新增MasterLog 失敗，FileName = " + tbFile.FileName + " 錯誤：" + ex.Message);
            return;
        }

        //非同步處理匯入的資料
        Thread th_tmp = new Thread(new ThreadStart(AsyncJob));
        th_tmp.Start();

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('檔案匯入處理中...');", true);
    }

    /// <summary>
    /// 非同步處理匯入的資料
    /// </summary>
    protected void AsyncJob()
    {
        int SuccessCnt = 0;
        int RepeatCnt = 0;
        int ErrorCnt = 0;

        //3.讀檔，檢查資料是否重複；逐筆寫入匯入Log，若格式不符輸出檔案
        try
        {
            using (StreamReader sr = new StreamReader(this.tbFile.PostedFile.InputStream, System.Text.Encoding.Default))
            {
                String FileLine = "";
                //資料從第三列開始
                sr.ReadLine();
                sr.ReadLine();
                while ((FileLine = sr.ReadLine()) != null)
                {
                    int Status = 0;//0:異常 1:正常 2:重複
                    if (!FileLine.Trim().Equals(""))
                    {
                        //異常：出生日期、出境日期 格式不符 or 對應不到個案
                        string[] FileData = FileLine.Trim().Split(',');
                        if (!IsRepeat(FileData))
                        {
                            Status = AddEmiData(FileData);
                            if (Status == 0)
                                ErrorCnt++;
                            else
                                SuccessCnt++;
                        }
                        else
                        {
                            Status = 2;
                            RepeatCnt++;
                        }
                        //寫入匯入紀錄
                        AddEmiLog(MasterID, FileData, Status);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " 讀檔失敗，MasterID = " + MasterID.ToString() + " 錯誤：" + ex.Message);
        }
        //4.更新MasterLog
        UpdateMasterLog(MasterID, SuccessCnt, RepeatCnt, ErrorCnt);
    }

    /// <summary>
    /// 寫入主紀錄
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="CatID"></param>
    /// <returns></returns>
    protected int AddMasterLog(string FileName)
    {
        int MasterID = 0;
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_xAdd", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", FileName);
                cmd.Parameters.AddWithValue("@UserID", AuthServer.GetLoginUser().ID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }
        if (ds.Tables.Count > 0)
            int.TryParse(ds.Tables[0].Rows[0]["ID"].ToString(), out MasterID);
        return MasterID;
    }

    /// <summary>
    /// 檢查是否為重複資料
    /// </summary>
    /// <param name="FileData"></param>
    /// <returns></returns>
    protected bool IsRepeat(string[] FileData)
    {
        int cnt = 0;
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_EmiLog_xCheckRepeat", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Seq", FileData[0]);
                cmd.Parameters.AddWithValue("@CaseID", FileData[1]);
                cmd.Parameters.AddWithValue("@Birthday", FileData[2]);
                cmd.Parameters.AddWithValue("@EmiDate", FileData[3]);
                cmd.Parameters.AddWithValue("@UserName", FileData[4]);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }
        if (ds.Tables.Count > 0)
            int.TryParse(ds.Tables[0].Rows[0]["cnt"].ToString(), out cnt);
        if (cnt > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 更新Master匯入資訊
    /// </summary>
    /// <param name="MasterID"></param>
    /// <param name="SuccessCnt"></param>
    /// <param name="RepeatCnt"></param>
    /// <param name="ErrorCnt"></param>
    protected void UpdateMasterLog(int MasterID, int SuccessCnt, int RepeatCnt, int ErrorCnt)
    {
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_xUpdate", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", MasterID);
                cmd.Parameters.AddWithValue("@SuccessCnt", SuccessCnt);
                cmd.Parameters.AddWithValue("@RepeatCnt", RepeatCnt);
                cmd.Parameters.AddWithValue("@ErrorCnt", ErrorCnt);
                sc.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// 新增個案出境紀錄
    /// </summary>
    /// <param name="FileData"></param>
    /// <returns></returns>
    protected int AddEmiData(string[] FileData)
    {
        int Status = 0;
        string birthday = TaiwanYear.ToDateString(FileData[2]);
        string EmiDate = TaiwanYear.ToDateString(FileData[3]);
        if (birthday.Equals("") || EmiDate.Equals(""))
            return 0;

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            DataTable dt = new DataTable();
            //寫入入境資料
            using (SqlCommand cmd = new SqlCommand("usp_CaseUser_xAddImmiData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdNo", FileData[1]);
                cmd.Parameters.AddWithValue("@ImmiStatus", 1);
                cmd.Parameters.AddWithValue("@ImmiDate", EmiDate.Replace("/",""));
                cmd.Parameters.AddWithValue("@FlightNo", "");
                cmd.Parameters.AddWithValue("@ArrivalLoc", "");
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["bSuccess"].ToString().Equals("1"))
                {
                    Status = 1;
                }
            }
        }
        return Status;
    }

    /// <summary>
    /// 寫入detail log
    /// </summary>
    /// <param name="MasterID"></param>
    /// <param name="FileData"></param>
    /// <param name="Status"></param>
    protected void AddEmiLog(int MasterID, string[] FileData, int Status)
    {
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            sc.Open();
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_EmiLog_xAdd", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Seq", FileData[0]);
                cmd.Parameters.AddWithValue("@CaseID", FileData[1]);
                cmd.Parameters.AddWithValue("@Birthday", FileData[2]);
                cmd.Parameters.AddWithValue("@EmiDate", FileData[3]);
                cmd.Parameters.AddWithValue("@UserName", FileData[4]);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@MasterID", MasterID);
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void Log(string logMessage)
    {
        try
        {
            using (StreamWriter w = File.AppendText("EmiUploadLog.txt"))
            {
                w.WriteLine(logMessage);
            }
        }
        catch
        {
        }
    }
}