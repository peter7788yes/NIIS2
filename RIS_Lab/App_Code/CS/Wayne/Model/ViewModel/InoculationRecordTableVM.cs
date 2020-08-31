using Newtonsoft.Json;
using System;

public class InoculationRecordTableVM
{
    [JsonProperty(PropertyName = "CN")]
    public string CaseUserName { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public DateTime BirthDate { get; set; }

    [JsonProperty(PropertyName = "RI")]
    public string RocID { get; set; }

    [JsonProperty(PropertyName = "LV")]
    public string LiveVillage { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "MR")]
    public string MotherRocID { get; set; }

}