using Newtonsoft.Json;
using System;

public class ApplyEffectVM
{
    [JsonProperty(PropertyName = "I")]
    public int ApplyEffectID { get; set; }

    [JsonProperty(PropertyName = "SD")]
    public DateTime SignDate { get; set; }

    [JsonProperty(PropertyName = "ED")]
    public DateTime EffectDate { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "D3")]
    public bool Day3 { get; set; }

    [JsonProperty(PropertyName = "D7")]
    public bool Day7 { get; set; }

    [JsonIgnore]
    public string ApplyEffectCateIDs { get; set; }

    [JsonProperty(PropertyName = "AS")]
    public string ApplyEffectCateIDsString
    {
        get
        {
            string rtn = "";
            if(ApplyEffectCateIDs != null)
            {
                string.Format(",{0},", ApplyEffectCateIDs.Trim(','));
            }
            return rtn;
        }
    }
}