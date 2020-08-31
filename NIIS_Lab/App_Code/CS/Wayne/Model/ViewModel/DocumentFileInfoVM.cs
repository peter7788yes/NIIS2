using Newtonsoft.Json;

public class DocumentFileInfoVM
{
    [JsonIgnore]
    public int DocumentInfoID { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }

    [JsonProperty(PropertyName = "F")]
    public int FileInfoID { get; set; }
}