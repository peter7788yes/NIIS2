using Newtonsoft.Json;

/// <summary>
/// EmiMasterVM 的摘要描述
/// </summary>
public class EmiMasterVM
{
    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "CreateDate")]
    public string CreateDate { get; set; }

    [JsonProperty(PropertyName = "SuccessCnt")]
    public int SuccessCnt { get; set; }

    [JsonProperty(PropertyName = "RepeatCnt")]
    public int RepeatCnt { get; set; }

    [JsonProperty(PropertyName = "ErrorCnt")]
    public int ErrorCnt { get; set; }

    [JsonProperty(PropertyName = "TotalCnt")]
    public int TotalCnt { get { return SuccessCnt + RepeatCnt + ErrorCnt; } }
}