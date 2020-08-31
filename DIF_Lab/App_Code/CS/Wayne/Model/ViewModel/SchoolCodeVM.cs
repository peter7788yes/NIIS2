using Newtonsoft.Json;
using System;

public class SchoolCodeVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "SN")]
    public string SchoolName { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string SchoolCode { get; set; }

    [JsonIgnore]
    public int EnableState { get; set; }

    [JsonProperty(PropertyName = "ES")]
    public string EnableStateString
    {
        get
        {
            return SystemCode.GetName("CodeM_SchoolCode_EnableState", EnableState);
        }
    }

    [JsonIgnore]
    public int SchoolCounty { get; set; }

    [JsonIgnore]
    public int SchoolTown { get; set; }

    [JsonIgnore]
    public int SchoolVillage { get; set; }

    [JsonProperty(PropertyName = "SC")]
    public string SchoolCountyString
    {
        get
        {
            return SystemAreaCode.GetName(SchoolCounty);
        }
    }

    [JsonProperty(PropertyName = "SA")]
    public string SchoolAddress { get; set; }


    [JsonProperty(PropertyName = "SP")]
    public string SchoolPhoneNumber { get; set; }


}