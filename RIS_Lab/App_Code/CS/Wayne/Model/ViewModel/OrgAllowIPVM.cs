using Newtonsoft.Json;
using System;

public class OrgAllowIPVM
{
    [JsonProperty(PropertyName = "IS")]
    public string IpStart { get; set; }

    [JsonProperty(PropertyName = "IE")]
    public string IpEnd { get; set; }
}
