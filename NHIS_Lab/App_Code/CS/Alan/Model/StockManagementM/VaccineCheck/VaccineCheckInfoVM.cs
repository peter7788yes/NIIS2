using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineCheckDataVM 的摘要描述
/// </summary>
public class VaccineCheckInfoVM
{
    [JsonIgnore]
    public int CheckOrgID { get; set; }

    [JsonProperty(PropertyName = "c1")]
    public string CheckOrgName 
    {
        get
        {
            return SystemOrg.GetName(CheckOrgID);
        }
    }

    [JsonProperty(PropertyName = "c2")]
    public string CheckDate { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string CheckAccountName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int Result { get; set; }

}