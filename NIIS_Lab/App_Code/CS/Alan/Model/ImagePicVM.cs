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
    public string PicPath { get; set; }
}