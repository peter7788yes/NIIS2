using Newtonsoft.Json;
using System;

public class SystemRoleVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "R")]
    public string RoleName { get; set; }

    [JsonProperty(PropertyName = "IU")]
    public bool IsUserRole { get; set; }

    [JsonIgnore]
    public int RoleCateID { get; set; }

    [JsonProperty(PropertyName = "RL")]
    public int RoleLevel { get; set; }
}