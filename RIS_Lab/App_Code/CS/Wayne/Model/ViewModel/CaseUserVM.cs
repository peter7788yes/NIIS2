using Newtonsoft.Json;
using System;

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

    [JsonProperty(PropertyName = "HN")]
    public string HouseNo { get; set; }

    [JsonProperty(PropertyName = "L")]
    public string Language { get; set; }


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

    [JsonProperty(PropertyName = "CR")]
    public string CaseRemark { get; set; }

    [JsonProperty(PropertyName = "FN")]
    public int FluNotes { get; set; }

    [JsonProperty(PropertyName = "SC")]
    public int StoolCard { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime ApplyDate { get; set; }

    [JsonProperty(PropertyName = "YUN")]
    public string YellowCardCreatedUserName { get; set; }

    
    [JsonIgnore]
    public int YellowCardCreatedUserOrgID { get; set; }

    [JsonProperty(PropertyName = "YO")]
    public string YellowCardCreatedUserOrgIDString {
        get
        {
            return SystemOrg.GetName(YellowCardCreatedUserOrgID);
        }
    }

    [JsonProperty(PropertyName = "HY")]
    public bool HasYellowCardMessage { get; set; }

}