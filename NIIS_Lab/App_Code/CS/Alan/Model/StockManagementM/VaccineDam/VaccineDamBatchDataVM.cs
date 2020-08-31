using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineDamBatchDataVM 的摘要描述
/// </summary>
public class VaccineDamBatchDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string Staff { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string VaccineIDName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string BatchIDName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string AvaDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string FormDrugName { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public float DosePer { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public float Price { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int Num { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public float DoseNum { get; set; }

}