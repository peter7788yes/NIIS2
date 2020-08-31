using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// QnADataVM 的摘要描述
/// </summary>
public class QnADataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int QaType { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Question { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string Answer { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int QaStatus { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int ViewNum { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public int ModifyAccount { get; set; }
}