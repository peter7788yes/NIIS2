using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WarmSetVM 的摘要描述
/// </summary>
public class WarmSetVM
{
    [JsonProperty(PropertyName = "c1")]
    public string TempHigh { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string TempLow { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string WarningTypes { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int OrgID { get; set; }

}