using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class RegisterDataVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "B")]
    public DateTime BirthDate { get; set; }

    [JsonProperty(PropertyName = "IC")]
    public string IDCard { get; set; }

    [JsonProperty(PropertyName = "M")]
    public string MyName { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "MB")]
    public DateTime MotherBirthDate { get; set; }

    [JsonProperty(PropertyName = "MC")]
    public string MotherIDCard { get; set; }

    [JsonProperty(PropertyName = "PD")]
    public string PhoneNumberDay { get; set; }

    [JsonProperty(PropertyName = "PN")]
    public string PhoneNumberNight { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string CellPhone { get; set; }
}