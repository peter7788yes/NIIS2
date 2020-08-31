using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// UserOTPVM 的摘要描述
/// </summary>
public class UserOTPVM
{
    public UserOTPVM()
    {
    }

    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "RocID")]
    public string RocID { get; set; }

    [JsonProperty(PropertyName = "UserID")]
    public string UserID { get; set; }

    [JsonProperty(PropertyName = "LoginName")]
    public string LoginName { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "OrgName")]
    public string OrgAgencyName { get; set; }

    [JsonProperty(PropertyName = "ApplyTime")]
    public string ApplyTime { get; set; }

    [JsonProperty(PropertyName = "AuditStatus")]
    public int AuditStatus { get; set; }

    [JsonProperty(PropertyName = "AuditComment")]
    public string AuditComment { get; set; }

    [JsonProperty(PropertyName = "IsBusiness")]
    public bool IsBusiness { get; set; }

    public string OTPCode { get; set; }

    public DateTime AuditTime { get; set; }
}