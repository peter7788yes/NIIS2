using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ImagePicVM 的摘要描述
/// </summary>
public class ImagePicVM
{
    [JsonProperty(PropertyName = "c1")]
    public int FileInfoID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string StorageFileName { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string FileCreateDate { get; set; }

}