using Newtonsoft.Json;

public class RecordYellowDataVM
{
    [JsonProperty(PropertyName = "AE")]
    public string AgeEngilsh { get; set; }

    [JsonProperty(PropertyName = "DID")]
    public string DoseID { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int Period { get; set; }

}