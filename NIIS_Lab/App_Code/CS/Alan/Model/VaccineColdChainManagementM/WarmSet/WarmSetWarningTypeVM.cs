using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WarmSetWarningTypeVM 的摘要描述
/// </summary>
public class WarmSetWarningTypeVM
{
    [JsonProperty(PropertyName = "c1")]
    public int WarningType { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string WarningTypeName { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string Staffs { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Content { get; set; }
}