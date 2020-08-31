using Newtonsoft.Json;
using System;

public class BCGRecordVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int BCGRecordID { get; set; }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    [JsonProperty(PropertyName = "SY")]
    public int StatisticalYear { get; set; }

    [JsonProperty(PropertyName = "YS")]
    public int YearSeason { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "SD")]
    public DateTime SignDate { get; set; }

    [JsonProperty(PropertyName = "BN")]
    public int BirthNumber { get; set; }


    [JsonProperty(PropertyName = "IBN")]
    public int InoculationBabyNumber { get; set; }


    [JsonProperty(PropertyName = "IKN")]
    public int InoculationKidNumber { get; set; }


}