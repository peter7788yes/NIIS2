using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineDamDataBatchVM 的摘要描述
/// </summary>
public class VaccineDamDataBatchVM
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
    public string Staff { get; set; }

}