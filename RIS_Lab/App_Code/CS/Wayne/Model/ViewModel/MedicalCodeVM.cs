using Newtonsoft.Json;
using System;

public class MedicalCodeVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "AN")]
    public string OrgAgencyName { get; set; }

    [JsonProperty(PropertyName = "AC")]
    public string AgencyCode { get; set; }

    [JsonProperty(PropertyName = "AS")]
    public string OrgAgencyShortName { get; set; }

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


    [JsonProperty(PropertyName = "CD")]
    public DateTime ChangeDate { get; set; }
}