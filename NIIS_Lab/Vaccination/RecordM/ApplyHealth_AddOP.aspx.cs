using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_ApplyHealth_AddOP : BasePage
{

    public Vaccination_RecordM_ApplyHealth_AddOP()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        DateTime AssessmentDate = DateTime.Now;

        int SelfOrFamily = GetNumber<int>("sf");
        int CaseUserID = GetNumber<int>("cc");
        int AssessmentUserID = GetNumber<int>("au");
        int SystemRecordVaccineID = GetNumber<int>("ri");
        string AssessmentUserName = GetString("aun");
        int UpdateUID = GetNumber<int>("uu");
        //DateTime.TryParse(Request.Form["AD"], out AssessmentDate);
        //.TryParseExact((GetString("ad")  ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
        DateTime.TryParseExact((GetString("ad") ?? new DateTime(2099,1,1,1,1,1,0).ToShortTaiwanDate()).RepublicToAD(),
                               "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out AssessmentDate);

        bool AllowWork = GetNumber<int>("aw") == 1 ? true :false;
        string ApplyHealthCateIDs = GetString("ah");
        bool IsValid = false;

        try
        {
            List<int> list = ApplyHealthCateIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }


        string OtherState = GetString("os");
        int RecordDataID = GetNumber<int>("rd");

        if (UpdateUID == 0)
        {
            if (IsValid == false || AssessmentUserID == 0 || AssessmentDate == default(DateTime) || RecordDataID == 0)
            {
                OPVM VMerr = new OPVM();
                VMerr.chk = 0;
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(JsonConvert.SerializeObject(VMerr));
                Response.End();
            }
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddOrUpdateApplyHealth"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ApplyHealthID", UpdateUID },
                                                    { "@AssessmentUserID", AssessmentUserID },
                                                    { "@AssessmentDate", AssessmentDate },
                                                    { "@CreatedUserID", user.ID },
                                                    { "@AllowWork", AllowWork},
                                                    { "@ApplyHealthCateIDs", ApplyHealthCateIDs },
                                                    { "@OtherState", OtherState },
                                                    { "@RecordDataID", RecordDataID },
                                                    { "@OrgID", user.OrgID },
                                                    { "@CaseUserID", CaseUserID },
                                                    { "@SelfOrFamily", SelfOrFamily},
                                                    { "@SystemRecordVaccineID",SystemRecordVaccineID}
                                        });

        Chk = (int)OutDict["@Chk"];

        OPVM VM = new OPVM();
        VM.chk = Chk;
        if (UpdateUID > 0)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["I"] = UpdateUID;
            dict["AD"] = AssessmentDate;
            dict["N"] = AssessmentUserName;
            dict["ON"] = user.OrgName;
            dict["AS"] = "," + ApplyHealthCateIDs.Trim(',') + ",";
            dict["OS"] = OtherState;
            dict["AW"] = AllowWork;
            VM.obj = dict;
        }
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }

}