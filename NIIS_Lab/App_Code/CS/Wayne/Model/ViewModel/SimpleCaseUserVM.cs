using Newtonsoft.Json;
using System;

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