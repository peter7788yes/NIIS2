using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ReportProgressVM 的摘要描述
/// </summary>
public class ReportProgressVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string ReportStatus { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string ReportDate { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public int Status { get; set; }

}