using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SystemParametersVM 的摘要描述
/// </summary>
public class SystemParametersVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID  { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string ParaId { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string ParaDesc { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int ParaValue { get; set; }
}