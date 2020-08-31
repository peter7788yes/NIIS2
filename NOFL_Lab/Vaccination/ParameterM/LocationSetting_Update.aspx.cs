using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_ParameterM_LocationSetting_Update : BasePage
{
    public int ID = 0;
    public int County = 0;
    public int Town = 0;
    public int Village = 0;

    public string CountyJson = "";
    public string TownJson = "";
    public string VillageJson = "";

    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public Vaccination_ParameterM_LocationSetting_Update()
    {
        PowerList = base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewPower = base.GetPower(PowerList[0]);
        UpdatePower = base.GetPower(PowerList[1]);

        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["i"], out ID);

        if (ID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        if(UpdatePower.HasPower==false)
        {
            tbAddress.Enabled = false;
            tbTel.Enabled = false;
            tbVaccine.Enabled = false;
            ddAgState.Enabled = false;
            tbDepartment.Enabled = false;
        }

        if (this.IsPostBack == false)
        {
            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetAgencyByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AgencyInfoID", ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }
                }
            }


            AgencyInfoVM VM = new AgencyInfoVM();
            List<AddVaccineVM> list = new List<AddVaccineVM>();

            EntityS.FillModel(VM, ds.Tables[0]);
            EntityS.FillModel(list, ds.Tables[1]);



            var codes = SystemCode.dict["ParameterM_LocationSetting_AgencyState"];

            ddAgState.Items.Add(new ListItem("請選擇", "0"));
            foreach (var item in codes)
            {
                ddAgState.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
            }

            ddAgState.SelectedValue = VM.AgencyState.ToString();
            lblBsState.Text = VM.BusinessStateString;
            tbDepartment.Text = VM.Department;

            County = VM.AgencyCounty;
            Town = VM.AgencyTown;
            Village = VM.AgencyVillage;

           

            tbAddress.Text = VM.AgencyAddress;
            tbTel.Text = VM.AgencyPhoneNumber;

            lblName.Text = VM.AgencyName;
            lblCode.Text = VM.AgencyCode;
            tbOrg.Text = VM.OrgName;
            hfOrgID.Value = VM.OrgID.ToString();
            tbDepartment.Text = VM.Department;

            tbVaccine.Text = string.Join(",", list.Select(item => item.VaccineCName));

            CountyJson = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
            TownJson = JsonConvert.SerializeObject(SystemAreaCode.GetTownList(County));
            VillageJson = JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Town));
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        string State = ddAgState.Text.Trim();
        string BsStateString = lblBsState.Text.Trim();
        var codes = SystemCode.dict["ParameterM_LocationSetting_AgencyState"];
        int BsState = 0;
        foreach (var item in codes)
        {
            if (item.EnumName.Equals(BsStateString))
            {
                BsState = item.EnumValue;
                break;
            }

        }
        string Department = tbDepartment.Text.Trim();
        string Address = tbAddress.Text.Trim();
        string Tel = tbTel.Text.Trim();

        int Address1 = 0;
        int Address2 = 0;
        int Address3 = 0;

        int.TryParse(Request.Form["SelectCounty"], out Address1);
        int.TryParse(Request.Form["SelectTown"], out Address2);
        int.TryParse(Request.Form["SelectVillage"], out Address3);

        string Name = lblName.Text.Trim();
        string Code =  lblCode.Text.Trim();
        string Org =  tbOrg.Text.Trim();
        int OrgID = 0;

        int.TryParse(hfOrgID.Value, out OrgID);

        string VaccineIDs = hfVaccineIDs.Value;

        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xUpdateAgency", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgencyInfoID", ID);
                cmd.Parameters.AddWithValue("@AgencyName", Name);
                cmd.Parameters.AddWithValue("@AgencyState", State);
                cmd.Parameters.AddWithValue("@AgencyCounty", Address1);
                cmd.Parameters.AddWithValue("@AgencyTown", Address2);
                cmd.Parameters.AddWithValue("@AgencyVillage", Address3);
                cmd.Parameters.AddWithValue("@AgencyAddress", Address);
                cmd.Parameters.AddWithValue("@AgencyPhoneNumber", Tel);
                cmd.Parameters.AddWithValue("@BusinessState", BsState);
                cmd.Parameters.AddWithValue("@AgencyCode", Code);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@VaccineIDs", VaccineIDs);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/Vaccination/ParameterM/LocationSetting.aspx';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}