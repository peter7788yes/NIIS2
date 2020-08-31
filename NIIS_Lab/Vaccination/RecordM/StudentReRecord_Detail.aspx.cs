using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_RecordM_StudentReRecord_Detail : BasePage
{
    public int ElementaryRecordID = 0;
    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public string tbAry = "[]";
    public string tbAry2 = "[]";
    public UserVM user = new UserVM();
    public int StudentNumber = 0;
    public int HasYellowCardNumber = 0;
    new bool IsValid = true;

    public Vaccination_RecordM_StudentReRecord_Detail()
    {
        base.AddPower("/Vaccination/RecordM/StudentRecord.aspx", MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        ElementaryRecordID = GetNumber<int>("i");

        if (ElementaryRecordID == 0)
        {
            IsValid = false;
            string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        user = AuthServer.GetLoginUser();
        if (this.IsPostBack==false)
        {
            DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_RecordM_xGetElementaryRecordByID"
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ElementaryRecordID", ElementaryRecordID }
                                         });

            ElementaryRecordVM VM = new ElementaryRecordVM();
            EntityS.FillModel(VM, ds.Tables[0]);
            List<ElementaryRecordDataVM> list = new List<ElementaryRecordDataVM>();
            EntityS.FillModel(list, ds.Tables[1]);

            for (int i = VM.AdmissionYear; i <= DateTime.Now.Year - 1911; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }


            var dict = SystemCode.GetDict("RecordM_StudentRecord_VaccineCate");
            if (dict.Count > 0)
                tbAry = JsonConvert.SerializeObject(dict);
            if (list.Count > 0)
                tbAry2 = JsonConvert.SerializeObject(list);

            ddlST.SelectedValue = VM.StudentYear.ToString();

            var slist = SystemElementarySchool.list.Where(item => item.OrgID == user.OrgID);
            ddlSchool.Items.Add(new ListItem("請選擇學校名稱", "0"));
            foreach (var item in slist)
            {
                ddlSchool.Items.Add(new ListItem(item.SchoolName, item.ElementarySchoolID.ToString()));
            }

            ddlSchool.SelectedValue = VM.ElementarySchoolID.ToString();
        }

        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.IsValid == false)
            return;

        int AdmissionYear = 0;
        int.TryParse(ddlYear.SelectedValue, out AdmissionYear);
        int SchoolID = 0;
        int.TryParse(ddlSchool.SelectedValue, out AdmissionYear);

        int HasYellowCardNumber = 0;
        int StudentNumber = 0;

        int SignUserID = user.ID;
        int OrgID = user.OrgID;


        int.TryParse(ddlYear.SelectedValue, out AdmissionYear);
        int.TryParse(ddlSchool.SelectedValue, out SchoolID);

        string ElementaryRecordDataIDs = GetString("vv");
        string InoculationNumberString = GetString("ii");
        string ShouldInoculationNumberString = GetString("ss");

        bool IsValid = false;

        try
        {
            List<int> list = ElementaryRecordDataIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            list = InoculationNumberString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
               .ToList<string>()
               .ConvertAll<int>(item => int.Parse(item));

            list = ShouldInoculationNumberString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
              .ToList<string>()
              .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }

        if (IsValid == false || SchoolID <= 0)
        {
            string sc = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        string script = "";

        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xUpdateElementaryRecord"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ElementarySchoolID", SchoolID },
                                                    { "@AdmissionYear", AdmissionYear },
                                                    { "@StudentNumber", StudentNumber },
                                                    { "@HasYellowCardNumber", HasYellowCardNumber },
                                                    { "@SignUserID", user.ID },
                                                    { "@StudentYear", 1 },
                                                    { "@InoculationType", 2 },
                                                    { "@ElementaryRecordID", ElementaryRecordID },
                                                    { "@ElementaryRecordDataIDs", string.Join(",", ElementaryRecordDataIDs) },
                                                    { "@InoculationNumberString", string.Join(",", InoculationNumberString) },
                                                    { "@ShouldInoculationNumberString", string.Join(",", ShouldInoculationNumberString) },
                                                    { "@OrgID", OrgID }
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');location.href = '/Vaccination/RecordM/StudentReRecord.aspx';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}