using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using WayneEntity;

public partial class VaccinationM_RegisterData_Detail : BasePage
{
    public string AgeString = "";
    public string  uJson = "''";

    public string tbAry = "[]";
    public string scAry = "[]";
    public string fnAry = "[]";

    public string CapacityString = "";
    public int CaseUserID = 0;
    public string OrgName = "";
    public CaseUserVM VM = new CaseUserVM();

    public VaccinationM_RegisterData_Detail()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);
        
        CaseUserID = GetNumber<int>("i");
      
        if (CaseUserID == 0)
        {
            string script = "<style>body{disply:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        ucCaseRemark1.CaseID = CaseUserID;
        var user = AuthServer.GetLoginUser();
        OrgName = user.OrgName;

        DataTable dt = new DataTable();
        int YCardMainID = 0;
        int DeltaDays = 0;
        Dictionary<string, object> OutDict = new Dictionary<string, object>()
                                                     {
                                                        { "@YCardMainID", YCardMainID },
                                                        { "@DeltaDays" , DeltaDays}
                                                     };

        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_RecordM_xGetCaseUserByIDAndGetOrAddYCardMain"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                     { "@CaseUserID", CaseUserID }
                                        });

        YCardMainID = (int)OutDict["@YCardMainID"];
        DeltaDays = (int)OutDict["@DeltaDays"];

        List<RegisterStoolCardVM> scList = new List<RegisterStoolCardVM>();
        List<RegisterFluNotesVM> fnList = new List<RegisterFluNotesVM>();
        string CapacityIDs = "";
        EntityS.FillModel(VM, ds.Tables[0]);
        if (ds.Tables[2].Rows.Count > 0)
        {
            EntityS.FillModel(VM, ds.Tables[2]);
            VM.HasYellowCardMessage = true;
        }
        EntityS.FillModel(scList, ds.Tables[3]);
        EntityS.FillModel(fnList, ds.Tables[4]);

        if (ds.Tables[1].Rows.Count > 0)
            CapacityIDs = ds.Tables[1].Rows[0][0].ToString();

        if (scList.Count > 0)
            scAry = JsonConvert.SerializeObject(scList);

        if (fnList.Count > 0)
            fnAry = JsonConvert.SerializeObject(fnList);

        List<string> CapacityList = new List<string>();

        foreach(var item in CapacityIDs.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
        {
            CapacityList.Add(SystemCode.GetName("CaseUser_Capacity", int.Parse(item)));
        }

        CapacityString = string.Join(",", CapacityList.ToArray());

        //if(ds.Tables[1].Rows.Count>0)
        //    YCardMainID = (int)ds.Tables[1].Rows[0][0];




        uJson = JsonConvert.SerializeObject(VM);

        AgeCalculatorT AT = new AgeCalculatorT();

        AgeString = AT.GetYearMonthAge(VM.BirthDate);


        DateTime birthDate = VM.BirthDate;

        dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetRecordDataByCaseUserID"
                                         , new Dictionary<string, object>()
                                         {
                                             { "@CaseUserID", CaseUserID }
                                         });

        List<RecordDataVM> list = new List<RecordDataVM>();

        //List<RecordYellowDataVM> yellowList = new List<RecordYellowDataVM>();
        var yellowList = SystemYCard.GetDict(YCardMainID).Where(item => item.YCardDataType == 1);
        //var x = SystemYCard.dict;
        List<RecordUserDataVM> userList = new List<RecordUserDataVM>();

        //List<RecordYellowDataVM> yellowRemoveList = new List<RecordYellowDataVM>();
        List<RecordUserDataVM> userRemoveList = new List<RecordUserDataVM>();

        EntityS.FillModel(userList, dt);

        //EntityS.FillModel(yellowList, ds.Tables[0]);
        //EntityS.FillModel(userList, ds.Tables[1]);

        //var userVaccineCodes =userList.Select(item => item.SystemRecordVaccineCode).ToList();
        //var DoseIDs = yellowList.Select(item => item.DoseID).ToList();



        foreach (var yellow in yellowList)
        {
            var queryList = userList.FindAll(item=>item.SystemRecordVaccineCode.Equals(yellow.DoseID));

            if (queryList.Count > 0)
            {
                foreach (var q in queryList)
                {
                    //yellowRemoveList.Add(yellow);
                    userRemoveList.Add(q);
                    RecordDataVM rVM = new RecordDataVM(birthDate);
                    rVM.InoculationDate = q.InoculationDate;
                    rVM.IsRule = true;
                    rVM.OrgID = q.OrgID;
                    rVM.VaccineBatchID = q.VaccineBatchID;
                    rVM.SystemRecordVaccineID = q.SystemRecordVaccineID;
                    rVM.AgeEngilsh=yellow.AgeEngilsh;
                    rVM.DoseID=yellow.DoseID;
                    rVM.Period=yellow.Period;
                    rVM.CreatedDate = q.CreatedDate;
                    rVM.CreateType = q.CreateType;
                    //rVM.ColorType = 1;
                    rVM.RecordDataID = q.RecordDataID;
      

                    //if (rVM.InoculationDate == null || DateTime.Equals(rVM.InoculationDate, new DateTime(2099, 1, 1, 1, 1, 1, 0)))
                    //{
                    //    rVM.OrgID = 0;
                    //    rVM.ColorType = 0;
                    //    rVM.CreatedDate = null;
                    //}
                    //else if(rVM.AppointmentDate!=null && DateTime.Equals(rVM.InoculationDate, new DateTime(2099, 1, 1, 1, 1, 1, 0))==false)
                    //{
                    //   if(DateTime.Compare(rVM.InoculationDate.Value, rVM.AppointmentDate.Value.AddDays(90))>0)
                    //    {
                    //        rVM.ColorType = 1;
                    //    }
                    //}

                    list.Add(rVM);
          
                }
            }
            else
            {
                RecordDataVM rVM = new RecordDataVM(birthDate);
                rVM.IsRule = true;
                rVM.AgeEngilsh = yellow.AgeEngilsh;
                rVM.DoseID = yellow.DoseID;
                //rVM.SystemRecordVaccineID = SystemRecordVaccine.GetID(yellow.DoseID);
                rVM.Period = yellow.Period;
                //rVM.ColorType = 0;
                list.Add(rVM);

            }
        }

        //yellowList.RemoveAll(item => yellowRemoveList.Contains(item));

        userList.RemoveAll(item => userRemoveList.Contains(item));

        list.OrderBy(item => item.AppointmentDate).ThenBy(item => item.Period).ThenBy(item=>item.DoseID);

        foreach (var u in userList)
        {
            RecordDataVM rVM = new RecordDataVM(birthDate);
            rVM.InoculationDate = u.InoculationDate;
            rVM.IsRule = false;
            rVM.OrgID = u.OrgID;
            rVM.VaccineBatchID = u.VaccineBatchID;
            rVM.SystemRecordVaccineID = u.SystemRecordVaccineID;
            rVM.RecordDataID = u.RecordDataID;

            if (rVM.InoculationDate == null || DateTime.Equals(rVM.InoculationDate, new DateTime(2099, 1, 1, 1, 1, 1, 0)))
            {
                rVM.OrgID = 0;
                rVM.CreatedDate = null;
            }

            //list.Add(VM);
            //find last InoculationDate
            var index =list.FindLastIndex(item => item.InoculationDateOut != null );
            if (index >= 0)
            {
                if (index + 1 <= list.Count)
                {
                    list.Insert(index+1, rVM);
                }
                else
                {
                    list.Add(rVM);
                }
            }
            else
            {
                list.Insert(0, rVM);
            }
        }

        var LastInoculationDateIndex = list.FindLastIndex(item => item.InoculationDateOut != null);

        for (int i = LastInoculationDateIndex + 1; i <= list.Count - 1; i++)
        {
            list[i].DeltaDays = DeltaDays;
        }
        

        if (list.Count>0)
            tbAry = JsonConvert.SerializeObject(list);

    }




}