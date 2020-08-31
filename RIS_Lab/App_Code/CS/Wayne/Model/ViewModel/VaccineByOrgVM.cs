using Newtonsoft.Json;

public class VaccineByOrgVM
{
    [JsonIgnore]
    public int VaccineID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string VaccineIDString {
        get
        {
            return VaccineID.ToString();
        }
    }

    [JsonProperty(PropertyName = "V")]
    public string VaccineCName { get; set; }

    [JsonProperty(PropertyName = "E")]
    public string VaccineEName { get; set; }

    [JsonProperty(PropertyName = "EV")]
    public string VaccineENameVaccineCName {
        get
        {
            return VaccineEName + " " + VaccineCName;

        }
    }


}