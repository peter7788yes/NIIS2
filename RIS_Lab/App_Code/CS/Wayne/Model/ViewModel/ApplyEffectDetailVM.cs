using Newtonsoft.Json;
using System;

public class ApplyEffectDetailVM
{
    [JsonProperty(PropertyName = "ET")]
    public int EffectType { get; set; }

    //EffectType,EffectValue,EffectScopeValue,EffectStartDate,EffectDays

    [JsonProperty(PropertyName = "EV")]
    public int EffectValue { get; set; }

    [JsonProperty(PropertyName = "ESV")]
    public int EffectScopeValue { get; set; }

    [JsonIgnore]
    public DateTime EffectStartDate { get; set; }

    [JsonProperty(PropertyName = "ES")]
    public string EffectStartDateString {
        get
        {
            return EffectStartDate.ToShortTaiwanDate();
        }
    }

    [JsonProperty(PropertyName = "ED")]
    public int EffectDays { get; set; }

}