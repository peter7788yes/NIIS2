using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// NewsPublishedVM 的摘要描述
/// </summary>
public class NewsPublishedVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string RoleName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Subject { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string Contents { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string PublishedStarDate { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string PublishedEndDate { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string EmailOrg { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public string ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public int FileNum { get; set; }
}