using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_BCGRecord_Add : BasePage
{
    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public UserVM user = new UserVM();

    public Vaccination_RecordM_BCGRecord_Add()
    {
        base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            tbOrg.Text = user.OrgName;
            int nowYear = DateTime.Now.Year - 1911;

            for(int i=0;i<2;i++)
            {
                ddlYear.Items.Add(new ListItem((nowYear-i).ToString() + "年", nowYear.ToString()));
            }

            ddlSeason.Items.Add(new ListItem("第一季", "1"));
            ddlSeason.Items.Add(new ListItem("第二季", "2"));
            ddlSeason.Items.Add(new ListItem("第三季", "3"));
            ddlSeason.Items.Add(new ListItem("第四季", "4"));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int StatisticalYear = 0;
        int YearSeason = 0;

        int SignUserID = user.ID;
        int OrgID = user.OrgID;

        int BirthNumber = 0;
        int InoculationKidNumber = 0;
        int InoculationBabyNumber = 0;


        int BabyNoScar1 = 0;
        int KidNoScar1 = 0;
        int OtherNoScar1 = 0;
        int OtherHasScar1 = 0;

        int BabyNoScar2 = 0;
        int KidNoScar2 = 0;
        int OtherNoScar2 = 0;
        int OtherHasScar2 = 0;


        int BabyNoScar3 = 0;
        int KidNoScar3 = 0;
        int OtherNoScar3 = 0;

        int.TryParse(ddlYear.SelectedValue, out StatisticalYear);
        int.TryParse(ddlSeason.SelectedValue, out YearSeason);

        int.TryParse(tbBirthNumber.Text, out BirthNumber);
        int.TryParse(tbKid.Text, out InoculationKidNumber);
        int.TryParse(tbBaby.Text, out InoculationBabyNumber);

        int.TryParse(tbBabyNoScar1.Text, out BabyNoScar1);
        int.TryParse(tbKidNoScar1.Text, out KidNoScar1);
        int.TryParse(tbOtherNoScar1.Text, out OtherNoScar1);
        int.TryParse(tbOtherHasScar1.Text, out OtherHasScar1);

        int.TryParse(tbBabyNoScar2.Text, out BabyNoScar2);
        int.TryParse(tbKidNoScar2.Text, out KidNoScar2);
        int.TryParse(tbOtherNoScar2.Text, out OtherNoScar2);
        int.TryParse(tbOtherHasScar2.Text, out OtherHasScar2);

        int.TryParse(tbBabyNoScar3.Text, out BabyNoScar3);
        int.TryParse(tbKidNoScar3.Text, out KidNoScar3);
        int.TryParse(tbOtherNoScar3.Text, out OtherNoScar3);

        List<string> KidTypeStringList = new List<string>();
        List<string> TestTypeStringList = new List<string>();
        List<string> KidNumberStingList = new List<string>();

        KidTypeStringList.Add(1.ToString()); TestTypeStringList.Add(1.ToString()); KidNumberStingList.Add(BabyNoScar1.ToString());
        KidTypeStringList.Add(1.ToString()); TestTypeStringList.Add(2.ToString()); KidNumberStingList.Add(BabyNoScar2.ToString());
        KidTypeStringList.Add(1.ToString()); TestTypeStringList.Add(3.ToString()); KidNumberStingList.Add(BabyNoScar3.ToString());


        KidTypeStringList.Add(2.ToString()); TestTypeStringList.Add(1.ToString()); KidNumberStingList.Add(KidNoScar1.ToString());
        KidTypeStringList.Add(2.ToString()); TestTypeStringList.Add(2.ToString()); KidNumberStingList.Add(KidNoScar2.ToString());
        KidTypeStringList.Add(2.ToString()); TestTypeStringList.Add(3.ToString()); KidNumberStingList.Add(KidNoScar3.ToString());

        KidTypeStringList.Add(3.ToString()); TestTypeStringList.Add(1.ToString()); KidNumberStingList.Add(OtherNoScar1.ToString());
        KidTypeStringList.Add(3.ToString()); TestTypeStringList.Add(2.ToString()); KidNumberStingList.Add(OtherNoScar2.ToString());
        KidTypeStringList.Add(3.ToString()); TestTypeStringList.Add(3.ToString()); KidNumberStingList.Add(OtherNoScar3.ToString());


        KidTypeStringList.Add(4.ToString()); TestTypeStringList.Add(1.ToString()); KidNumberStingList.Add(OtherHasScar1.ToString());
        KidTypeStringList.Add(4.ToString()); TestTypeStringList.Add(2.ToString()); KidNumberStingList.Add(OtherHasScar2.ToString());

        string script = "";

        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddBCGRecord"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                     { "@StatisticalYear", StatisticalYear },
                                                     { "@YearSeason", YearSeason },
                                                     { "@SignUserID", user.ID },
                                                     { "@OrgID", user.OrgID },
                                                     { "@BirthNumber", BirthNumber },
                                                     { "@InoculationKidNumber", InoculationKidNumber },
                                                     { "@InoculationBabyNumber", InoculationBabyNumber },
                                                     { "@CreatedUserID", user.ID },
                                                     { "@KidTypeString",  string.Join(",", KidTypeStringList) },
                                                     { "@TestTypeString", string.Join(",", TestTypeStringList) },
                                                     { "@KidNumberSting", string.Join(",", KidNumberStingList) }
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');location.href = '/Vaccination/RecordM/BCGRecord.aspx';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}