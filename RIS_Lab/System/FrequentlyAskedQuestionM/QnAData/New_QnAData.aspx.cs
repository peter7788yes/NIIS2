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
using WayneEntity;

public partial class System_FrequentlyAskedQuestionM_QnAData_New_QnAData : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_FrequentlyAskedQuestionM_QnAData_New_QnAData()
    {
        list = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.新增, MyPowerEnum.上傳);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        UploadPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
                
        if (this.IsPostBack == false)
        {
            ReleaseDate.Text = DateTime.Now.ToShortTaiwanDate();
            ReleaseDate.Enabled = false;
            QnAType.PostBackUrl = "/System/FrequentlyAskedQuestionM/QnAData/New_QnAType.aspx?PageView=add1";
            QnAType.Text = "新增類別";

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_QnAType_xGetQnATypeAllData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }
            List<SystemCodeVM> TableList = new List<SystemCodeVM>();
            EntityS.FillModel(TableList, ds.Tables[0]);
            if (ds.Tables.Count > 0)
            {
                List<SystemCodeVM> SystemCodeList = TableList;
                foreach (SystemCodeVM sc in SystemCodeList) QuestionType.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            PublishedStatus1.Checked = true;
        }
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
        int Success = 0;

        List<int> OutFileInfoID_List = new List<int>();

        if (tbFile.HasFile == true)
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
                string contentType = tbFile.PostedFile.ContentType;
                OutFileInfoID = WS.UploadFile(3, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

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

        int questionType = int.Parse(QuestionType.SelectedValue);
        string question = Question.Text.Trim();
        string reply = Reply.Text.Trim();
        int publishedStatus = 0;

        if (PublishedStatus1.Checked == true)
        {
            publishedStatus = 1;
        }
        if (PublishedStatus2.Checked == true)
        {
            publishedStatus = 2;
        }

        if (UploadFileSuccess == true)
        {
            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_QnAData_xAddQnAData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuestionType", questionType);
                    cmd.Parameters.AddWithValue("@Question", question);
                    cmd.Parameters.AddWithValue("@Answer", reply);
                    cmd.Parameters.AddWithValue("@FileInfoIDs", OutFileInfoIDs);
                    cmd.Parameters.AddWithValue("@QaStatus", publishedStatus);
                    cmd.Parameters.AddWithValue("@CreatAccount", user.ID);
                    SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                    sp.Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        cmd.ExecuteNonQuery();
                        Success = (int)sp.Value;
                    }
                }
            }
        }

        if (UploadFileSuccess && Success > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);  
    }
    private string CheckNeeded()
    {
        string rtn = "";
        string releaseDate = ReleaseDate.Text.Trim();
        int questionType = int.Parse(QuestionType.SelectedValue);
        string question = Question.Text.Trim();
        string reply = Reply.Text.Trim();
        int publishedStatus = 0;

        if (releaseDate.Length == 0)
        {
            rtn += "發佈日期:必填!\\n";
        }

        if (questionType == 0)
        {
            rtn += "問題類別:必填!\\n";
        }

        if (question.Length == 0)
        {
            rtn += "問題:必填!\\n";
        }

        if (reply.Length == 0)
        {
            rtn += "回覆:必填!\\n";
        }

        if (PublishedStatus1.Checked == true)
        {
            publishedStatus = 1;
        }
        if (PublishedStatus1.Checked == true)
        {
            publishedStatus = 2;
        }
        if (publishedStatus == 0)
        {
            rtn += "上架狀態:必填!\\n";
        }
        if (tbFile.HasFile == true)
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
                     rtn += "附件:此檔案" + uploadedFile.FileName + "超過3M!\\n";
                 }
             }
        }

        return rtn;

    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("QnAData.aspx");
    }
}