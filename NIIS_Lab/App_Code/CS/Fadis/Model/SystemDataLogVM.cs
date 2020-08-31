using Newtonsoft.Json;

/// <summary>
/// SystemDataLogVM 的摘要描述
/// </summary>
public class SystemDataLogVM
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

    [JsonProperty(PropertyName = "ModifyDate")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "DataID")]
    public string DataID { get; set; }

    [JsonProperty(PropertyName = "DataIDDisplay")]
    public string DataIDDisplay
    {
        get
        {
            return DataType.Equals("4") ? DataID : DataMask.IdentityNumber(DataID);
        }
    }

    [JsonProperty(PropertyName = "UserID")]
    public string UserID { get; set; }

    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "DataType")]
    public string DataType { get; set; }

    [JsonProperty(PropertyName = "ModifyType")]
    public string ModifyType { get; set; }

    [JsonProperty(PropertyName = "DataLog")]
    public string DataLog { get; set; }
}