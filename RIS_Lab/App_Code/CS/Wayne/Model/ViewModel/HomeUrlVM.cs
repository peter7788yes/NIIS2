using Newtonsoft.Json;
using System;

public class HomeUrlVM
{
    [JsonProperty(PropertyName = "P")]
	public string PageUrl { get; set; }

    [JsonProperty(PropertyName = "d")]
    public DateTime date { get; set; }
}