using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ICalculatorProvider 的摘要描述
/// </summary>
public class ReportResultVM
{
    [JsonProperty(PropertyName = "RS")]
    public int ResultState { get; set; }

    [JsonProperty(PropertyName = "RD")]
    public List<BaseReportRecordVM> ResultData { get; set; }

    [JsonConverter(typeof(ByteArrayConverter))]
    [JsonProperty(PropertyName = "RF")]
    public byte[] ResultFile { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }

}