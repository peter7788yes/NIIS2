using Newtonsoft.Json;
using System;

public class UserNameIDVM
{
    [JsonProperty(PropertyName = "U")]
    public int UserID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string UserName { get; set; }
}