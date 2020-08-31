using System;
using System.Collections.Generic;

public partial class System_OrgM_OrgManagement_UpdateOP : BasePage
{

    public System_OrgM_OrgManagement_UpdateOP()
    {
        base.AddPower("/System/OrgM/OrgManagement.aspx", MyPowerEnum.瀏覽);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");



        int ID = GetNumber<int>("i");
        int OrgLevel = GetNumber<int>("ol");
        int AgencyState = GetNumber<int>("as");
        int OrderNumber = GetNumber<int>("on");


        string AgencyCode = GetString("ac");
        string OrgAgencyName = GetString("n");
        string OrgAgencyEnName = GetString("en");
        string OrgAgencyShortName = GetString("sn"); 
        string IpStart = GetString("is");  
        string IpEnd = GetString("ie");  

        if (ID > 0)
        {

            int Chk = 0;

            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_OrgM_xUpdateOrgDetailByID"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@OrgID", ID },
                                                    { "@AgencyCode", AgencyCode },
                                                    { "@OrgAgencyName", OrgAgencyName },
                                                    { "@OrgAgencyEnName", OrgAgencyEnName },
                                                    { "@OrgAgencyShortName", OrgAgencyShortName },
                                                    { "@OrgLevel", OrgLevel },
                                                    { "@AgencyState", AgencyState },
                                                    { "@OrderNumber", OrderNumber },
                                                    { "@IpStart", IpStart },
                                                    { "@IpEnd", IpEnd }
                                            });

            Chk = (int)OutDict["@Chk"];

            OPVM VM = new OPVM();
            VM.chk = Chk;

            if(Chk>0)
            {
                SystemOrg.Update();
            }
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
            Response.End();
        }
    }
}
