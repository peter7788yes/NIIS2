using Newtonsoft.Json;
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

public partial class VaccinationM_RegisterData_Detail : BasePage
{
    public string AgeString = "";
    public string  uJson = "''";

    public string tbAry = "[]";

    public int CaseUserID = 0;
    public string OrgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["I"], out CaseUserID);

      
        if (CaseUserID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        OrgName = user.OrgName;

        DataTable dt = new DataTable();
        int YCardMainID = 0;
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetCaseUserByIDAndGetOrAddYCardMain", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@YCardMainID", YCardMainID);
                sp.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                    YCardMainID = (int)sp.Value;
                }
            }
        }


        CaseUserVM VM = new CaseUserVM();
        EntityS.FillModel(VM, dt);

        //if(ds.Tables[1].Rows.Count>0)
        //    YCardMainID = (int)ds.Tables[1].Rows[0][0];

        uJson = JsonConvert.SerializeObject(VM);

        AgeCalculatorT AT = new AgeCalculatorT();

        AgeString = AT.GetAge(VM.BirthDate);


        DateTime birthDate = VM.BirthDate;

        dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetRecordDataByCaseUserID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }

            }
        }

        List<RecordDataVM> list = new List<RecordDataVM>();

        //List<RecordYellowDataVM> yellowList = new List<RecordYellowDataVM>();
        List<SystemYCardVM> yellowList = SystemYCard.GetDict(YCardMainID);
        var x = SystemYCard.dict;
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
                    rVM.ColorType = 1;
                    rVM.RecordDataID = q.RecordDataID;
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
                rVM.ColorType = 0;
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


        if(list.Count>0)
            tbAry = JsonConvert.SerializeObject(list);

    }




}