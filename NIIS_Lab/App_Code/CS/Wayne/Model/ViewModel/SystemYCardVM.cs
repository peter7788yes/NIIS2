using Newtonsoft.Json;

public class SystemYCardVM
{
    [JsonProperty(PropertyName = "YMID")]
    public int YCardMID { get; set; }

    [JsonProperty(PropertyName = "AE")]
    public string AgeEngilsh { get; set; }

    [JsonProperty(PropertyName = "DID")]
    public string DoseID { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int Period { get; set; }

    [JsonProperty(PropertyName = "YT")]
    public int YCardDataType { get; set; }

}