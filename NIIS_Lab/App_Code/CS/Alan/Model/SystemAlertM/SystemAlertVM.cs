using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SystemAlertVM 的摘要描述
/// </summary>
public class SystemAlertVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int AlertType { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string AlertTypeName
    {
        get
        {
            return SystemCode.GetName("SystemAlertM_AlertType", AlertType);
        }
    }

    [JsonProperty(PropertyName = "c5")]
    public string AlertContent { get; set; }
}