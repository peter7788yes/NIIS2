using Newtonsoft.Json;
using System;

public class RolePowerSettingVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "RN")]
    public string RoleName { get; set; }

    [JsonIgnore]
    public int RoleLevel { get; set; }

    [JsonProperty(PropertyName = "R")]
    public string RoleLevelString
    { 
        get
        {
            return Enum.GetName(typeof(OrgLevelEnum), RoleLevel);
        }
        
    }

    [JsonProperty(PropertyName = "RD")]
    public string RoleDescription { get; set; }


  
}