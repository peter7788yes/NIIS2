using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class LogListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }
     
    [JsonProperty(PropertyName = "ID")]
    public string ID { get; set; }

    [JsonProperty(PropertyName = "FileDate")]
    public string FileDate { get; set; }

    [JsonProperty(PropertyName = "FileName")]
    public string FileName { get; set; }

      [JsonProperty(PropertyName = "LogItemName")]
    public string LogItemName { get; set; }
     

    [JsonProperty(PropertyName = "CheckDate")]
    public string CheckDate { get; set; }


    [JsonProperty(PropertyName = "MustCount")]
    public string MustCount { get; set; }

    [JsonProperty(PropertyName = "ActuallyCount")]
    public string ActuallyCount { get; set; }

    [JsonProperty(PropertyName = "ItemID")]
    public string ItemID { get; set; }

    [JsonProperty(PropertyName = "LogID")]
    public string LogID { get; set; }

    [JsonProperty(PropertyName = "Y")]
    public string SucY { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string SucN { get; set; }
  
}