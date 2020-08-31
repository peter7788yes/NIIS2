using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneTools;

public partial class System_AccountM_AccountMaintain_Add : BasePage
{
    UserVM user = new UserVM();

    public System_AccountM_AccountMaintain_Add()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            tbDept.Text = user.OrgName;

            foreach (var item in SystemRole.list)
            {
                ListItem li = new ListItem();
                li.Text = item.RoleName;
                li.Value = item.ID.ToString();
                if (item.IsUserRole)
                {
                    li.Selected = true;
                }
                cbList.Items.Add(li);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string checkScript = "";
        string message = CheckValid();

        if (message.Length > 0)
        {
            checkScript = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", checkScript, false);
            return;
        }


        string Account = PureString(tbAccount.Text);
        string Name = PureString(tbName.Text);
        string Phone = PureString(tbPhone.Text);
        string Email = PureString(tbEmail.Text);
        string Title = PureString(tbTitle.Text);
        string R1 = PureString(tbR1.Text);
        string R2 = PureString(tbR2.Text);
        string RID = PureString(tbRID.Text);

        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
        int Chk = 0;

        bool IsBusiness = false;
        if (cbP.Checked == true)
            IsBusiness = true;

        List<String> checkList = new List<string>();
        foreach (ListItem item in cbList.Items)
        {
            if (item.Selected)
            {
                checkList.Add(item.Value);
            }
        }

        List<int> OutFileInfoID_List = new List<int>();
        StringBuilder errorSb = new StringBuilder();
        string errMsg = "";

        if (tbFile.HasFile == true)
        {
            List<string> list = new List<string>()
                {
                    "application/pdf",
                    "application/msword",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    //"application/vnd.ms-excel",
                    //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
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
                    extension = ary.Last().ToLower();
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

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_AccountM_xAddUserInfo"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                { "@LoginName", Account },
                                                { "@UserName", Name },
                                                { "@RocID", RID },
                                                { "@PhoneNumber", Phone },
                                                { "@Email", Email },
                                                { "@OrgID",  user.OrgID },
                                                { "@Title", Title },
                                                { "@ApplyReason", R1 },
                                                { "@ApplyRemark", R2 },
                                                { "@IsBusiness", IsBusiness },
                                                { "@CheckState", 1 },
                                                { "@SystemPowerString",  1023 },
                                                { "@EnableState", 1 },
                                                { "@CreatedUserID",  user.ID },
                                                { "@ApplyDate", DateTime.Now },
                                                { "@RoleIDs", string.Join(",", checkList) },
                                                { "@FileInfoIDs", OutFileInfoIDs }

                                    });

        Chk = (int)OutDict["@Chk"];

        string script = "";
        if (Chk > 0)
        {
            script = "</script><style>body{display:none;}</style><script>alert('儲存成功');location.href = '/System/AccountM/AccountMaintain.aspx?i=" + ID+ "';</script>";
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
        string Account = PureString(tbAccount.Text);
        string Name = PureString(tbName.Text);
        string RID = PureString(tbRID.Text);
        string Phone = PureString(tbPhone.Text);
        string Email = PureString(tbEmail.Text);
        string Title = PureString(tbTitle.Text);
        string R1 = PureString(tbR1.Text);
        string R2 = PureString(tbR2.Text);
  
        bool HasFile = tbFile.HasFile;

        ValidT VT = new ValidT();
       
        if (Account.Length == 0)
        {
            sb.Append("帳號:必填\\n");
        }

        if (Name.Length == 0)
        {
            sb.Append("姓名:必填\\n");
        }

        if (RID.Length == 0)
        {
            sb.Append("身分證號:必填\\n");
        }

        if (VT.CheckRocID(RID) == false)
        {
            sb.Append("身分證號:不合法\\n");
        }

        if (Phone.Length == 0)
        {
            sb.Append("電話:必填\\n");
        }

        if (Email.Length == 0)
        {
            sb.Append("電子信箱:必填\\n");
        }

        if (VT.CheckEmail(Email) == false)
        {
            sb.Append("電子信箱:不合法\\n");
        }

        List<ListItem> list = cbList.Items.Cast<ListItem>()
                                             .Where(li => li.Selected)
                                             .ToList();

        if (list.Count == 0)
        {
            sb.Append("所屬角色:必填\\n");
        }

        if (R1.Length == 0)
        {
            sb.Append("申請事由:必填\\n");
        }
        
        return sb.ToString();

    }
}