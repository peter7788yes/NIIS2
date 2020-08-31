using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ColdChainVM 的摘要描述
/// </summary>
public class ColdChainVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string WatchDate { get; set; }

    [JsonIgnore]
    public int WatchOrg { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string WatchOrgName 
    {
        get
        {
            return SystemOrg.GetName(WatchOrg);
        }
    }

    [JsonProperty(PropertyName = "c5")]
    public string WatchPlace { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string Temp { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string CreateTypeName { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string Staff { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CreateDate { get; set; }

}