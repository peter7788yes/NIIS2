using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CheckFileLogListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }
     
    [JsonProperty(PropertyName = "ID")]
    public string ID { get; set; }

    [JsonProperty(PropertyName = "FileDate")]
    public string FileDate { get; set; }

    [JsonProperty(PropertyName = "MustCount")]
    public string MustCount { get; set; }

      [JsonProperty(PropertyName = "ActuallyCount")]
    public string ActuallyCount { get; set; }
     

    [JsonProperty(PropertyName = "CheckDate")]
    public string CheckDate { get; set; }

    [JsonProperty(PropertyName = "FinishDate")]
    public string FinishDate { get; set; }

  [JsonProperty(PropertyName = "TransferMsg")]
    public string TransferMsg { get; set; }
    
     
}