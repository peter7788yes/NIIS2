using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserProfileContactListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int CaseID { get; set; }

    [JsonProperty(PropertyName = "CC")]
    public int ContactCaseID { get; set; } 

    [JsonProperty(PropertyName = "I")]
    public string IdNo { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public string BirthDate { get; set; }
      
    [JsonProperty(PropertyName = "N")]
    public string ChName { get; set; }

     [JsonProperty(PropertyName = "M")] 
    public int IsMain { get; set; }

     [JsonProperty(PropertyName = "RS")]
     public string RS { get; set; } 
    


}