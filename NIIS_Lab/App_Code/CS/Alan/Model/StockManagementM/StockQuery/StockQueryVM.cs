﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// StockQueryVM 的摘要描述
/// </summary>
public class StockQueryVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineIDName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string BatchTypeName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string FormDrugName { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public float DosePer { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public float StorageDose { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string AvaDate { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public int Remaining { get; set; }

}