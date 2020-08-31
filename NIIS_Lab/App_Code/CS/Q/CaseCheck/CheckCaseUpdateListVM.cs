using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CheckCaseUpdateListVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }



 [JsonProperty(PropertyName = "ID")]
    public string ID { get; set; }

    [JsonProperty(PropertyName = "CaseIdNo")]
    public string CaseIdNo { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "RoleName")]
    public string RoleName { get; set; }

    [JsonProperty(PropertyName = "ModifyDate")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "CaseName")]
    public string CaseName { get; set; }

    [JsonProperty(PropertyName = "UpdateFields")]
    public string UpdateFields { get; set; }

    [JsonProperty(PropertyName = "IsFinish")]
    public int IsFinish { get; set; }


}