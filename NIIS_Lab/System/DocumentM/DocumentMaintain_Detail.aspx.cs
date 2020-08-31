using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class DocumentManagementM_DocumentMaintain_Detail : BasePage
{
    public new int ID = 0;
    public string FileList { get; set; }
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));
    List<MyPowerVM> list = new List<MyPowerVM>();
    new bool IsValid = true;

    public DocumentManagementM_DocumentMaintain_Detail()
    {
        list = base.AddPower("/System/DocumentM/DocumentMaintain.aspx", MyPowerEnum.修改,MyPowerEnum.刪除);
    }
    protected new void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        UpdatePower = base.GetPower(list[0]);
        DeletePower = base.GetPower(list[1]);

        if (Request.HttpMethod.Equals("POST"))
        {

            ID = GetNumber<int>("i");

            if (ID == 0)
            {
                IsValid = false;
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


            //if (this.IsPostBack == false)
            //{
            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_DocumentM_xGetDocByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocumentInfoID", ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }
                }
            }

            DocumentInfoVM VM = new DocumentInfoVM();
            List<DocumentFileInfoVM> fileList = new List<DocumentFileInfoVM>();
            EntityS.FillModel(VM, ds.Tables[0]);
            EntityS.FillModel(fileList, ds.Tables[1]);
            VM.FileList = fileList;

            if (this.IsPostBack == false)
            {

                lblDate.Text = VM.PublishStartDate.ToShortTaiwanDate();
                tbTitle.Text = VM.DocTitle;
                tbDesp.Text = VM.DocDescription;

                if (VM.publishState == 1)
                {
                    rb1.Checked = true;
                }
                else
                {
                    rb2.Checked = true;
                }
            }

            FileList = JsonConvert.SerializeObject(VM.FileList);
           
            if(UpdatePower.HasPower ==false)
            {
                tbTitle.Enabled = false;
                tbFile.Enabled = false;
                tbDesp.Enabled = false;
                rb1.Enabled = false;
                rb2.Enabled = false;
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;

        string checkScript = "";
        string message = CheckValid();
        if (message.Length > 0)
        {
            checkScript = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", checkScript, false);
            return;
        }

        string title = PureString(tbTitle.Text);
        string description = PureString(tbDesp.Text);

        int state = 0;
        RadioButton thisRb = null;
        thisRb = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (thisRb != null)
        {
            switch (thisRb.ID)
            {
                case "rb1":
                    state = 1;
                    break;
                case "rb2":
                    state = 2;
                    break;
            }
        }

        UserVM user = AuthServer.GetLoginUser();
        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
        int Chk = 0;

        List<int> OutFileInfoID_List = new List<int>();
        StringBuilder errorSb = new StringBuilder();
        string errMsg = "";

        if (tbFile.HasFile == true)
        {
            List<string> list = new List<string>()
                {
                    "application/pdf",
                    "application/msword",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "application/vnd.ms-excel",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };

            HttpFileCollection multipleFiles = Request.Files;
            for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
            {

                //string fileName = Path.GetFileName(uploadedFile.FileName);
                //if (uploadedFile.ContentLength > 0)
                //{
                //    uploadedFile.SaveAs(Server.MapPath("~/Files/") + fileName);
                //    Label1.Text += fileName + "Saved <BR>";
                //}

                HttpPostedFile uploadedFile = multipleFiles[fileCount];
                string extension = "";
                //string[] ary = tbFile.FileName.Split('.');
                string[] ary = uploadedFile.FileName.Split('.');
                if (ary.Length > 1)
                {
                    extension = ary.Last();
                }

                if (list.Contains(uploadedFile.ContentType) == false)
                {
                    errorSb.Append("alert('上傳格式限PDF、WORD、EXCEL');");
                }

                if (uploadedFile.ContentLength > 3 * 1024 * 1024)
                {
                    errorSb.Append("alert('大小限3M以內');");
                }
                errMsg = errorSb.ToString();
                if (errMsg.Length > 0)
                    break;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }
                
                NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient();
                string contentType = tbFile.PostedFile.ContentType;
                OutFileInfoID = WS.UploadFile(1, contentType, extension, uploadedFile.FileName, user.ID, user.RoleID, fileData);

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

        if (errMsg.Length > 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", "<script>" + errMsg + "</script>", false);
            return;
        }

        string OutFileInfoIDs = string.Join(",", OutFileInfoID_List.Select(x => x.ToString()).ToArray());


        if (UploadFileSuccess == true)
        {

            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_DocumentM_xUpdateDocumentInfo"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@DocTitle", title },
                                                    { "@PublishState", state },
                                                    { "@DocDescription", description },
                                                    { "@DocumentInfoID", ID },
                                                    { "@FileInfoIDs", OutFileInfoIDs }
                                            });

            Chk = (int)OutDict["@Chk"];

        }

        string script = "";
        if (UploadFileSuccess && Chk > 0)
        {
            script = string.Format("{0}<script>alert('儲存成功');location.href='{1}#{2}';</script>",
                     "<style>body{display:none;}</style>"
                     , "/System/DocumentM/DocumentMaintain.aspx"
                     , HttpUtility.HtmlDecode(GetString("hash") ?? "")
                   );
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }

    private string CheckValid()
    {
        StringBuilder sb = new StringBuilder();
        string date = PureString(lblDate.Text);
        string title = PureString(tbTitle.Text);
        bool HasFile = tbFile.HasFile;


        if (date.Length == 0)
        {
            sb.Append("發佈日期:必填\\n");
        }

        if (title.Length == 0)
        {
            sb.Append("標題:必填\\n");
        }

        //if (HasFile == false)
        //{
        //    rtn += "檔案名稱:必填\\n";
        //}


        RadioButton selected = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (selected == null)
        {
            sb.Append("上架狀態:必填");
        }
        return sb.ToString();

    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;

        int Chk = 0;
      

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_DocumentM_xDeleteDocumentInfoByID"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@DocumentInfoID", ID }
                                         });

        Chk = (int)OutDict["@Chk"];

        string script = "";

        if (Chk > 0)
        {
            script = string.Format("{0}<script>alert('儲存成功');location.href='{1}#{2}';</script>",
                     "<style>body{display:none;}</style>"
                     , "/System/DocumentM/DocumentMaintain.aspx"
                     , HttpUtility.HtmlDecode(GetString("hash") ?? "")
                   );

        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}