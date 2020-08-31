using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CheckUserListVM
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
     

   [JsonProperty(PropertyName = "N")]
    public string Name { get; set; }
     

    [JsonProperty(PropertyName = "AddrCountyTownName")]
    public string AddrCountyTownName { get; set; }

    [JsonProperty(PropertyName = "UpdateDate")]
    public string UpdateDate { get; set; }
}