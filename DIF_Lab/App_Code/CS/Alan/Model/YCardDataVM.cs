using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// YCardDataVM 的摘要描述
/// </summary>
public class YCardDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DoseID { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string AgeChinese { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string AgeEngilsh { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int Period { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string YCardDataType { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int CreatAccount { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int ModifyAccount { get; set; }
}