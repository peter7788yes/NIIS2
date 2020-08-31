using Newtonsoft.Json;
using System;

/// <summary>
/// UserChildListVM 的摘要描述
/// </summary>
public class UserChildListVM
{  
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

     [JsonProperty(PropertyName = "ParentID")]
    public string ParentID { get; set; }


    [JsonProperty(PropertyName = "ChildYear")]
     public string ChildYear { get; set; }

    [JsonProperty(PropertyName = "ChildCount")]
    public string ChildCount { get; set; }
     

     
}