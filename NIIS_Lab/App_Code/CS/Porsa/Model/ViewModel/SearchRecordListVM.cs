using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class SearchRecordVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string IDNO { get; set; }

    [JsonProperty(PropertyName = "CI")]
    public string CaseID { get; set; }

    [JsonProperty(PropertyName = "BID")]
    public int BPreDataID { get; set; }

    [JsonProperty(PropertyName = "BIM")]
    public int BPreMainID { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public string BirthDate { get; set; }

    [JsonProperty(PropertyName = "ED")]
    public string ExpDate { get; set; }

    [JsonProperty(PropertyName = "DD")]
    public string DrawDate { get; set; }

    [JsonProperty(PropertyName = "sAg")]
    public string HBsAg { get; set; }

    [JsonProperty(PropertyName = "eAg")]
    public string HBeAg { get; set; }

    //[JsonProperty(PropertyName = "PID")]
    public string PreID { get; set; }

    [JsonProperty(PropertyName = "PID")]
    public string OrgName { get {
            if(this.PreID == "")
            {
                return "";
            }
            else
            {
                return SystemOrg.GetName(Int32.Parse(this.PreID));
            }} set { this.PreID = value; }}
}