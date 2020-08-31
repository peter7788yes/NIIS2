using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserModifyLogVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "CI")]
    public int CaseID { get; set; }

     [JsonProperty(PropertyName = "MI")]
    public int ModifyID { get; set; }
     

    [JsonProperty(PropertyName = "D")]
     public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "O")]
    public string ModifyOrg { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string ModifyUserID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string ModifyColumn { get; set; }
 

    [JsonProperty(PropertyName = "B")]
    public string BeforeModify { get; set; }

    [JsonProperty(PropertyName = "A")]
    public string AfterModify { get; set; }
     

}