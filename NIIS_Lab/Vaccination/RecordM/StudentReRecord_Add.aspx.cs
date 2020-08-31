using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_StudentReRecord_Add : BasePage
{
    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public string tbAry = "[]";
    public string sAry = "[]";
    public UserVM user = new UserVM();

    public Vaccination_RecordM_StudentReRecord_Add()
    {
       base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx",  MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        user = AuthServer.GetLoginUser();
        if (this.IsPostBack == false)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var dict = SystemCode.GetDict("RecordM_StudentRecord_VaccineCate");

            if (dict.Count > 0)
            {
                var query = dict.Where(item => list.Contains(item.EnumValue));
                tbAry = JsonConvert.SerializeObject(query);
            }

            int nowYear = DateTime.Now.Year - 1911;
            for(int i=0;i<5;i++)
            {
                ddlYear.Items.Add(new ListItem((nowYear-i).ToString() + "年", nowYear.ToString()));
            }

            if (SystemElementarySchool.list.Count > 0)
                sAry = JsonConvert.SerializeObject(SystemElementarySchool.list.Where(item => item.OrgID == user.OrgID));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int AdmissionYear = 0;
        int.TryParse(PureString(ddlYear.SelectedValue), out AdmissionYear);
        int SchoolID = GetNumber<int>("ss");
        int StudentYear = ddlST.SelectedIndex+1;

        int SignUserID = user.ID;
        int OrgID = user.OrgID;

        string VaccineTypeString = GetString("v");
        string InoculationNumberString = GetString("i");
        string ShouldInoculationNumberString = GetString("ss");

        bool IsValid = false;

        try
        {
            List<int> list = VaccineTypeString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
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

        if (IsValid == false && SchoolID>0)
        {
            string sc = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        string script = "";

        int Chk = 0;


        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddElementaryRecord"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ElementarySchoolID", SchoolID },
                                                    { "@AdmissionYear", AdmissionYear },
                                                    { "@StudentNumber", 0 },
                                                    { "@HasYellowCardNumber", 0 },
                                                    { "@SignUserID", user.ID },
                                                    { "@CreatedUserID", user.ID },
                                                    { "@StudentYear", StudentYear },
                                                    { "@InoculationType", 2 },
                                                    { "@VaccineTypeString", string.Join(",", VaccineTypeString) },
                                                    { "@InoculationNumberString", string.Join(",", InoculationNumberString) },
                                                    { "@ShouldInoculationNumberString", string.Join(",", ShouldInoculationNumberString) },
                                                    { "@OrgID", OrgID }
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');location.href = '/Vaccination/RecordM/StudentReRecord.aspx';</script>";
            SystemElementarySchool.Update();
        }
        else
        {
            switch (Chk)
            {
                case -1:
                    script = "<script>alert('一個學校一個年度一級別僅有一份數據');</script>";
                    break;
                default:
                    script = "<script>alert('儲存失敗');</script>";
                    break;
            }
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}