using Newtonsoft.Json;
using System;

public class SystemCodeCateVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string CodeKey { get; set; }

    [JsonProperty(PropertyName = "CD")]
    public string CodeDescription { get; set; }

    [JsonProperty(PropertyName = "R")]
    public int RecordCount { get; set; }

    [JsonIgnore]
    public bool CanEdit { get; set; }

}