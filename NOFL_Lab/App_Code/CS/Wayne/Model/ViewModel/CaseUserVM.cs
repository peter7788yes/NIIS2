using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class CaseUserVM
{

    [JsonProperty(PropertyName = "CN")]
    public string ChName { get; set; }

    [JsonProperty(PropertyName = "IN")]
    public string IdNo { get; set; }

    [JsonIgnore]
    public int Gender { get; set; }

    [JsonProperty(PropertyName = "G")]
    public string GenderString {
        get
        {
            return SystemCode.GetName("CaseUser_Gender", Gender);
        }
    }

    [JsonProperty(PropertyName = "BD")]
    public DateTime BirthDate { get; set; }

    [JsonProperty(PropertyName = "MI")]
    public string MontherIdNo { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "MD")]
    public DateTime MotherBirthDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public int ResCounty { get; set; }

    [JsonIgnore]
    public int ResTown { get; set; }

    [JsonIgnore]
    public int ResVillage { get; set; }

    [JsonIgnore]
    public string ResAddr { get; set; }

    [JsonProperty(PropertyName = "RA")]
    public string RAddress
    {
        get
        {
            return SystemAreaCode.GetName(ResCounty) + SystemAreaCode.GetName(ResTown) + SystemAreaCode.GetName(ResVillage) + ResAddr;
        }
    }

    [JsonIgnore]
    public int ConCounty { get; set; }

    [JsonIgnore]
    public int ConTown { get; set; }

    [JsonIgnore]
    public int ConVillage { get; set; }

    [JsonIgnore]
    public string ConAddr { get; set; }

    [JsonProperty(PropertyName = "CA")]
    public string CAddress
    {
        get
        {
            return SystemAreaCode.GetName(ConCounty) + SystemAreaCode.GetName(ConTown) + SystemAreaCode.GetName(ConVillage) + ConAddr;
        }
    }




}