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

public partial class Vaccination_CertificateM_ApplyData : BasePage
{

    public Vaccination_CertificateM_ApplyData()
    {
        base.AddPower("/Vaccination/CertificateM/PrintCertificate.aspx", MyPowerEnum.新增);
    }

    public int CaseUserID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        var user = AuthServer.GetLoginUser();

       
        int.TryParse(Request["i"], out CaseUserID);

        if (CaseUserID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script><body><style>body{display:none;}</style>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        if (this.IsPostBack == false)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CertificateM_xGetCaseUserNameByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                tbName.Text = dt.Rows[0]["ChName"].ToString();
                tbE.Text = dt.Rows[0]["EnName"].ToString();
            }
        }
       

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string script = "";


        string CaseUserChName = tbName.Text.Trim();
        string CaseUserEnName = tbE.Text.Trim();
        string ApplyUserName = tbA.Text.Trim();
        string UserRelationship = tbR.Text.Trim();
        var user = AuthServer.GetLoginUser();




        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
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

        int Chk = 0;
        if (UploadFileSuccess == true)
        {

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CertificateM_xAddApplyData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                    cmd.Parameters.AddWithValue("@CaseUserChName", CaseUserChName);
                    cmd.Parameters.AddWithValue("@CaseUserEnName", CaseUserEnName);
                    cmd.Parameters.AddWithValue("@ApplyUserName", ApplyUserName);
                    cmd.Parameters.AddWithValue("@UserRelationship", UserRelationship);
                    cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
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
}