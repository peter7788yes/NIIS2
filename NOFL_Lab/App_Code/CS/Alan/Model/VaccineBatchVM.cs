using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineBatchVM 的摘要描述
/// </summary>
public class VaccineBatchVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string POID { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string PODate { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public int BatchType { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string BatchTypeString { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int FormDrug { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string FormDrugString { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public float DosePer { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public float Price { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public string AvaDate { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public string BatchBrand { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public string EntireBrand { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public string BatchPic { get; set; }

    [JsonProperty(PropertyName = "c17")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c18")]
    public string CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c19")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c20")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c21")]
    public string ModifyAccount { get; set; }
}