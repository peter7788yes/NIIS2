using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineOutDataVM 的摘要描述
/// </summary>
public class VaccineOutDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string OutOrgName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string InOrgName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string DealType { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string DealHospital { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int ModifyAccount { get; set; }
}