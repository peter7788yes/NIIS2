using Newtonsoft.Json;

public class SystemOrgVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "O")]
	public string OrgName { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int PID { get; set; }

    [JsonProperty(PropertyName = "sort")]
    public int OrderNumber { get; set; }
    
    [JsonIgnore]
    public int OrgCateID { get; set; }

    [JsonIgnore]
    public string IpStart { get; set; }

    [JsonIgnore]
    public string IpEnd { get; set; }

    [JsonIgnore]
    public int OrgLevel { get; set; }

    [JsonIgnore]
    public int AgencyCounty { get; set; }

    [JsonIgnore]
    public int AgencyTown { get; set; }

    [JsonIgnore]
    public int AgencyVillage { get; set; }
    

}