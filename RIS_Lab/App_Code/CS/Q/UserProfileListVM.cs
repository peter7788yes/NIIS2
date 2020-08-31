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
    public string IDNO { get; set; }

    [JsonProperty(PropertyName = "BirthDate")]
    public string BirthDate { get; set; }

    [JsonProperty(PropertyName = "ResAddr")]
    public string ResAddr { get; set; }

    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "MotherName")]
    public string MotherName { get; set; }

    [JsonProperty(PropertyName = "FatherName")]
    public string FatherName { get; set; }
     
}