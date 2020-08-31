using Newtonsoft.Json;
using System.Collections.Generic;

public class OrgManagementVM
{
    public OrgManagementVM()
    {
        OrgAllowIPList = new List<OrgAllowIPVM>();
    }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "AC")]
    public string AgencyCode { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string OrgAgencyName { get; set; }

    [JsonProperty(PropertyName = "EN")]
    public string OrgAgencyEnName { get; set; }

    [JsonProperty(PropertyName = "SN")]
    public string OrgAgencyShortName { get; set; }

    [JsonProperty(PropertyName = "OL")]
    public int OrgLevel { get; set; }

    [JsonProperty(PropertyName = "AS")]
    public int AgencyState { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public int OrderNumber { get; set; }

    [JsonProperty(PropertyName = "IP")]
    public List<OrgAllowIPVM> OrgAllowIPList { get; set; }

}