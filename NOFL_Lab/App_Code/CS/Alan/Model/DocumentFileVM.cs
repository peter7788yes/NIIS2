using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// QnADataFileVM 的摘要描述
/// </summary>
public class DocumentFileVM
{
    [JsonProperty(PropertyName = "c1")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int DocumentInfoID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int FileInfoID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DisplayFileName { get; set; }
}