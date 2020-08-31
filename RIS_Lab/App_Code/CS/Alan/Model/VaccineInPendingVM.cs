using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineInPendingVM 的摘要描述
/// </summary>
public class VaccineInPendingVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string RoleName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public float TotalPrice { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int TotalNum { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string DealType { get; set; }
}