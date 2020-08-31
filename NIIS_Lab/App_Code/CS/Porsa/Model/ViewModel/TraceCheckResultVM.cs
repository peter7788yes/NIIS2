using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class TraceCheckResultVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "BN")]
    public string babyName { get; set; }

    [JsonProperty(PropertyName = "BB")]
    public string babyBirth { get; set; }

    [JsonProperty(PropertyName = "BIN")]
    public string babyIdNo { get; set; }

    [JsonProperty(PropertyName = "BIDID")]
    public int BInfantDataID { get; set; }

    [JsonProperty(PropertyName = "BID")]
    public int babyCaseID { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public string babyDraw { get; set; }

    [JsonProperty(PropertyName = "BCON")]
    public string babyCheckOrgName { get; set; }

    [JsonProperty(PropertyName = "BSAG")]
    public string babyHBsAg { get; set; }

    [JsonProperty(PropertyName = "BAS")]
    public string babyAntiHBs { get; set; }

    [JsonProperty(PropertyName = "BAC")]
    public string babyAntiHBc { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "MB")]
    public string MotherBirth { get; set; }

    [JsonProperty(PropertyName = "MIN")]
    public string MotherIdNo { get; set; }

    [JsonProperty(PropertyName = "MD")]
    public string MotherDraw { get; set; }

    [JsonProperty(PropertyName = "MPON")]
    public string motherPreOrgName { get; set; }

    [JsonProperty(PropertyName = "MBSAG")]
    public string motherHBsAg { get; set; }

    [JsonProperty(PropertyName = "MBEAG")]
    public string motherHBeAg { get; set; }

}