using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OrgVaccSet 的摘要描述
/// </summary>
public class OrgVaccSet
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int VaccineStatus { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public int SafeNum { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int AvaPeriod { get; set; }

}