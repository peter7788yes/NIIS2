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

public partial class System_ElectronBulletinM_NewsPublished_Modify_NewsPublished : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_ElectronBulletinM_NewsPublished_Modify_NewsPublished()
    {
        list = base.AddPower("/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx", MyPowerEnum.修改, MyPowerEnum.刪除, MyPowerEnum.上傳);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(list[0]);
        DeletePower = base.AddPower(list[1]);
        UploadPower = base.AddPower(list[2]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        OrgName.Text = HttpUtility.HtmlEncode(Request.Form[OrgName.UniqueID]);
        if (this.IsPostBack == false)
        {
            PublishedStarDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            PublishedStarDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + PublishedStarDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            PublishedEndDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            PublishedEndDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + PublishedEndDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");

            int ID = 0;

            int.TryParse(Request.QueryString["CheckID"], out ID);

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xGetNewsPublishedData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                ReleaseDate.Text = dt.Rows[0]["CreateDate"].ToString();
                ReleaseOrg.Text = SystemOrg.GetName(int.Parse(dt.Rows[0]["OrgID"].ToString()));
                Subject.Text = dt.Rows[0]["Subject"].ToString();
                Contents.Text = dt.Rows[0]["Contents"].ToString();
                PublishedStarDate.Text = dt.Rows[0]["PublishedStarDate"].ToString();
                PublishedEndDate.Text = dt.Rows[0]["PublishedEndDate"].ToString();
                EmailCheck.Checked = Convert.ToBoolean(dt.Rows[0]["EmailCheck"].ToString());
                if (EmailCheck.Checked == true)
                {
                    OrgName.Visible = true;
                    OrgImg.Visible = true;
                    OrgID.Value = dt.Rows[0]["EmailOrg"].ToString();
                    OrgType.Value = dt.Rows[0]["EmailOrgType"].ToString();
                    string[] Temp = OrgID.Value.Split(',');
                    if (OrgType.Value == "1")
                    {
                        for (var i = 0; i < Temp.Length; i++)
                        {
                            if (Temp[i] == "1")
                            {
                                OrgName.Text = OrgName.Text + "CDC" + ",";
                            }
                            if (Temp[i] == "2")
                            {
                                OrgName.Text = OrgName.Text + "區管中心" + ",";
                            }
                            if (Temp[i] == "3")
                            {
                                OrgName.Text = OrgName.Text + "局" + ",";
                            }
                            if (Temp[i] == "4")
                            {
                                OrgName.Text = OrgName.Text + "所" + ",";
                            }
                            if (Temp[i] == "5")
                            {
                                OrgName.Text = OrgName.Text + "院" + ",";
                            }
                        }
                    }
                    else if (OrgType.Value == "2")
                    {
                        for (var i = 0; i < Temp.Length; i++)
                        {
                            OrgName.Text = OrgName.Text + SystemOrg.GetName(int.Parse(Temp[i])) + ",";
                        }
                    }
                    int index = OrgName.Text.LastIndexOf(",");
                    OrgName.Text = OrgName.Text.Remove(index);
                }
            }

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
                OutFileInfoID = WS.UploadFile(2, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

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

        string subject = Subject.Text.Trim();
        string contents = Contents.Text.Trim();
        string publishedStarDate = (Convert.ToInt32(PublishedStarDate.Text.Substring(0, 3)) + 1911).ToString() + "/" + PublishedStarDate.Text.Substring(3, 2) + "/" + PublishedStarDate.Text.Substring(5, 2);
        string publishedEndDate = (Convert.ToInt32(PublishedEndDate.Text.Substring(0, 3)) + 1911).ToString() + "/" + PublishedEndDate.Text.Substring(3, 2) + "/" + PublishedEndDate.Text.Substring(5, 2);
        bool emailCheck = EmailCheck.Checked;
        string orgID = "";
        int orgType = 0;
        if (emailCheck == true)
        {
            orgID = OrgID.Value;
            int.TryParse(OrgType.Value, out orgType);
        }

        if (UploadFileSuccess == true)
        {
            int ID = 0;

            int.TryParse(Request.QueryString["CheckID"], out ID);

            DataSet ds = new DataSet();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xUpdateNewsPublished", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@Contents", contents);
                    cmd.Parameters.AddWithValue("@FileInfoIDs", OutFileInfoIDs);
                    cmd.Parameters.AddWithValue("@PublishedStarDate", publishedStarDate);
                    cmd.Parameters.AddWithValue("@PublishedEndDate", publishedEndDate);
                    cmd.Parameters.AddWithValue("@EmailCheck", emailCheck);
                    cmd.Parameters.AddWithValue("@EmailOrg", orgID);
                    cmd.Parameters.AddWithValue("@EmailOrgType", orgType);
                    cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
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
            script = "<script>alert('儲存成功');location.href = '/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int ID = 0;
        int Success = 0;
        string script = "";

        int.TryParse(Request.QueryString["CheckID"], out ID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xDeleteNewsPublished", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();
                Success = (int)sp.Value;

            }
        }
        if (Success > 0)
        {
            script = "<script>alert('刪除成功');location.href = '/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx';</script>";
        }
        else
        {
            script = "<script>alert('刪除失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    protected void EmailCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (EmailCheck.Checked == true)
        {
            OrgName.Visible = true;
            OrgImg.Visible = true;
        }
        else
        {
            OrgName.Visible = false;
            OrgImg.Visible = false;
        }
    }
    private string CheckNeeded()
    {
        string rtn = "";
        string subject = Subject.Text.Trim();
        string contents = Contents.Text.Trim();
        string publishedStarDate = PublishedStarDate.Text.Trim();
        string publishedEndDate = PublishedEndDate.Text.Trim();
        string orgName = OrgName.Text.Trim();
        string orgID = OrgID.Value.ToString().Trim();
        string orgType = OrgType.Value.ToString().Trim();

        if (subject.Length == 0)
        {
            rtn += "主旨:必填!\\n";
        }

        if (contents.Length == 0)
        {
            rtn += "內容:必填!\\n";
        }

        if (publishedStarDate.Length == 0)
        {
            rtn += "上架日期的起始日期:必填!\\n";
        }

        if (publishedEndDate.Length == 0)
        {
            rtn += "上架日期的截止日期:必填!\\n";
        }

        if (EmailCheck.Checked == true)
        {
            if (orgName.Length == 0 || orgID.Length == 0 || orgType.Length == 0)
            {
                rtn += "電子郵件:必填!\\n";
            }
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
    protected void Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewsPublished.aspx");
    }
}