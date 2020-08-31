using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineReturnAcceptVM 的摘要描述
/// </summary>
public class VaccineReturnAcceptVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string ReturnTargName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string Staff { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string Vaccine { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public float TotalPrice { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int ReturnFile { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string DisplayFileName { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public int ReturnOrgID { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string ReturnOrgName { get; set; }
}