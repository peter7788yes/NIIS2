using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineInDataBatchVM 的摘要描述
/// </summary>
public class VaccineInDataBatchVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string InOrgName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string Staff { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string VaccineIDName { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string BatchIDName { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string BatchTypeName { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string FormDrugName { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public float DosePer { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public float Price { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int Num { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public float DoseNum { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public string FroIdxName { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public string OriFroIdxName { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public string MonIdxName { get; set; }

}