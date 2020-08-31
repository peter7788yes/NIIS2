﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_DocumentManagementM_DocumentMaintain_Add : BasePage
{
    public System_DocumentManagementM_DocumentMaintain_Add()
    {
        base.AddPower("/System/DocumentM/DocumentMaintain.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (this.IsPostBack == false)
        {
            lblDate.Text = DateTime.Now.ToShortTaiwanDate();
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



       

        string title = PureString(tbTitle.Text);
        string description = PureString(tbDesp.Text);

        int state = 0;
        RadioButton thisRb= null;
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
                OutFileInfoID = WS.UploadFile(1, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

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

            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_DocumentM_xAddDocumentInfo"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                { "@DocTitle", title },
                                                { "@PublishState", state },
                                                { "@DocDescription", description },
                                                { "@CreatedUserID",  user.ID },
                                                { "@FileInfoIDs", OutFileInfoIDs }
                                            });

            Chk = (int)OutDict["@Chk"];
           
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
        string date = PureString(lblDate.Text);
        string title = PureString(tbTitle.Text);
        bool HasFile = tbFile.HasFile;


        if (date.Length==0)
        {
            rtn += "發佈日期:必填\\n";
        }

        if (title.Length == 0)
        {
            rtn += "標題:必填\\n";
        }

        if (HasFile==false)
        {
            rtn += "檔案名稱:必填\\n";
        }


        RadioButton selected = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (selected == null)
        {
            rtn += "上架狀態:必填";
        }
        return rtn;
       
    }
}
