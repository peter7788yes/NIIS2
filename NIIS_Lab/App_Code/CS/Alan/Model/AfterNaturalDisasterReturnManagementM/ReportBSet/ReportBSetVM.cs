using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ReportBSetVM 的摘要描述
/// </summary>
public class ReportBSetVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string ReportName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Date { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string CreateName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string Progress { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int CheckProgress { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int CheckDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string StartDate { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string EndDate { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int ReportStatus { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public string OrgName { get; set; }
}