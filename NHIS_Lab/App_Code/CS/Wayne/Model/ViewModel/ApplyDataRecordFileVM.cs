using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class ApplyDataRecordFileVM
{
    [JsonProperty(PropertyName = "AI")]
    public int ApplyDataID { get; set; }

    [JsonProperty(PropertyName = "FI")]
    public int FileInfoID { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }


}