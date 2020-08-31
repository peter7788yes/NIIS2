using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CaseUrgeSettingListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int UrgeID { get; set; }
     

    [JsonProperty(PropertyName = "US")]
     public string UrgeStatus { get; set; }

    [JsonProperty(PropertyName = "USN")]
    public string UrgeStatusName { get { return (SystemCode.GetName("CaseUrge_UrgeStatus", Convert.ToInt32(UrgeStatus)) ?? ""); } }

    [JsonProperty(PropertyName = "CD")]
    public string CreateDate { get; set; }


    [JsonProperty(PropertyName = "UT")]
    public string UrgeType { get; set; }

    [JsonProperty(PropertyName = "UTN")]
    public string UrgeTypeName { get { return (SystemCode.GetName("CaseUrge_UrgeType", Convert.ToInt32(UrgeType)) ?? ""); } }

     
    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }


    [JsonProperty(PropertyName = "UC")]
    public string UrgeCount { get; set; }
  
}