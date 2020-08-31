using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// VaccineVM 的摘要描述
/// </summary>
public class VaccineVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public int VaccineType { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string VaccineEName { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string DiseaseID { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string DiseaseCName { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int VaccineUsage { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string VaccineCent1 { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string VaccineCent2 { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string VaccineCent3 { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public string VaccineCent4 { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public int VaccineNum { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public string IntervalVal { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public int SafeNum { get; set; }

    [JsonProperty(PropertyName = "c17")]
    public int AvaPeriod { get; set; }

    [JsonProperty(PropertyName = "c18")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c19")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c20")]
    public string RemarkFile { get; set; }

    [JsonProperty(PropertyName = "c21")]
    public int VaccineSequence { get; set; }

    [JsonProperty(PropertyName = "c22")]
    public int VaccineStatus { get; set; }

    [JsonProperty(PropertyName = "c23")]
    public string CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c24")]
    DateTime CreateDate { get; set; }

    [JsonProperty(PropertyName = "c25")]
    public string ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c26")]
    DateTime ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c27")]
    public int BatchNum { get; set; }
}