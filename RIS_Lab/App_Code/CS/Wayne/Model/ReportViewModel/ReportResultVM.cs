using Newtonsoft.Json;
using System.Collections.Generic;

public class ReportResultVM
{
    [JsonProperty(PropertyName = "RS")]
    public int ResultState { get; set; }

    [JsonProperty(PropertyName = "RD")]
    public List<Dictionary<string,object>> ResultData { get; set; }

    [JsonConverter(typeof(ByteArrayConverter))]
    [JsonProperty(PropertyName = "RF")]
    public byte[] ResultFile { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }
}