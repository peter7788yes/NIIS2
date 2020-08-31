using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// YCardMainVM 的摘要描述
/// </summary>
public class YCardMainVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string StartDate { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string EndDate { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int YCardDataNum { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int CreatAccount { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int EndCheck { get; set; }
}