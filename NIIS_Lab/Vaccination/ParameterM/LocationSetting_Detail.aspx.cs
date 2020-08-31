using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using Newtonsoft.Json;
using System.Globalization;
using System.Web;
using System.Text;

public partial class Vaccination_ParameterM_LocationSetting_Detail : BasePage
{
    public string StateListAry = "[]";
    public string ddListOutAry = "[]";
    public int ContractID = 0;
    public new int ID = 0;
    public int County = 0;
    public int Town = 0;
    public int Village = 0;
    public string CountyName = "";
    public string TownName = "";
    public string VillageName = "";
    public string CountyJson = "";
    public string TownJson = "";
    public string VillageJson = "";
    public string tbOtherIDs = "";
    public string tbOther = "";
    public string AgencyState = "";
    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    new bool IsValid = true;

    public Vaccination_ParameterM_LocationSetting_Detail()
    {
        PowerList = base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        ViewPower = base.GetPower(PowerList[0]);
        UpdatePower = base.GetPower(PowerList[1]);

        //if (UpdatePower.HasPower)
        //{
        //    form1.DefaultButton = "btnSave";
        //}

        ID = GetNumber<int>("i");
        ContractID = GetNumber<int>("i2");

        if (ID == 0)
        {
            IsValid = false;
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        if(UpdatePower.HasPower==false)
        {
            tbAddress.Enabled = false;
            tbTel.Enabled = false;
            tbVaccine.Enabled = false;
            ddlAgState.Enabled = false;
            //tbDepartment.Enabled = false;
        }
        if (this.IsPostBack == false)
        {
            uc1.TableName = "O_OrgLog";
            uc1.WhereDict = new Dictionary<string, object>()
                                         {
                                              { "@OrgID", ID }
                                        };

            DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_ParameterM_xGetAgencyByID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@AgencyInfoID", ID }
                                        });

            AgencyInfoVM VM = new AgencyInfoVM();
            List<AddVaccineVM> list = new List<AddVaccineVM>();
            OrgContractVM VM2 = new OrgContractVM();
            List<DayTimeVM> ddListIn = new List<DayTimeVM>();
            List<DayTimeVM> ddListOut = new List<DayTimeVM>();
            EntityS.FillModel(VM, ds.Tables[0]);
            EntityS.FillModel(list, ds.Tables[1]);
            EntityS.FillModel(VM2, ds.Tables[2]);
            EntityS.FillModel(ddListIn, ds.Tables[3]);

            foreach(var item in ddListIn)
            {
                DayTimeVM inVM = new DayTimeVM();
                inVM.ID = item.ID;
                inVM.Monday = item.Monday;
                inVM.Tuesday = item.Tuesday;
                inVM.Wednesday = item.Wednesday;
                inVM.Thursday = item.Thursday;
                inVM.Friday = item.Friday;
                inVM.Saturday = item.Saturday;
                inVM.Sunday = item.Sunday;
                inVM.TimeAry = new List<Dictionary<string, string>>();
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    if(item.ID == Convert.ToInt32(dr["BusinesssDayID"].ToString()))
                    {
                        Dictionary<string, string> ssee = new Dictionary<string, string>();
                        ssee.Add("ss", dr["StartTime"].ToString().Substring(0, 5));
                        ssee.Add("ee", dr["StartTime"].ToString().Substring(0, 5));
                        inVM.TimeAry.Add(ssee);
                    }
                }

                ddListOut.Add(inVM);

            }


            if (ddListOut.Count > 0)
                ddListOutAry= JsonConvert.SerializeObject(ddListOut);

            if (VM2.ID>0)
            {
                ContractID = VM2.ID;
                tbDateStart.Text = VM2.ContractStart.ToShortTaiwanDate();
                tbDateEnd.Text = VM2.ContractEnd.ToShortTaiwanDate();
                tbDateStop.Text = VM2.ContractStop.ToShortTaiwanDate();

                if(tbDateStart.Text.Length==0)
                {
                    tbDateStart.Text=DateTime.Now.ToShortTaiwanDate();
                }
            }

            var dict = SystemCode.GetDict("LocationSettingM_Divisions");

