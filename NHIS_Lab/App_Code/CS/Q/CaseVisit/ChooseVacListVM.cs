using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class ChooseVacListVM
{ 
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "VID")]
    public int ID { get; set; }

     [JsonProperty(PropertyName = "VC")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "VCN")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "VEN")]
    public string VaccineEName { get; set; }
      
  
}