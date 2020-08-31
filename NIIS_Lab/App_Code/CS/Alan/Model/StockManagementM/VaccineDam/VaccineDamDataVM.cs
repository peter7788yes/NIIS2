using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineDamDataVM 的摘要描述
/// </summary>
public class VaccineDamDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string LoginDate { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string DamOrgName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string OccPlace { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string DamType1 { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string DamType1Other { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string DamType2 { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string DamType2Other { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string DamType3 { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string DamType3Other { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public string DamType4 { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public string DamType4Other { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public string DamType5 { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public string DamType5Other { get; set; }

    [JsonProperty(PropertyName = "c17")]
    public int DamType { get; set; }

    [JsonProperty(PropertyName = "c18")]
    public string AppDate { get; set; }

    [JsonProperty(PropertyName = "c19")]
    public string AppNum { get; set; }

    [JsonProperty(PropertyName = "c20")]
    public int AppStatus { get; set; }

    [JsonProperty(PropertyName = "c21")]
    public int AppMulti { get; set; }

    [JsonProperty(PropertyName = "c22")]
    public string SentDate { get; set; }

    [JsonProperty(PropertyName = "c23")]
    public string SentNum { get; set; }

    [JsonProperty(PropertyName = "c24")]
    public string CDCRevDate { get; set; }

    [JsonProperty(PropertyName = "c25")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c26")]
    public int SystemCodeCateID { get; set; }
}