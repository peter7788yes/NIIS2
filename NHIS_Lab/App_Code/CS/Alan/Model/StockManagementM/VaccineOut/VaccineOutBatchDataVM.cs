using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineOutBatchDataVM 的摘要描述
/// </summary>
public class VaccineOutBatchDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineIDName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string BatchIDName { get; set; }

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
    public string FroIdxName { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string OriFroIdxName { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string MonIdxName { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public int VaccInID { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public int VaccBatchID { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public int FroIdx { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public int MonIdx { get; set; }

    [JsonProperty(PropertyName = "c17")]
    public float TempHigh { get; set; }

    [JsonProperty(PropertyName = "c18")]
    public float TempLow { get; set; }

    [JsonProperty(PropertyName = "c19")]
    public int OriFroIdx { get; set; }

    [JsonProperty(PropertyName = "c20")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c21")]
    public int CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c22")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c23")]
    public int ModifyAccount { get; set; }
}