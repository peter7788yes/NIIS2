using Newtonsoft.Json;
using System;

public class SystemRecordVaccineVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "R")]
	public string SystemRecordVaccineCode { get; set; }
}