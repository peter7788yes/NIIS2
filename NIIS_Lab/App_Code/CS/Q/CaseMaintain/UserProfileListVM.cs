using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserProfileListVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int CaseID { get; set; }

     [JsonIgnore]
    public string IDNO { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string IDNOMask
    {
        get { return DataMask.IdentityNumber(IDNO); }
    }

     [JsonIgnore]
    public string BirthDate { get; set; }


    [JsonProperty(PropertyName = "BD")]
    public string BirthDateTaiwan
    {
        get { return DataMask.BirthDateTaiwan(BirthDate); }
    }

     [JsonIgnore]
    public string MotherBirthDate { get; set; }

    [JsonProperty(PropertyName = "MBD")]
    public string MotherBirthDateTaiwan
    {
        get
        {
            return   DataMask.BirthDateTaiwan(MotherBirthDate);
        }
    }


     [JsonIgnore]
    public string MotherID { get; set; }

    [JsonProperty(PropertyName = "MI")]
    public string MotherIDMask
    {
        get
        {
            return DataMask.IdentityNumber(MotherID);
        }
    }


    [JsonProperty(PropertyName = "MC")]
    public string MotherCaseID { get; set; }

   [JsonProperty(PropertyName = "N")]
    public string Name { get; set; }

  
    //public string NameMask
    //{
    //    get
    //    {
    //        return DataMask.Name(Name);
    //    }
    //}

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "TD")]
    public string TelD { get; set; }

    [JsonProperty(PropertyName = "TN")]
    public string TelN { get; set; }

    [JsonProperty(PropertyName = "MM")]
    public string MotherMobile
    { get; set; }

    [JsonProperty(PropertyName = "BS")]
    public string BirthSeq { get; set; }



    [JsonProperty(PropertyName = "GID")]
    public string GroupID { get; set; }


    [JsonProperty(PropertyName = "AddrCountyTownName")]
    public string AddrCountyTownName { get; set; }

}