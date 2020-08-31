using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SwitchAccountFileVM 的摘要描述
/// </summary>
public class SwitchAccountFileVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int AccountFile { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DisplayFileName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string AccountFileDesc { get; set; }

}