using Newtonsoft.Json;
using System;

public class DefaultBatchVaccineVM
{
    [JsonProperty(PropertyName = "VI")]
    public int VaccineBatchID { get; set; }

    [JsonProperty(PropertyName = "BI")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime AvaDate { get; set; }

    [JsonProperty(PropertyName = "BB")]
    public string BatchBrand { get; set; }

    [JsonProperty(PropertyName = "DP")]
    public float DosePer { get; set; }

    [JsonIgnore]
    public int FormDrug { get; set; }


    [JsonProperty(PropertyName = "FD")]
    public string FormDrugString {
        get {
            SystemCode.GetName("VaccineM_BatchFormDrug", FormDrug);
            return "";
        }
    }


}

