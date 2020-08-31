using Newtonsoft.Json;

public class CertificateSignVM
{
    [JsonProperty(PropertyName = "I")]
    public int CertificateSignID { get; set; }

    [JsonProperty(PropertyName = "O")]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string OrgName {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    [JsonProperty(PropertyName = "P")]
    public string PhysicianSignature { get; set; }

    [JsonProperty(PropertyName = "D")]
    public string UnitDirector { get; set; }

    [JsonProperty(PropertyName = "S")]
    public string UnitStamp { get; set; }

    [JsonProperty(PropertyName = "E")]
    public string EnglishFullTitle { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string ChineseFullTitle { get; set; }





}