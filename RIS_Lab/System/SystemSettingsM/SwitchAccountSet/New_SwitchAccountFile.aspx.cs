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

public partial class System_SystemSettingsM_SwitchAccountSet_New_SwitchAccountFile : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_SystemSettingsM_SwitchAccountSet_New_SwitchAccountFile()
    {
        list = base.AddPower("/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSet.aspx", MyPowerEnum.新增, MyPowerEnum.上傳);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        UploadPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
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

        if (AccountFile.HasFile == true)
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
                string contentType = AccountFile.PostedFile.ContentType;
                OutFileInfoID = WS.UploadFile(12, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

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

        int OrgID = 0;
        string accountFileDesc = AccountFileDesc.Text.Trim();

        int.TryParse(Request.QueryString["OrgID"], out OrgID);

        int Success = 0;

        if (UploadFileSuccess == true)
        {
            DataSet ds = new DataSet();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_AccountSet_xAddAccountFileData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    cmd.Parameters.AddWithValue("@AccountFile", OutFileInfoIDs);
                    cmd.Parameters.AddWithValue("@AccountFileDesc", accountFileDesc);
                    cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                    SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                    sp.Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                        Success = (int)sp.Value;
                    }
                }
            }
        }

        if (UploadFileSuccess && Success > 0)
        {
            script = "<script>alert('儲存成功');window.opener.refresh(" + OrgID + ");window.close();</script>";
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

        if (AccountFile.HasFile == false)
        {
            rtn += "檔案:必填!\\n";
        }
        else if (AccountFile.HasFile == true)
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
                     rtn += "檔案:此檔案" + uploadedFile.FileName + "超過3M!\\n";
                 }
             }
        }

        return rtn;
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        int OrgID = 0;
        string script = "";

        int.TryParse(Request.QueryString["OrgId"], out OrgID);

        script = "<script>window.opener.refresh("+OrgID+");window.close();</script>";

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "window", script, false);
    }
}