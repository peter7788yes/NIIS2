using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineInPendDataVM 的摘要描述
/// </summary>
public class VaccineInPendDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string FormDrugName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public float DosePer { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public float Price { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int Num { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public float DoseNum { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int DealStatus { get; set; }
}