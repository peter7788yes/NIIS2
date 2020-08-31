using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UrgeSettingCaseListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }


    [JsonProperty(PropertyName = "CaseIdNo")]
    public string CaseIdNo { get; set; }

    [JsonProperty(PropertyName = "CaseName")]
    public string CaseName { get; set; }

    [JsonProperty(PropertyName = "ContactName")]
    public string ContactName { get; set; }


    [JsonProperty(PropertyName = "SendContent")]
    public string SendContent { get; set; }



    [JsonProperty(PropertyName = "RS")]
    public string RS { get; set; }

     
  
}