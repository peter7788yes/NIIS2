using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_BCGRecord_Detail : BasePage
{
    public int BCGRecordID = 0;
    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public UserVM user = new UserVM();
    public string tbAry = "[]";
    //public string iAry = "[]";
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    List<BCGRecordDataVM> list = new List<BCGRecordDataVM>();

    public int BirthNumber = 0;
    public int Baby = 0;
    public int Kid = 0;


    public Vaccination_RecordM_BCGRecord_Detail()
    {
        UpdatePower = base.AddPower("/Vaccination/RecordM/BCGRecord.aspx",MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdatePower = base.GetPower(UpdatePower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";


        int.TryParse(Request["i"], out BCGRecordID);

        if (BCGRecordID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        user = AuthServer.GetLoginUser();
       

        if (this.IsPostBack == false)
        {


            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetBCGRecordByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BCGRecordID", BCGRecordID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }
                }
            }

            BCGRecordVM VM = new BCGRecordVM();
            EntityS.FillModel(VM, ds.Tables[0]);
             list = new List<BCGRecordDataVM>();
            EntityS.FillModel(list, ds.Tables[1]);

            if(list.Count>0)
                tbAry = JsonConvert.SerializeObject(list);

            BirthNumber = VM.BirthNumber;
            Baby = VM.InoculationBabyNumber;
            Kid = VM.InoculationKidNumber;

            hf.Value = string.Join(",", list.ConvertAll<string>(item => item.ID.ToString()));

          

            tbOrg.Text = user.OrgName;
            int nowYear = DateTime.Now.Year - 1911;

            for (int i = VM.StatisticalYear; i <= DateTime.Now.Year - 1911; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }


            ddlSeason.Items.Add(new ListItem("第一季", "1"));
            ddlSeason.Items.Add(new ListItem("第二季", "2"));
            ddlSeason.Items.Add(new ListItem("第三季", "3"));
            ddlSeason.Items.Add(new ListItem("第四季", "4"));

            if(UpdatePower.HasPower==false)
            {
                ddlYear.Enabled = false;
                ddlSeason.Enabled = false;
                tbBirthNumber.Enabled = false;
                tbKid.Enabled = false;
                tbBaby.Enabled = false;
                tbBabyNoScar1.Enabled = false;
                tbBabyNoScar2.Enabled = false;
                tbBabyNoScar3.Enabled = false;
                tbKidNoScar1.Enabled = false;
                tbKidNoScar2.Enabled = false;
                tbKidNoScar3.Enabled = false;
                tbOtherNoScar1.Enabled = false;
                tbOtherHasScar1.Enabled = false;
                tbOtherNoScar2.Enabled = false;
                tbOtherHasScar2.Enabled = false;
                tbOtherNoScar3.Enabled = false;
            }
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
        List<string> BCGRecordDataIDsStringList = new List<string>();

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

        //BCGRecordDataIDsStringList = list.ConvertAll<string>(item => item.ID.ToString());


        bool IsValid = false;
        try
        {
            List<int> list = hf.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }

        if (IsValid == false)
        {
            string sc = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        string script = "";

        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xUpdateBCGRecord", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BCGRecordID", BCGRecordID);
                cmd.Parameters.AddWithValue("@StatisticalYear", StatisticalYear);
                cmd.Parameters.AddWithValue("@YearSeason", YearSeason);
                cmd.Parameters.AddWithValue("@SignUserID", user.ID);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@BirthNumber", BirthNumber);
                cmd.Parameters.AddWithValue("@InoculationKidNumber", InoculationKidNumber);
                cmd.Parameters.AddWithValue("@InoculationBabyNumber", InoculationBabyNumber);
                cmd.Parameters.AddWithValue("@BCGRecordDataIDsString", hf.Value);
                cmd.Parameters.AddWithValue("@KidTypeString",  string.Join(",", KidTypeStringList));
                cmd.Parameters.AddWithValue("@TestTypeString", string.Join(",", TestTypeStringList));
                cmd.Parameters.AddWithValue("@KidNumberSting", string.Join(",", KidNumberStingList));
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }


        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/Vaccination/RecordM/BCGRecord.aspx';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}