using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserProfileListVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

     [JsonProperty(PropertyName = "CaseID")]
    public int CaseID { get; set; }

    //[JsonProperty(PropertyName = "IDNO")]
    // public string IDNOM
    // {
    //    // get { return  }
           
    // }

    [JsonProperty(PropertyName = "IDNO")]
     public string IDNOM { get { return DataMask.IdentityNumber(IDNO); } }
    [JsonIgnore]
    public string IDNO { get; set; }

    [JsonProperty(PropertyName = "BirthDate")]
    public string BirthDateM { get { return DataMask.BirthDateTaiwan(BirthDate); } }
    [JsonIgnore]
    public string BirthDate { get; set; }

    [JsonProperty(PropertyName = "ResAddr")]
    public string ResAddrM { get { return DataMask.Address(ResAddr); } }
    [JsonIgnore]
    public string ResAddr { get; set; }

    [JsonProperty(PropertyName = "Name")]
    public string NameM { get { return DataMask.Name(Name); } }
    [JsonIgnore]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "MotherName")] 
    public string MotherNameM { get { return DataMask.Name(MotherName); } }
    [JsonIgnore]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "FatherName")]
    public string FatherNameM { get { return DataMask.Name(FatherName);} }
    [JsonIgnore]
    public string FatherName { get; set; }
     
}