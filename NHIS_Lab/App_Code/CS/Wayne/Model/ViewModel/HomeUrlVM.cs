using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// HomeUrlVM 的摘要描述
/// </summary>
public class HomeUrlVM
{
    [JsonProperty(PropertyName = "P")]
	public string PageUrl { get; set; }

    [JsonProperty(PropertyName = "d")]
    public DateTime date { get; set; }
}