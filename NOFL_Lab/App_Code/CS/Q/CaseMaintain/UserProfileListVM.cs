using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserProfileListVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

     [JsonProperty(PropertyName = "C")]
    public int CaseID { get; set; }
     

    [JsonProperty(PropertyName = "I")]
    public string IDNO { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public string BirthDate { get; set; }

    [JsonProperty(PropertyName = "MBD")]
    public string MotherBirthDate { get; set; }

    [JsonProperty(PropertyName = "MI")]
    public string MotherID { get; set; }

    [JsonProperty(PropertyName = "MC")]
    public string MotherCaseID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "TD")]
    public string TelD { get; set; }

    [JsonProperty(PropertyName = "TN")]
    public string TelN { get; set; }
    
    [JsonProperty(PropertyName = "MM")]
    public string MotherMobile
    { get; set; }

    [JsonProperty(PropertyName = "BS")]
    public string BirthSeq { get; set; }



    [JsonProperty(PropertyName = "GID")]
    public string GroupID { get; set; }
}