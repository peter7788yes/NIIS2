using Newtonsoft.Json;

public class SystemOrgVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "O")]
	public string OrgName { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int PID { get; set; }

    [JsonIgnore]
    public int OrgCateID { get; set; }

    [JsonIgnore]
    public string IpStart { get; set; }

    [JsonIgnore]
    public string IpEnd { get; set; }

    [JsonIgnore]
    public int OrgLevel { get; set; }

}