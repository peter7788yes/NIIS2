using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class VaccineByOrgVM
{
    [JsonIgnore]
    public int VaccineID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string VaccineIDString {
        get
        {
            return VaccineID.ToString();
        }
    }

    [JsonProperty(PropertyName = "V")]
    public string VaccineCName { get; set; }

}