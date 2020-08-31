using Newtonsoft.Json;

/// <summary>
/// EmiLogVM 的摘要描述
/// </summary>
public class EmiLogVM
{
    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "Seq")]
    public string Seq { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "Birthday")]
    public string Birthday { get; set; }

    [JsonProperty(PropertyName = "CaseID")]
    public string CaseID { get; set; }

    [JsonProperty(PropertyName = "EmiDate")]
    public string EmiDate { get; set; }

    [JsonProperty(PropertyName = "Status")]
    public int Status { get; set; }

    [JsonProperty(PropertyName = "strStatus")]
    public string strStatus {
        get {
            switch(Status)
            {
                case 0:
                    return "異常";
                case 1:
                    return "成功";
                case 2:
                    return "重複";
                default:
                    return "異常";
            }
        }
    }
}