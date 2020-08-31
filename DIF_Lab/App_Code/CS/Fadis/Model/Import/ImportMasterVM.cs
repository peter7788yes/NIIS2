using Newtonsoft.Json;

/// <summary>
/// ImportMasterVM 的摘要描述
/// </summary>
public class ImportMasterVM
{
    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "CreateDate")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "DataCount")]
    public int DataCount { get; set; }

    [JsonProperty(PropertyName = "ErrorMsg")]
    public string ErrorMsg { get; set; }

    [JsonProperty(PropertyName = "IsSuccess")]
    public bool IsSuccess { get; set; }

    [JsonProperty(PropertyName = "Status")]
    public string Status { get { return IsSuccess ? "成功" : "失敗"; } }
}