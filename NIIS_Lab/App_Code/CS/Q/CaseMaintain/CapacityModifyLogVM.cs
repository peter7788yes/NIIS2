using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CapacityModifyLogVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }
      
    [JsonProperty(PropertyName = "D")]
     public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "O")]
    public string ModifyOrgName { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string ModifyUserName { get; set; }
       
    [JsonProperty(PropertyName = "K")]
    public string Kind { get; set; }
     

}