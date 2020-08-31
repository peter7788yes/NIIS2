using Newtonsoft.Json;

public class AgencyVaccineVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "V")]
    public int VaccineCName { get; set; }

}