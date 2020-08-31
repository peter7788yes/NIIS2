using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class MyPartialDataVM
{
    [JsonProperty(PropertyName = "EN")]
    public string EnumName { get; set; }

    [JsonProperty(PropertyName = "EV")]
    public int EnumValue { get; set; }

    [JsonProperty(PropertyName = "DA")]
    public List<SystemCodeVM> DataAry { get; set; }

    [JsonIgnore]
    public DateTime EffectStartDate { get; set; }

    [JsonProperty(PropertyName = "ES")]
    public string EffectStartDateString { get { return EffectStartDate.ToShortTaiwanDate(); } }


    [JsonProperty(PropertyName = "ED")]
    public int EffectDays { get; set; }

}