using Newtonsoft.Json;

public class AddVaccineVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "VI")]
    public string VaccineID { get; set; }

    [JsonProperty(PropertyName = "VC")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "VE")]
    public string VaccineEName { get; set; }

    [JsonProperty(PropertyName = "IC")]
    public bool IsChecked { get; set; }

}