            StateListAry = JsonConvert.SerializeObject(dict);

            var codes = SystemCode.dict["ParameterM_LocationSetting_AgencyState"];

            ddlAgState.Items.Add(new ListItem("請選擇", ""));
            foreach (var item in codes)
            {
                ddlAgState.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
            }

            ddlAgState.SelectedValue = VM.AgencyState.ToString();
            AgencyState = VM.AgencyState.ToString();

            lblBsState.Text = VM.BusinessStateString;
            //tbDepartment.Text = VM.Department;

            County = VM.AgencyCounty;
            Town = VM.AgencyTown;
            Village = VM.AgencyVillage;

            CountyName = SystemAreaCode.GetName(VM.AgencyCounty);
            TownName = SystemAreaCode.GetName(VM.AgencyTown);
            VillageName = SystemAreaCode.GetName(VM.AgencyVillage);

            tbAddress.Text = VM.AgencyAddress;
            tbTelZone.Text = VM.PhoneAreaCode;
            tbTel.Text = VM.AgencyPhoneNumber;
            tbSchedule.Text = VM.InoculationSchedule;
            ddlAgencyCate.SelectedValue = VM.AgencyCate.ToString();

            lblName.Text = VM.AgencyName;
            lblCode.Text = VM.AgencyCode;
            tbOrg.Text = VM.OrgName;
            hfOrgID.Value = VM.OrgID.ToString();
            //tbDepartment.Text = VM.Department;

            tbVaccine.Text = string.Join(",", list.Select(item => item.VaccineCName));
            tbOther = VM.DepartmentOther;
            switch(VM.ReportingType)
            {
                case 1:
                    rb1.Checked = true;
                    break;
                case 2:
                    rb2.Checked = true;
                    break;
                case 3:
                    rb3.Checked = true;
                    break;
                case 4:
                    rb4.Checked = true;
                    break;
            }

