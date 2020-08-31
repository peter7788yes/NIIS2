using Newtonsoft.Json;
using System;

public class ApplyDataRecordVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ApplyDataID { get; set; }

    [JsonProperty(PropertyName = "CN")]
    public string CaseUserChName { get; set; }

    [JsonProperty(PropertyName = "EN")]
    public string CaseUserEnName { get; set; }

    [JsonProperty(PropertyName = "AN")]
    public string ApplyUserName { get; set; }

    [JsonProperty(PropertyName = "UR")]
    public string UserRelationship { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "CD")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime ApplyDate { get; set; }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "O")]
    public string OrgName {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

}