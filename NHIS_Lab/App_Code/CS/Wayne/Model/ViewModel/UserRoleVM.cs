using Newtonsoft.Json;
using System;

public class UserRoleVM
{
    [JsonProperty(PropertyName = "U")]
    public int UserID { get; set; }

    [JsonProperty(PropertyName = "R")]
    public int RoleID { get; set; }
}