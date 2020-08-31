using Newtonsoft.Json;
using System;

public class DiseaseVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string DiseaseCName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string DiseaseEName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string DiseaseStatus { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public int DiseaseSequence { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public int LogicDel { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public DateTime CreateDate { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string CreatAccount { get; set; }
        
    [JsonProperty(PropertyName = "c10")]
    public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "c11")]
    public string ModifyAccount { get; set; }
}