﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineUseDataVM 的摘要描述
/// </summary>
public class VaccineUseDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int UseOrgID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string UseOrgName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public int ModifyAccount { get; set; }
}