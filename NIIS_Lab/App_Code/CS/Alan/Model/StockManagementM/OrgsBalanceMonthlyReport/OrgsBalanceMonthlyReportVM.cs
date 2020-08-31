using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OrgsBalanceMonthlyReportVM 的摘要描述
/// </summary>
public class OrgsBalanceMonthlyReportVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string FormDrug { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string AvaDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int LastMonthNum { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int OutNum { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int InNum { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int UseNum { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public int DamNum { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int ReturnNum { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public int ThisMonthNum { get; set; }
}