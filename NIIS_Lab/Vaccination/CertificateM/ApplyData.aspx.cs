using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_CertificateM_ApplyData : BasePage
{
    public int CaseUserID = 0;
    new bool IsValid = true;

    public Vaccination_CertificateM_ApplyData()
    {
        base.AddPower("/Vaccination/CertificateM/PrintCertificate.aspx", MyPowerEnum.新增);
    }
  
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            var user = AuthServer.GetLoginUser();

            CaseUserID = GetNumber<int>("i");

            if (CaseUserID == 0)
            {
                IsValid = false;
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            if (this.IsPostBack == false)
            {
                DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_CertificateM_xGetCaseUserNameByID"
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@CaseUserID", CaseUserID }
                                            });

                if (dt.Rows.Count > 0)
                {
                    tbName.Text = dt.Rows[0]["ChName"].ToString();
                    tbE.Text = dt.Rows[0]["EnName"].ToString();
                }
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
        string script = "";

        string CaseUserChName = PureString(tbName.Text);
        string CaseUserEnName = PureString(tbE.Text);
        string ApplyUserName = PureString(tbA.Text);
        string UserRelationship = PureString(tbR.Text);
        var user = AuthServer.GetLoginUser();

        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
        List<int> OutFileInfoID_List = new List<int>();
        StringBuilder errorSb = new StringBuilder();
        string errMsg = "";

        if (tbFile.HasFile == true)
        {
            List<string> list = new List<string>()
                {
                    //"application/pdf",
                    "application/msword",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "application/vnd.ms-excel",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };

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

                if (list.Contains(uploadedFile.ContentType) == false)
                {
                    errorSb.Append("alert('上傳格式限WORD、EXCEL');");
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

        if (errMsg.Length > 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", "<script>" + errMsg + "</script>", false);
            return;
        }

        string OutFileInfoIDs = string.Join(",", OutFileInfoID_List.Select(x => x.ToString()).ToArray());

        int ApplyFormat = 0;
        RadioButton selected = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        if (selected != null)
        {
            switch (selected.ID)
            {
                case "rb1":
                    ApplyFormat = 1;
                    break;
                case "rb2":
                    ApplyFormat = 2;
                    break;

            }
        }

        int Chk = 0;
        if (UploadFileSuccess == true)
        {
            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_CertificateM_xAddApplyData"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@CaseUserID", CaseUserID },
                                                    { "@CaseUserChName", CaseUserChName },
                                                    { "@CaseUserEnName", CaseUserEnName },
                                                    { "@ApplyUserName", ApplyUserName},
                                                    { "@UserRelationship", UserRelationship },
                                                    { "@CreatedUserID", user.ID },
                                                    { "@OrgID", user.OrgID },
                                                    { "@FileInfoIDs", OutFileInfoIDs },
                                                    { "@ApplyFormat", ApplyFormat },
                                            });

            Chk = (int)OutDict["@Chk"];

        }

        if (UploadFileSuccess && Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}