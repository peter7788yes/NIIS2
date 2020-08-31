using Newtonsoft.Json;
using System;

/// <summary>
/// MergeCheckListVM 的摘要描述
/// </summary>
public class MergeCheckListVM 
{
    [JsonProperty(PropertyName = "GID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int CaseID { get; set; }

     [JsonIgnore]
    public string IdNo { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string IDNOMask
    {
        get { return DataMask.IdentityNumber(IdNo); }
    }

    [JsonProperty(PropertyName = "N")]
    public string Name { get; set; } 
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


    [JsonProperty(PropertyName = "MC")]
    public string MotherCaseID { get; set; }
     [JsonIgnore]
    public string MotherIdNo { get; set; }

    [JsonProperty(PropertyName = "MI")]
    public string MotherIDMask
    {
        get
        {
            return DataMask.IdentityNumber(MotherIdNo);
        }
    }
     

    [JsonProperty(PropertyName = "MN")]
    public string MotherName { get; set; } 

    [JsonProperty(PropertyName = "BS")]
    public string BirthSeq { get; set; }
     

    
    [JsonProperty(PropertyName = "BM")]
    public string BirthMulti { get; set; }
    

    //父親或母親證號	父親或母親姓名	父親或母親生日	個案身份證字號	個案姓名	個案出生日期	同胎次序


    [JsonProperty(PropertyName = "C99")]
    public int CaseID99 { get; set; }

    [JsonIgnore]
    public string IdNo99 { get; set; }

    [JsonProperty(PropertyName = "I99")]
    public string IDNOMask99
    {
        get { return DataMask.IdentityNumber(IdNo99); }
    }

    [JsonProperty(PropertyName = "N9")]
    public string Name99 { get; set; }

    [JsonIgnore]
    public string BirthDate99 { get; set; }


    [JsonProperty(PropertyName = "BD99")]
    public string BirthDateTaiwan99
    {
        get { return DataMask.BirthDateTaiwan(BirthDate99); }
    }

 

    [JsonProperty(PropertyName = "BS99")]
    public string BirthSeq99 { get; set; }


    [JsonProperty(PropertyName = "UpdateDate")]
    public string UpdateDate { get; set; }

    [JsonProperty(PropertyName = "BM99")]
    public string BirthMulti99 { get; set; }

}