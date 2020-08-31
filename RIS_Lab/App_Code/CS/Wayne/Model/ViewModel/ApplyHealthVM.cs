using Newtonsoft.Json;
using System;

public class ApplyHealthVM
{
    [JsonProperty(PropertyName = "I")]
    public int ApplyHealthID { get; set; }

    [JsonProperty(PropertyName = "AUID")]
    public int AssessmentUserID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string AssessmentUserName { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime AssessmentDate { get; set; }

    [JsonProperty(PropertyName = "AW")]
    public bool AllowWork { get; set; }

    [JsonIgnore]
    public string ApplyHealthCateIDs { get; set; }

    [JsonProperty(PropertyName = "AS")]
    public string ApplyHealthCateIDsString
    {
        get
        {
            return string.Format(",{0},", ApplyHealthCateIDs.Trim(','));
        }
    }



    [JsonProperty(PropertyName = "OS")]
    public string OtherState { get; set; }

    [JsonProperty(PropertyName = "RID")]
    public int RecordDataID { get; set; }

    [JsonProperty(PropertyName = "OID")]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "SOF")]
    public int SelfOrFamily { get; set; }
}