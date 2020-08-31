using Newtonsoft.Json;
using System;

public class SystemRoleVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "R")]
	public string RoleName { get; set; }
}