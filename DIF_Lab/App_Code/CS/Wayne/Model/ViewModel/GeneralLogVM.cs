using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class GeneralLogVM
{

    
    [JsonProperty(PropertyName = "CT")]
    public int CreateType { get; set; }

    [JsonProperty(PropertyName = "CD")]
    public DateTime ChangeDate { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public int ChangeUserID { get; set; }

    [JsonIgnore]
    public int ChangeOrgID { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string ChangeOrgIDString
    {
        get
        {
            return SystemOrg.GetName(ChangeOrgID);
        }
    }
}
