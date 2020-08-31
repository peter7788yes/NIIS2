using Newtonsoft.Json;

public class AgencyInfoVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "AN")]
    public string AgencyName { get; set; }

    [JsonIgnore]
    public int AgencyState { get; set; }

    [JsonProperty(PropertyName = "AS")]
    public string AgencyStateString
    {
        get
        {
            return SystemCode.GetName("ParameterM_LocationSetting_AgencyState", AgencyState);
        }
    }

 
    [JsonProperty(PropertyName = "C")]
    public string AgencyCode { get; set; }

    [JsonProperty(PropertyName = "AC", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int AgencyCounty { get; set; }

    [JsonProperty(PropertyName = "AT", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int AgencyTown { get; set; }

    [JsonProperty(PropertyName = "AV", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int AgencyVillage { get; set; }

    [JsonProperty(PropertyName = "AA", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string AgencyAddress { get; set; }

    [JsonProperty(PropertyName = "PA", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string PhoneAreaCode { get; set; }

    [JsonProperty(PropertyName = "AP", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string AgencyPhoneNumber { get; set; }

    [JsonProperty(PropertyName = "CA", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string AgencyCate { get; set; }

    [JsonIgnore]
    public int BusinessState { get; set; }

    [JsonProperty(PropertyName = "BS")]
    public string BusinessStateString
    {
        get
        {
            return SystemCode.GetName("ParameterM_LocationSetting_BusinessState", BusinessState);
        }
    }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "O")]
    public string OrgName {
        get {
            return SystemOrg.GetName(OrgID);
        }
    }

 


    [JsonProperty(PropertyName = "EV")]
    public int EnableVaccineID { get; set; }


    [JsonProperty(PropertyName = "D")]
    public string DepartmentIDs { get; set; }


    [JsonProperty(PropertyName = "DO")]
    public string DepartmentOther { get; set; }

    [JsonProperty(PropertyName = "RT")]
    public int ReportingType { get; set; }

    [JsonProperty(PropertyName = "SF")]
    public bool IsSimpleFlu { get; set; }

    [JsonProperty(PropertyName = "Z")]
    public string Zone
    {
        get
        {
            string rtn = "";

            var CountyList = SystemAreaCode.GetCountyList();
            var TownList = SystemAreaCode.GetTownList(AgencyCounty);

           
            SystemAreaCodeVM VM = CountyList.Find(item => item.ID == AgencyCounty);
            if (VM != null)
                rtn += VM.AreaName;

            VM = TownList.Find(item => item.ID == AgencyTown);
            if (VM != null)
                rtn += VM.AreaName;

            return rtn;
        }
    }

    [JsonProperty(PropertyName = "IS")]
    public string InoculationSchedule { get; set; }
}