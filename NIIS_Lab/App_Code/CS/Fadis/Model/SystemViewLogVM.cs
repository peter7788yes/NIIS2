using Newtonsoft.Json;

/// <summary>
/// SystemViewLogVM 的摘要描述
/// </summary>
public class SystemViewLogVM
{
    [JsonProperty(PropertyName = "SID")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int ID { get; set; }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "OrgName")]
    public string OrgName
    {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    [JsonProperty(PropertyName = "ViewDate")]
    public string ViewDate { get; set; }

    [JsonProperty(PropertyName = "UserID")]
    public string UserID { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "DataType")]
    public string DataType { get; set; }

    [JsonProperty(PropertyName = "ViewType")]
    public string ViewType { get; set; }

    [JsonProperty(PropertyName = "FileID")]
    public string FileID { get; set; }

    [JsonProperty(PropertyName = "Cnt")]
    public int Cnt { get; set; }

    [JsonProperty(PropertyName = "DataID")]
    public string DataID { get; set; }

    [JsonProperty(PropertyName = "DataInfo")]
    public string DataInfo
    {
        get
        {
            if (ViewType.Equals("瀏覽資料"))
                return DataMask.IdentityNumber(DataID);
            else
                return Cnt.ToString();
        }
    }
}