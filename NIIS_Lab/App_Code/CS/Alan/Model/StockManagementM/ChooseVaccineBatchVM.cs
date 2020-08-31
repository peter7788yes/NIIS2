using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ChooseVaccineBatchVM 的摘要描述
/// </summary>
public class ChooseVaccineBatchVM
{
    [JsonProperty(PropertyName = "c1")]
    public string ID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string AvaDate { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string FormDrugName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public float Price { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string BatchTypeName { get; set; }    
}