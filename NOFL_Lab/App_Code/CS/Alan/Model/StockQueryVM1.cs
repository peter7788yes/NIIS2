using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// StockQueryVM1 的摘要描述
/// </summary>
public class StockQueryVM1
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public float DoseStorage { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int Remaining { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public int AvapPeriod { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int SafeNum { get; set; }
}