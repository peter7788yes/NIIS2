using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ModifyInfoVM 的摘要描述
/// </summary>
public class ModifyInfoVM
{
    [JsonProperty(PropertyName = "c1")]
    public string ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string ModifyRole { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string ModifyDate { get; set; }
}