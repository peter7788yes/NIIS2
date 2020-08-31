using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using WayneEntity;

public partial class Vaccination_RecordM_ApplyEffect : BasePage
{
    //public string ApplyEffectJson = "";
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public int UpdateUID = 0;
    public string UpdateUserData = "{}";
    public string UpdateUserDataList = "[]";
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string SystemRecordVaccineCode = "";
    public int SystemRecordVaccineID = 0;
    public string AppointmentDate = "";
    new bool IsValid = true;
    List<int> et = new List<int>();
    List<int> ev = new List<int>();
    List<int> esv = new List<int>();
    List<int> es = new List<int>();
    List<DateTime> es_datetime = new List<DateTime>();
    List<int> ed = new List<int>();
    public string MyPartialData = "[]";
    public string MyBodyData = "[]";
    public string Agency = "";
    public int AgencyID = 0;

    public Vaccination_RecordM_ApplyEffect()
    {
        AddPower = base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            CaseUserID = GetNumber<int>("c");
            RecordDataID = GetNumber<int>("i");
            SystemRecordVaccineCode = GetString("r");
            SystemRecordVaccineID = GetNumber<int>("ri");
            AppointmentDate = GetString("a");

            UpdateUID = GetNumber<int>("uu");

            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            //AppointmentDate = date.ToShortTaiwanDate();

            //if (success==false || CaseUserID == 0 || RecordDataID == 0 )
            if (UpdateUID == 0)
            {
                if (CaseUserID == 0 || RecordDataID == 0)
                {
                    IsValid = false;
                    string script = "<script>alert('資料取得失敗');window.close();</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                    return;
                }
            }
            lblVC.Text = SystemRecordVaccineCode;
            lblAD.Text = success ? AppointmentDate : "";
            var user = AuthServer.GetLoginUser();

            Agency = user.OrgName;
            AgencyID = user.OrgID;

            List<MyPartialDataVM> partialLst = new List<MyPartialDataVM>();
            if (SystemCode.dict.ContainsKey("RecordM_ApplyEffect_Partial"))
            {
                foreach(var item in SystemCode.dict["RecordM_ApplyEffect_Partial"])
                {
                    MyPartialDataVM VM = new MyPartialDataVM();
                    VM.EnumName = item.EnumName;
                    VM.EnumValue = item.EnumValue;
                    string key = "RecordM_ApplyEffect_Partial" + "_Value" + item.EnumValue.ToString();
                    if (SystemCode.dict.ContainsKey(key))
                    {
                        VM.DataAry = SystemCode.dict[key];
                    }
                    partialLst.Add(VM);
                }

                MyPartialData = JsonConvert.SerializeObject(partialLst);
            }


            partialLst.Clear();
            if (SystemCode.dict.ContainsKey("RecordM_ApplyEffect_Body"))
            {
                foreach (var item in SystemCode.dict["RecordM_ApplyEffect_Body"])
                {
                    MyPartialDataVM VM = new MyPartialDataVM();
                    VM.EnumName = item.EnumName;
                    VM.EnumValue = item.EnumValue;
                    string key = "RecordM_ApplyEffect_Body" + "_Value" + item.EnumValue.ToString();
                    if (SystemCode.dict.ContainsKey(key))
                    {
                        VM.DataAry = SystemCode.dict[key];
                    }
                    partialLst.Add(VM);
                }

                MyBodyData = JsonConvert.SerializeObject(partialLst);
            }


            ///Get ApplyEffectList 
            //DataTable dt = new DataTable();

            //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetApplyEffectByRecordDataID", sc))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            sc.Open();
            //            da.Fill(dt);
            //        }
            //    }
            //}


            //List<ApplyEffectVM> list = new List<ApplyEffectVM>();
            //EntityS.FillModel(list, dt);

            //ApplyEffectJson = JsonConvert.SerializeObject(list);


            
                if (UpdateUID > 0)
                {
                    var ds = MSDB.GetDataSet("ConnDB", "dbo.usp_RecordM_xGetApplyEffectID"
                                            , new Dictionary<string, object>()
                                            {
                                              { "@ID", UpdateUID }

                                           });

                    ApplyEffectVM VM = new ApplyEffectVM();
                    List<ApplyEffectDetailVM> list = new List<ApplyEffectDetailVM>();
                    EntityS.FillModel(VM, ds.Tables[0]);
                    EntityS.FillModel(list, ds.Tables[1]);

                    UpdateUserData = JsonConvert.SerializeObject(VM);
                    UpdateUserDataList = JsonConvert.SerializeObject(list);
                }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;
        string ddd = GetString("et");
        et = GetList<int>("et");
        ev = GetList<int>("ev");
        esv = GetList<int>("esv");
        es = GetList<int>("es");
        ed = GetList<int>("ed");

        if (
            et.Count != ev.Count ||
            ev.Count != esv.Count ||
            esv.Count != es.Count ||
            es.Count != ed.Count )
        {
            string msg = "<script>alert('儲存失敗');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", msg, false);
            return;
        }

        DateTime EffectDate = DateTime.Now;

        //DateTime.TryParse(Request.Form["AD"], out AssessmentDate);
        DateTime.TryParseExact((PureString(tbDate.Text) ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                               "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out EffectDate);

        foreach(var item in es)
        {
            DateTime date = DateTime.Now;
            bool success = DateTime.TryParseExact((item.ToString() ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                                                   "yyyyMMdd",
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None,
                                                   out date);
            if(success)
            {
                es_datetime.Add(date);
            }
        }

        if (DateTime.Equals(EffectDate, DateTime.MinValue) == true)
            EffectDate = DateTime.Now;

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddOrUpdateApplyEffect"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                              { "@ApplyEffectID", UpdateUID },
                                              { "@EffectDate", EffectDate },
                                              { "@EffectDays", 0 },
                                              { "@SignUserID", user.ID },
                                              { "@CreatedUserID", user.ID },
                                              { "@HasSymptom", true },
                                              { "@RecordDataID", RecordDataID },
                                              { "@OrgID", user.OrgID },
                                              { "@Day3", rb3b.Checked },
                                              { "@Day7", rb7b.Checked },
                                              { "@CaseUserID", CaseUserID},
                                              { "@EffectTypeList" , string.Join(",",et.ConvertAll<string>(item=>item.ToString()))},
                                              { "@EffectValueList" , string.Join(",",ev.ConvertAll<string>(item=>item.ToString()))},
                                              { "@EffectScopeValueList" ,string.Join(",",esv.ConvertAll<string>(item=>item.ToString()))},
                                              { "@EffectStartDateList" , string.Join(",",es_datetime.ConvertAll<string>(item=>item.ToString("yyyy-MM-dd HH:mm:ss")))},
                                              { "@EffectDaysList" , string.Join(",",ed.ConvertAll<string>(item=>item.ToString()))},
                                              { "@SystemRecordVaccineID",SystemRecordVaccineID}

                                        });

        Chk = (int)OutDict["@Chk"];

        string script = "";

        
        if (Chk > 0)
        {
            script = "<style>body{display:none;}</style><script>alert('儲存成功');";
            if(UpdateUID>0)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["I"] = UpdateUID;
                dict["SD"] = DateTime.Now;
                dict["UN"] = user.UserName;
                dict["ON"] = user.OrgName;
                dict["ED"] = EffectDate;
                dict["D3"] = rb3b.Checked;
                dict["D7"] = rb7b.Checked;
                script += "window.opener.refreshEffect("+JsonConvert.SerializeObject(dict)+");";
            }
            script+="window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}