using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ReportBMainVM 的摘要描述
/// </summary>
public class ReportBMainVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string ReportName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string Date { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string ReportStatus { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int CheckProgress { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int CheckDate { get; set; }

}