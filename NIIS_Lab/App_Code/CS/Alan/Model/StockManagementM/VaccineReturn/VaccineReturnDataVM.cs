using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VaccineReturnDataVM 的摘要描述
/// </summary>
public class VaccineReturnDataVM
{
    [JsonProperty(PropertyName = "c1")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "c2")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "c3")]
    public int ReturnOrgID { get; set; }

    [JsonProperty(PropertyName = "c4")]
    public string ReturnOrgName { get; set; }

    [JsonProperty(PropertyName = "c5")]
    public string DealDate { get; set; }

    [JsonProperty(PropertyName = "c6")]
    public string ReturnTargName { get; set; }

    [JsonProperty(PropertyName = "c7")]
    public string Remark { get; set; }

    [JsonProperty(PropertyName = "c8")]
    public int ReturnStatus { get; set; }

    [JsonProperty(PropertyName = "c9")]
    public string ReturnStatusName
    {
        get
        {
            return SystemCode.GetName("StockManagementM_ReturnStatus", ReturnStatus);
        }
    }

    [JsonIgnore]
    public int BackReason { get; set; }

    [JsonProperty(PropertyName = "c10")]
    public string BackReasonName
    {
        get
        {
            return SystemCode.GetName("StockManagementM_BackReason", BackReason);
        }
    }
}