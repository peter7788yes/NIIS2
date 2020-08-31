using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class ApplyRecordVM
{

    [JsonProperty(PropertyName = "I")]
    public int ApplyRecordID { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public DateTime InoculationDate { get; set; }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    [JsonProperty(PropertyName = "BID")]
    public string BatchID { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonIgnore]
    public int ReSignReason { get; set; }

    [JsonIgnore]
    public int ReInoculationReason { get; set; }

    [JsonIgnore]
    public int EarlyLateReason { get; set; }

    [JsonProperty(PropertyName = "RSR")]
    public string ReSignReasonString {
        get
        {
            
            return SystemCode.GetName("RecordM_ApplyRecord_ReSignReason", ReSignReason);
        }
    }

    [JsonProperty(PropertyName = "RIR")]
    public string ReInoculationReasonString
    {
        get
        {
            return SystemCode.GetName("RecordM_ApplyRecord_ReInoculationReason", ReSignReason);
        }
    }

    [JsonProperty(PropertyName = "ELR")]
    public string EarlyLateReasonString
    {
        get
        {
            return SystemCode.GetName("RecordM_ApplyRecord_EarlyLateReason", ReSignReason);
        }
    }

    [JsonProperty(PropertyName = "RS")]
    public string ReasonString { get; set; }

    [JsonIgnore]
    public int CreateType { get; set; }

    [JsonProperty(PropertyName = "CT")]
    public string CreateTypeString {
        get
        {
            string rtn = "";
            switch(CreateType)
            {
                case 1:
                    rtn = "健保上傳";
                    break;
                case 2:
                    rtn = "媒體上傳";
                    break;
            }
            return rtn;
        }
    }



}