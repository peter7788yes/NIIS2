using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CreateInfoVM 的摘要描述
/// </summary>
public class CreateInfoVM
{
    [JsonProperty(PropertyName = "c1")]
    public string CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string CreateRole { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string CreateDate { get; set; }

}