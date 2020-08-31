using AlanTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_Modify_VaccineOutBatchList : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineOut_Modify_VaccineOutBatchList()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.新增, MyPowerEnum.上傳);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        UploadPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        UserVM user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            if (SystemCode.dict.ContainsKey("StockManagementM_FroIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_FroIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) FroIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            if (SystemCode.dict.ContainsKey("StockManagementM_MonIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_MonIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) MonIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            if (SystemCode.dict.ContainsKey("StockManagementM_OriFroIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_OriFroIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) OriFroIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }

            int VaccOutBatchDataID;

            HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["BI"], out VaccOutBatchDataID));

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_VaccineOut_xGetVaccineOutBatchData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", VaccOutBatchDataID);
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                VaccineID.Text = dt.Rows[0]["VaccineID"].ToString();
                BatchType.Text = dt.Rows[0]["BatchType"].ToString();
                BatchID.Text = dt.Rows[0]["BatchID"].ToString();
                FormDrug.Text = dt.Rows[0]["FormDrug"].ToString();
                Storage.Text = dt.Rows[0]["Storage"].ToString();
            }
            if (dt1.Rows.Count > 0)
            {
                Num.Text = dt1.Rows[0]["Num"].ToString();
                TempHigh.Text = Convert.ToDouble(dt1.Rows[0]["TempHigh"]).ToString();
                FroIdx.SelectedValue = dt1.Rows[0]["FroIdx"].ToString();
                TempLow.Text = Convert.ToDouble(dt1.Rows[0]["TempLow"]).ToString();
                OriFroIdx.SelectedValue = dt1.Rows[0]["OriFroIdx"].ToString();
                MonIdx.SelectedValue = dt1.Rows[0]["MonIdx"].ToString();
            }
        }
        OriFroIdx.Enabled = false;
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();
        string script = "";
        string message = CheckNeeded();
        if (message.Length > 0)
        {
            script = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
            return;
        }

        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
        
        List<int> OutFileInfoID_List = new List<int>();

        if (TempFile.HasFile == true)
        {
            HttpFileCollection multipleFiles = Request.Files;
            for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
            {
                HttpPostedFile uploadedFile = multipleFiles[fileCount];
                string extension = "";
                string[] ary = uploadedFile.FileName.Split('.');
                if (ary.Length > 1)
                {
                    extension = ary.Last();
                }

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }

                NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient();
                string contentType = TempFile.PostedFile.ContentType;
                OutFileInfoID = WS.UploadFile(5, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

                if (OutFileInfoID < 1)
                {
                    UploadFileSuccess = false;
                    break;
                }
                else
                {
                    OutFileInfoID_List.Add(OutFileInfoID);
                }
            }
        }

        string OutFileInfoIDs = string.Join(",", OutFileInfoID_List.Select(x => x.ToString()).ToArray());

        string num = Num.Text.Trim();
        string tempHigh = TempHigh.Text.Trim();
        int froIdx = int.Parse(FroIdx.SelectedValue);
        string tempLow = TempLow.Text.Trim();
        int oriFroIdx = int.Parse(OriFroIdx.SelectedValue);
        int monIdx = int.Parse(MonIdx.SelectedValue);
        int CheckStorage = 0;
        int Success = 0; 

        int VaccOutBatchDataID;

        HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["BI"], out VaccOutBatchDataID));

        if (UploadFileSuccess == true)
        {
            DataSet ds = new DataSet();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_VaccineOut_xUpdateTempVaccineOutBatchData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", VaccOutBatchDataID);
                    cmd.Parameters.AddWithValue("@Num", num);
                    cmd.Parameters.AddWithValue("@TempHigh", tempHigh);
                    cmd.Parameters.AddWithValue("@FroIdx", froIdx);
                    cmd.Parameters.AddWithValue("@TempLow", tempLow);
                    cmd.Parameters.AddWithValue("@OriFroIdx", oriFroIdx);
                    cmd.Parameters.AddWithValue("@MonIdx", monIdx);
                    cmd.Parameters.AddWithValue("@TempFile", OutFileInfoIDs);
                    cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                    SqlParameter sp = cmd.Parameters.AddWithValue("@CheckStorage", CheckStorage);
                    SqlParameter sp1 = cmd.Parameters.AddWithValue("@Success", Success);
                    sp.Direction = ParameterDirection.Output;
                    sp1.Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                        CheckStorage = (int)sp.Value;
                        Success = (int)sp1.Value;
                    }
                }
            }
        }

        if (UploadFileSuccess && Success > 0)
        {
            int VaccineInID;
            HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["I"], out VaccineInID));
            script = "<script>alert('儲存成功!');location.href = '/Vaccine/StockManagementM/VaccineOut/New_VaccineOutDataList.aspx?ID=" + VaccineInID + "';</script>";
        }
        else
        {
            if (CheckStorage > 0)
            {
                script = "<script>alert('庫存量不足!');</script>";
            }
            else
            {
                script = "<script>alert('儲存失敗!');</script>";
            }
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }

    private string CheckNeeded()
    {
        string rtn = "";
        string num = Num.Text.Trim();
        string storge = Storage.Text.Trim();
        string tempHigh = TempHigh.Text.Trim();
        string tempLow = TempLow.Text.Trim();

        if (num.Length == 0)
        {
            rtn += "撥出數量:必填!\\n";
        }
        else if (RegularExpressions.IsPlusInt(num) == false)
        {
            rtn += "撥出數量:請輸入正整數!\\n";
        }
        else if (int.Parse(storge) - int.Parse(num) < 0)
        {
            rtn += "撥出數量:請小於或等於庫目前庫存量!\\n";
        }

        if (tempHigh.Length == 0)
        {
            rtn += "運送期間溫度:最高:必填!\\n";
        }
        else if (RegularExpressions.IsFloatTo1(tempHigh) == false)
        {
            rtn += "運送期間溫度:最高:正整數或至小數點後一位!\\n";
        }

        if (tempLow.Length == 0)
        {
            rtn += "運送期間溫度:最低:必填!\\n";
        }
        else if (RegularExpressions.IsFloatTo1(tempLow) == false)
        {
            rtn += "運送期間溫度:最低:正整數或至小數點後一位!\\n";
        }

        if (tempHigh.Length != 0 && tempLow.Length != 0 && RegularExpressions.IsFloatTo1(tempHigh) == true && RegularExpressions.IsFloatTo1(tempLow) == true)
        {
            if (float.Parse(tempHigh) < float.Parse(tempLow) || float.Parse(tempHigh) > 8.0)
            {
                rtn += "運送期間溫度:最高:低於最低溫度或大於8°C!\\n";
            }

            if (float.Parse(tempLow) > float.Parse(tempHigh) || float.Parse(tempLow) < 2.0)
            {
                rtn += "運送期間溫度:最低:高於最高溫度或小於2°C!\\n";
            }
        }

        if (TempFile.HasFile == true)
        {
            HttpFileCollection multipleFiles = Request.Files;
            for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
            {
                HttpPostedFile uploadedFile = multipleFiles[fileCount];
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }
                if (fileData.Length > 3145728)
                {
                    rtn += "上傳溫度資料檔案:此檔案" + uploadedFile.FileName + "超過3M!\\n";
                }
            }
        }

        return rtn;

    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        int VaccineInID;
        HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["I"], out VaccineInID));
        Response.Redirect("New_VaccineOutDataList.aspx?ID=" + VaccineInID);
    }
}