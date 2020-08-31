using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// VaccineVM 的摘要描述
/// </summary>
public class VaccineVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public int VaccineType { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string VaccineEName { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int VaccineUsage { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string VaccineUsageString
    {
        get
        {
            return Enum.GetName(typeof(VaccineUsageEnum), VaccineUsage);
        }

    }

    [JsonProperty(PropertyName = "c9")]
    public string VaccineCent { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string VaccineCentOther { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public int VaccineNum { get; set; }

    [JsonProperty(PropertyName = "c12")]
    public int SafeNum { get; set; }

    [JsonProperty(PropertyName = "c13")]
    public int AvaPeriod { get; set; }

    [JsonProperty(PropertyName = "c14")]
    public int Storage { get; set; }

    [JsonProperty(PropertyName = "c15")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c16")]
    public string RemarkFile { get; set; }

    [JsonProperty(PropertyName = "c17")]
    public int VaccineSequence { get; set; }

    [JsonProperty(PropertyName = "c18")]
    public int VaccineStatus { get; set; }

    [JsonProperty(PropertyName = "c19")]
    public string VaccineStatusString
    {
        get
        {
            return Enum.GetName(typeof(VaccineStatusEnum), VaccineStatus);
        }

    }

    [JsonProperty(PropertyName = "c20")]
    public string CreateAccount { get; set; }

    [JsonProperty(PropertyName = "c21")]
    DateTime CreateDate { get; set; }

    [JsonProperty(PropertyName = "c22")]
    public string ModifyAccount { get; set; }

    [JsonProperty(PropertyName = "c23")]
    DateTime ModifyDate { get; set; }


}