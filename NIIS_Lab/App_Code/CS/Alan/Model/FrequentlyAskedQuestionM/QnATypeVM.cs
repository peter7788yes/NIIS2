using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// QnATypeVM 的摘要描述
/// </summary>
public class QnATypeVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string TypeStatus { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int CreatAccount { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public int ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int QaNum { get; set; }
}