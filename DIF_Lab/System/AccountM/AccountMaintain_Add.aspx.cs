using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_AccountM_AccountMaintain_Add : BasePage
{

    UserVM user = new UserVM();
    public System_AccountM_AccountMaintain_Add()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
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
                                                { "@SystemPowerString",  1 },
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
            script = "<script>alert('儲存成功');location.href = '/System/AccountM/AccountMaintain.aspx?i=" + ID+ "';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}