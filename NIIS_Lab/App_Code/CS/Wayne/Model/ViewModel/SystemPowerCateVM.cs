using Newtonsoft.Json;

public class SystemPowerCateVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "SP")]
	public string SystemPowerCateName { get; set; }

}