using Newtonsoft.Json;
using System;

public class OrgContractVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "CS")]
    public DateTime ContractStart { get; set; }

    [JsonProperty(PropertyName = "CE")]
    public DateTime ContractEnd { get; set; }

    [JsonProperty(PropertyName = "CST")]
    public DateTime ContractStop { get; set; }
}
