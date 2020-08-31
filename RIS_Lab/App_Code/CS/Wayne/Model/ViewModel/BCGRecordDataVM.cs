using Newtonsoft.Json;

public class BCGRecordDataVM
{

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "KN")]
    public int KidNumber { get; set; }

    [JsonProperty(PropertyName = "KT")]
    public int KidType { get; set; }

    [JsonProperty(PropertyName = "TT")]
    public int TestType { get; set; }


}