            if(VM.IsSimpleFlu==false)
            {
                rbB1.Checked = true;
            }
            else
            {
                rbB2.Checked = true;
            }
            tbOtherIDs = VM.DepartmentIDs;
            CountyJson = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
            TownJson = JsonConvert.SerializeObject(SystemAreaCode.GetTownList(County));
            VillageJson = JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Town));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;

        int ReportingType = 0;
        bool IsSimpleFlu = false;
        RadioButton selected = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        switch(selected.ID)
        {
            case "rb1":
                ReportingType = 1;
                break;
            case "rb2":
                ReportingType = 2;
                break;
            case "rb3":
                ReportingType = 3;
                break;
            case "rb4":
                ReportingType = 4;
                break;
        }

        if (rbB1.Checked == true)
        {
            IsSimpleFlu = true;
        }
        else
        {
            IsSimpleFlu = false;
        }

        int State = 0;
        int.TryParse(ddlAgState.SelectedValue,out State);
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
        string DepartmentIDs  = GetString("did");
        string DepartmentOther = GetString("tbOther");
        //string Department = tbDepartment.Text.Trim();
        int AgencyCate = 0;
        int.TryParse(ddlAgencyCate.SelectedValue, out AgencyCate);
        string Address = PureString(tbAddress.Text);
        string PhoneAreaCode = PureString(tbTelZone.Text);
        string Tel = PureString(tbTel.Text);
        string InoculationSchedule  = PureString(tbSchedule.Text);

        int Address1 = GetNumber<int>("SelectCounty");
        int Address2 = GetNumber<int>("SelectTown");
        int Address3 = GetNumber<int>("SelectVillage");

        string Name = PureString(lblName.Text);
        string Code = PureString(lblCode.Text);
        string Org = PureString(tbOrg.Text);
        int OrgID = 0;

        int.TryParse(hfOrgID.Value, out OrgID);

        string VaccineIDs = PureString(hfVaccineIDs.Value);

        string DateStart = PureString(tbDateStart.Text);
        string DateEnd = PureString(tbDateEnd.Text);
        string DateStop = PureString(tbDateStop.Text);

        DateTime ContractStart = default(DateTime);
        bool ContractStartSuccess = DateTime.TryParseExact(DateStart.RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out ContractStart);

        DateTime ContractEnd = default(DateTime);
        bool ContractEndSuccess = DateTime.TryParseExact(DateEnd.RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out ContractEnd);

        DateTime ContractStop = default(DateTime);
        bool ContractStopSuccess =  DateTime.TryParseExact(DateStop.RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out ContractStop);


        string daytime = HttpUtility.UrlDecode(Request.Form["daytime"] ?? "");
        List<DayTimeVM> list = JsonConvert.DeserializeObject<List<DayTimeVM>>(daytime);
        string BusinesssDays = "";
        string BusinesssTimes = "";
        List<string> week = new List<string>();
        if (list != null)
        {
            foreach (var item in list)
            {
                week.Add(item.Monday == true ? "1" : "0");
                week.Add(item.Tuesday == true ? "1" : "0");
                week.Add(item.Wednesday == true ? "1" : "0");
                week.Add(item.Thursday == true ? "1" : "0");
                week.Add(item.Friday == true ? "1" : "0");
                week.Add(item.Saturday == true ? "1" : "0");
                week.Add(item.Sunday == true ? "1" : "0");
                BusinesssDays += string.Join(",", week) + ";";
                week.Clear();
                foreach (var item2 in item.OutTimeAry)
                {
                    if(item2.ContainsKey("ss") && item2.ContainsKey("ee"))
                        BusinesssTimes += item2["ss"] + "," + item2["ee"] + ",";
                }
                BusinesssTimes = BusinesssTimes.Trim(',') + ";";
            }
        }

        //bool NeedToAddContaract = true;

        //if (ContractStart == default(DateTime) || ContractEnd == default(DateTime) || ContractStop == default(DateTime))
        //{
        //    NeedToAddContaract = false;
        //}
        BusinesssDays = BusinesssDays.Trim(';');
        BusinesssTimes = BusinesssTimes.Trim(';');

        var length = BusinesssTimes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length;

        BusinesssDays = string.Join(";", BusinesssDays.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Take(length));

        var dict = new Dictionary<string, object>()
                                         {
                                                    { "@AgencyInfoID", ID },
                                                    { "@AgencyName", Name },
                                                    { "@AgencyState", State },
                                                    { "@AgencyCounty", Address1 },
                                                    { "@AgencyTown", Address2 },
                                                    { "@AgencyVillage", Address3 },
                                                    { "@AgencyAddress", Address },
                                                    { "@AgencyPhoneNumber", Tel },
                                                    { "@BusinessState", BsState },
                                                    { "@AgencyCode", Code },
                                                    { "@OrgID", OrgID },
                                                    { "@DepartmentIDs", DepartmentIDs },
                                                    { "@DepartmentOther", DepartmentOther },
                                                    { "@ReportingType", ReportingType },
                                                    { "@IsSimpleFlu", IsSimpleFlu },
                                                    { "@VaccineIDs", VaccineIDs },
                                                    { "@InoculationSchedule", InoculationSchedule },
                                                    { "@ContractID", ContractID },
                                                    { "@AgencyCate", AgencyCate },
                                                    { "@PhoneAreaCode", PhoneAreaCode },
                                                    { "@BusinesssDays",  BusinesssDays },
                                                    { "@BusinesssTimes", BusinesssTimes },
                                                    //{ "@ContractStart", ContractStart  },
                                                    //{ "@ContractEnd", ContractEnd  },
                                                    //{ "@ContractStop", ContractStop  }
                                            };

        if (ContractStartSuccess == true)
            dict.Add("@ContractStart", ContractStart);

        if (ContractEndSuccess == true)
            dict.Add("@ContractEnd", ContractEnd);

        if (ContractStopSuccess == true)
            dict.Add("@ContractStop", ContractStop);

        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_ParameterM_xUpdateAgency"
                                         , ref OutDict
                                         , dict);
        Chk = (int)OutDict["@Chk"];

        string script = "<style>body{display:none;}</style>";
        if (Chk > 0)
        {
            script += "<script>alert('儲存成功');location.href = '/Vaccination/ParameterM/LocationSetting.aspx#" + HttpUtility.HtmlDecode(GetString("hash") ?? "").TrimStart('#') + "';</script>";
        }
        else
        {
            script += "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}