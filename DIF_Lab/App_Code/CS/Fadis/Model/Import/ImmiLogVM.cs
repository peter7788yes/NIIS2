using Newtonsoft.Json;


/// <summary>
/// ImmiLogVM 的摘要描述
/// </summary>
public class ImmiLogVM
{
    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "Birthday")]
    public string Birthday { get; set; }

    [JsonProperty(PropertyName = "Sex")]
    public bool Sex { get; set; }

    [JsonProperty(PropertyName = "strSex")]
    public string strSex { get { return Sex ? "男" : "女"; } }

    [JsonProperty(PropertyName = "CaseID")]
    public string CaseID { get; set; }

    [JsonProperty(PropertyName = "PassportNo")]
    public string PassportNo { get; set; }

    [JsonProperty(PropertyName = "CardNo")]
    public string CardNo { get; set; }

    [JsonProperty(PropertyName = "FlightDate")]
    public string FlightDate { get; set; }

    [JsonProperty(PropertyName = "FlightNo")]
    public string FlightNo { get; set; }

    [JsonProperty(PropertyName = "Port")]
    public string Port { get; set; }

    [JsonProperty(PropertyName = "ArrivalLoc")]
    public string ArrivalLoc { get; set; }

    [JsonProperty(PropertyName = "Address")]
    public string Address { get; set; }

    [JsonProperty(PropertyName = "OverseasAddr")]
    public string OverseasAddr { get; set; }

    [JsonProperty(PropertyName = "Reason")]
    public string Reason { get; set; }

    [JsonProperty(PropertyName = "Status")]
    public bool Status { get; set; }

    [JsonProperty(PropertyName = "strStatus")]
    public string strStatus { get { return Status ? "成功" : "異常"; } }
}