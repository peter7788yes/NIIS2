using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineCheckDataVM 的摘要描述
/// </summary>
public class VaccineCheckDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string OutOrgName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string InOrgName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public float TotalPrice { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int TotalNum { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CheckDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string CheckOrgName { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string CheckAccountName { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string Result { get; set; }
}