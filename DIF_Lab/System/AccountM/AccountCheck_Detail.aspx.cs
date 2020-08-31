using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_AccountM_AccountCheck_Detail : BasePage
{
    public string DisplayFileName = "";
    public int FileInfoID = 0;
    public System_AccountM_AccountCheck_Detail()
    {
        base.AddPower("/System/AccountM/AccountCheck.aspx", MyPowerEnum.查詢);
    }
    public UserVM VM;
    public string DisplayFileNamesListJson = "[]";
    public string FileInfoIDsListJson = "[]";
    public string Roles = "[]";
    public int ID = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {

            ID = GetNumber<int>("i");

            if (this.IsPostBack == false)
            {

                DataTable dt = GetDataTable("ConnUser", "dbo.usp_AccountM_xGetAccountCheckUserInfoByID"
                                          , new Dictionary<string, object>()
                                          {
                                                { "@UserID", ID }
                                          });

                VM = new UserVM();
                EntityS.FillModel(VM, dt);

                tbRID.Text = VM.RocID.Trim();
                tbName.Text = VM.UserName;
                tbPhone.Text = VM.PhoneNumber;
                tbTitle.Text = VM.Title;
                tbEmail.Text = VM.Email;
                lblAccount.Text = VM.LoginName;
                tbDept.Text = VM.OrgNameByOrgID;
                rbList.SelectedValue = VM.CheckState.ToString();
                tbReason.Text = VM.ApplyReason;
                tbDesp.Text = VM.CheckDescription;
                //DisplayFileName = VM.DisplayFileName;
                DisplayFileNamesListJson = JsonConvert.SerializeObject(VM.DisplayFileNamesList);
                FileInfoIDsListJson = JsonConvert.SerializeObject(VM.FileInfoIDsList);
                //FileInfoID = VM.FileInfoID;
                if (VM.IsBusiness == true)
                {
                    cbP.Checked = true;
                }


                dt = GetDataTable("ConnUser", "dbo.usp_PowerM_xGetUserRoleByUserIDRoleCateID"
                                          , new Dictionary<string, object>()
                                          {
                                                { "@UserID", ID },
                                                { "@RoleCateID",1}
                                          });
                


                List<SystemRoleVM> vmList = new List<SystemRoleVM>();
                EntityS.FillModel(vmList, dt);
                List<int> userRoles = vmList.Select(item => item.ID).ToList();

                List<SystemRoleVM> outList = new List<SystemRoleVM>();
                foreach (var item in SystemRole.list)
                {
                    SystemRoleVM child = new SystemRoleVM();
                    child.ID = item.ID;
                    child.RoleName = item.RoleName;
                    if (userRoles.Contains(item.ID))
                    {
                        child.IsUserRole = true;
                    }
                    outList.Add(child);
                }

                // Roles = JsonConvert.SerializeObject(outList);

                foreach (var item in outList)
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
            // Create the list to store.
            //List<String> YrStrList = new List<string>();
            //// Loop through each item.
            //foreach (ListItem item in cbList.Items)
            //{
            //    if (item.Selected)
            //    {
            //        // If the item is selected, add the value to the list.
            //        YrStrList.Add(item.Value);
            //    }
            //    else
            //    {
            //        // Item is not selected, do something else.
            //    }
            //}
            //// Join the string together using the ; delimiter.
            //String YrStr = String.Join(";", YrStrList.ToArray());

            //// Write to the page the value.
            //Response.Write(String.Concat("Selected Items: ", YrStr));
        }
        else
        {
            Response.Write("");
            Response.End();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int CheckState = PureNumber<int>(rbList.SelectedValue);
        bool IsBusiness = cbP.Checked;
        string UserName = PureString(tbName.Text);
        string RocID = PureString(tbRID.Text);
        string PhoneNumber = PureString(tbPhone.Text); 
        string Email = PureString(tbEmail.Text); 
        string Title = PureString(tbTitle.Text);
        string ApplyReason = PureString(tbReason.Text); 
        string CheckDescription = PureString(tbDesp.Text);

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

            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_AccountM_xUpdateAccountInfo"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                { "@UserID", ID },
                                                { "@UserName", UserName },
                                                { "@RocID", RocID },
                                                { "@PhoneNumber", PhoneNumber },
                                                { "@Email", Email },
                                                { "@Title", Title },
                                                { "@RoleIDs", "" },
                                                { "@ApplyReason", ApplyReason },
                                                { "@CheckState", CheckState },
                                                { "@CheckDescription", CheckDescription },
                                                { "@IsBusiness", IsBusiness },
                                                { "@FileInfoIDs", OutFileInfoIDs }

                                        });

            Chk = (int)OutDict["@Chk"];
        }
        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/System/AccountM/AccountCheck.aspx?i=" + ID + "';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }

    //protected void lbDownload_Click(object sender, EventArgs e)
    //{
    //    int ID = 0;
    //    int.TryParse(Request["i"], out ID);

    //    DownloadVM VM = new DownloadVM(ID);

    //    var user = AuthServer.GetLoginUser();

    //    DataTable dt = new DataTable();
    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("dbo.usp_FileM_xGetFileInfoByID", sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@ID", VM.ID);
    //            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                sc.Open();
    //                da.Fill(dt);
    //            }
    //        }
    //    }

    //    if (dt.Rows.Count > 0)
    //    {
    //        string Json = JsonConvert.SerializeObject(VM);
    //        string code = QueryStringEncryptToolS.Encrypt(Json);

    //        string script = @"<script>windows.open</script>";
    //        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    //        //Response.Redirect("http://niis_fs.hyweb.com.tw/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
    //        //Response.Redirect("http://localhost:64351/livestorage.aspx?o=" + HttpUtility.UrlEncode(code));
    //        //Response.End();
    //    }
    //    else
    //    {
    //        Response.Redirect("~/html/ErrorPage/NoPower.html");
    //    }
    //}
}