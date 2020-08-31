using Newtonsoft.Json;
using System;

public class LogVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonIgnore]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ThreadID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public string MachineName { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string LogName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string LogLevel { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string LogMessage { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string CallSite { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public string Exception { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string Stacktrace { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public DateTime CreateDateTime { get; set; }
}