using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ReportBDataVM 的摘要描述
/// </summary>
public class ReportBDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public int Abnormal { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string ReportStaff { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string ReportDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int VaccStatus { get; set; }
}