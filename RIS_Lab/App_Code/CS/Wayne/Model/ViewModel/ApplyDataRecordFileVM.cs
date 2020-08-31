using Newtonsoft.Json;

public class ApplyDataRecordFileVM
{
    [JsonProperty(PropertyName = "AI")]
    public int ApplyDataID { get; set; }

    [JsonProperty(PropertyName = "FI")]
    public int FileInfoID { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }


}