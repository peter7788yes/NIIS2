﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_CodeM_SchoolCode_Detail : BasePage
{
    public string StateListAry = "[]";
    public new int ID = 0;
    public string County = "";
    public string Town = "";
    public string Village = "";
    public string CountyName = "";
    public string TownName = "";
    public string VillageName = "";

    public string CountyJson = "[]";
    public string TownJson = "[]";
    public string VillageJson = "[]";
    public string tbOtherIDs = "";
    public string tbOther = "";

    public MedicalCodeVM VM = new MedicalCodeVM();
    public int ElementarySchoolID = 0;

    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    new bool IsValid = true;

    public System_CodeM_SchoolCode_Detail()
    {
        PowerList = base.AddPower("/System/CodeM/SchoolCode.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {

            base.AllowHttpMethod("POST");
            base.DisableTop(true);

            ViewPower = base.GetPower(PowerList[0]);
            UpdatePower = base.GetPower(PowerList[1]);

            ElementarySchoolID = GetNumber<int>("i");

            if (ElementarySchoolID == 0)
            {
                IsValid = false;
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }
           
                uc1.TableName = "O_OrgLog";
                uc1.WhereDict = new Dictionary<string, object>()
                                         {
                                              { "@OrgID", ID }
                                        };

                DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_CodeM_xGetElementarySchoolByID"
                                                     , new Dictionary<string, object>()
                                                     {
                                                     { "@ElementarySchoolID", ElementarySchoolID }
                                                    });
                SchoolCodeVM VM = new SchoolCodeVM();
                EntityS.FillModel(VM, dt);

                if (this.IsPostBack == false)
                {
                    tbName.Text = VM.SchoolName;
                    tbCode.Text = VM.SchoolCode;
                    tbTel.Text = VM.SchoolPhoneNumber;
                    tbAddress.Text = VM.SchoolAddress;
                }

                County = VM.SchoolCounty.ToString();
                Town = VM.SchoolTown.ToString();
                Village = VM.SchoolVillage.ToString();

                CountyName = SystemAreaCode.GetName(VM.SchoolCounty);
                TownName = SystemAreaCode.GetName(VM.SchoolTown);
                VillageName = SystemAreaCode.GetName(VM.SchoolVillage);

                var codes = SystemCode.dict["CodeM_SchoolCode_EnableState"];
                ddlEnState.Attributes.Add("required", "required");
                var listItem = new ListItem("請選擇", "");
                ddlEnState.Items.Add(listItem);
                foreach (var item in codes)
                {
                    ddlEnState.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                }

                try
                {
                    ddlEnState.SelectedValue = VM.EnableState.ToString();
                }
                catch
                { }


            CountyJson = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
            TownJson = JsonConvert.SerializeObject(SystemAreaCode.GetTownList(VM.SchoolCounty));
            VillageJson = JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(VM.SchoolTown));

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsValid == false)
            return;

        int State = 0;
        int.TryParse(ddlEnState.SelectedValue, out State);

        string Address = PureString(tbAddress.Text);
        string Tel = PureString(tbTel.Text);

        int Address1 = GetNumber<int>("SelectCounty");
        int Address2 = GetNumber<int>("SelectTown");
        int Address3 = GetNumber<int>("SelectVillage");

        string Name = PureString(tbName.Text);
        string Code = PureString(tbCode.Text);

        int Chk = 0;

        var user = AuthServer.GetLoginUser();
        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_CodeM_xUpdateElementarySchool"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ElementarySchoolID", ElementarySchoolID },
                                                    { "@SchoolName", Name },
                                                    { "@EnableState", State },
                                                    { "@SchoolCounty", Address1 },
                                                    { "@SchoolTown", Address2 },
                                                    { "@SchoolVillage", Address3 },
                                                    { "@SchoolAddress", Address },
                                                    { "@SchoolPhoneNumber", Tel },
                                                    { "@SchoolCode", Code },
                                                    { "@OrgID", user.OrgID },
                                        });

        Chk = (int)OutDict["@Chk"];

        string script = "";

        if (Chk > 0)
        {

            script = string.Format("{0}<script>alert('儲存成功');location.href='{1}#{2}';</script>",
                        "<style>body{display:none;}</style>"
                        , "/System/CodeM/SchoolCode.aspx"
                        , HttpUtility.HtmlDecode(GetString("hash") ?? "")
                      );

            SystemElementarySchool.Update();
        }
        else
        {
            switch (Chk)
            {
                case -1:
                    script = " <script>alert('代碼重複');</script>";
                    break;
                case -2:
                    script = "<script>alert('同縣市學校名稱重複');</script>";
                    break;
                default:
                    script = "<script>alert('儲存失敗');</script>";
                    break;
            }
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }

}