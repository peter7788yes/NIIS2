using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class RegisterStoolCardVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "CD")]
    public DateTime CheckDate { get; set; }

    [JsonIgnore]
    public int StoolCard { get; set; }

    [JsonProperty(PropertyName = "SC")]
    public String StoolCardSring
    {
        get
        {
            return SystemCode.GetName("RecordM_RegisterData_Detail_StoolCard", StoolCard);
        }

    }

    [JsonProperty(PropertyName = "SL")]
    public string ScreeningLocation { get; set; }

}