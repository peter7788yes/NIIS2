﻿using Newtonsoft.Json;
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

public partial class DocumentManagementM_DocumentMaintain_Update : BasePage
{
    public int ID = 0;

    public string FileList { get; set; }

    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public DocumentManagementM_DocumentMaintain_Update()
    {
        UpdatePower = base.AddPower("/System/DocumentManagementM/DocumentMaintain.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdatePower = base.GetPower(UpdatePower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass ="class='bodybg'";


        if (Request.HttpMethod.Equals("POST"))
        {
            int.TryParse(Request["i"], out ID);

            if (ID == 0)
            {
                string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
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
        string script = "";
        string message = CheckNeeded();
        if (message.Length > 0)
        {
            script = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }





        string title = tbTitle.Text.Trim();
        string description = tbDesp.Text.Trim();

        int state = 0;
        RadioButton thisRb = null;
        thisRb = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
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

        if (tbFile.HasFile == true)
        {
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

        string OutFileInfoIDs = string.Join(",", OutFileInfoID_List.Select(x => x.ToString()).ToArray());


        if (UploadFileSuccess == true)
        {

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_DocumentM_xUpdateDocumentInfo", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocTitle", title);
                    cmd.Parameters.AddWithValue("@PublishState", state);
                    cmd.Parameters.AddWithValue("@DocDescription", description);
                    cmd.Parameters.AddWithValue("@DocumentInfoID", ID);
                    //cmd.Parameters.AddWithValue("@FileInfoID", OutFileInfoID);
                    cmd.Parameters.AddWithValue("@FileInfoIDs", OutFileInfoIDs);

                    SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                    sp.Direction = ParameterDirection.Output;

                    sc.Open();
                    cmd.ExecuteNonQuery();

                    Chk = (int)sp.Value;
                }
            }


        }

        if (UploadFileSuccess && Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/System/DocumentM/DocumentMaintain.aspx';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }



        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }

    private string CheckNeeded()
    {
        string rtn = "";
        string date = lblDate.Text.Trim();
        string title = tbTitle.Text.Trim();
        bool HasFile = tbFile.HasFile;


        if (date.Length == 0)
        {
            rtn += "發佈日期:必填\\n";
        }

        if (title.Length == 0)
        {
            rtn += "標題:必填\\n";
        }

        //if (HasFile == false)
        //{
        //    rtn += "檔案名稱:必填\\n";
        //}


        RadioButton selected = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (selected == null)
        {
            rtn += "上架狀態:必填";
        }
        return rtn;

    }
}