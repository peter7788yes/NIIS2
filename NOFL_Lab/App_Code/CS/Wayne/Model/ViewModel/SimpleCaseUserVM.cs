using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class SimpleCaseUserVM
{

    [JsonProperty(PropertyName = "CN")]
    public string ChName { get; set; }

    [JsonProperty(PropertyName = "IN")]
    public string IdNo { get; set; }

    [JsonIgnore]
    public int Gender { get; set; }

    [JsonProperty(PropertyName = "G")]
    public string GenderString
    {
        get
        {
            return SystemCode.GetName("CaseUser_Gender", Gender);
        }
    }


    [JsonProperty(PropertyName = "U")]
    public DateTime BirthDate { get; set; }

   

}