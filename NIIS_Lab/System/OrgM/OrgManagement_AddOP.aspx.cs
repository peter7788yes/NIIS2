using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Configuration;

public partial class System_OrgM_OrgManagement_AddOP : BasePage
{
    public System_OrgM_OrgManagement_AddOP()
    {
        base.AddPower("/System/OrgM/OrgManagement.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
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
        string IPs = GetString("ips");
        IPs = HttpUtility.UrlDecode(IPs);
        var list = JsonConvert.DeserializeObject<List<OrgAllowIPVM>>(IPs);
        var IpSList = list.Select(item => PureString(item.IpStart)).ToList();
        var IpEList = list.Select(item => PureString(item.IpEnd)).ToList();
        if (ID > 0)
        {
            int Chk = 0;
            int OrgID = 0;

            Dictionary<string, object> OutDict = new Dictionary<string, object>() {
                                                                                    { "@Chk", Chk },
                                                                                    { "@OrgID", OrgID }
                                                                                };

            MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_OrgM_xAddOrgDetailByID"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@PID", ID },
                                                    { "@AgencyCode", AgencyCode },
                                                    { "@OrgAgencyName", OrgAgencyName },
                                                    { "@OrgAgencyEnName", OrgAgencyEnName },
                                                    { "@OrgAgencyShortName", OrgAgencyShortName },
                                                    { "@OrgLevel", OrgLevel },
                                                    { "@AgencyState", AgencyState },
                                                    { "@OrderNumber", OrderNumber },
                                                    { "@IpStart", string.Join(",",IpSList) },
                                                    { "@IpEnd", string.Join(",",IpEList) },
                                                    { "@OrgCateID", WebConfigurationManager.AppSettings["OrgCateID"] }
                                            });

            Chk = (int)OutDict["@Chk"];
            OrgID = (int)OutDict["@OrgID"];

            OPVM VM = new OPVM();
            VM.chk = Chk;
            VM.message = OrgID.ToString();

            if (Chk>0)
            {
                SystemOrg.Update();
            }
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
            Response.End();
        }
    }
}
