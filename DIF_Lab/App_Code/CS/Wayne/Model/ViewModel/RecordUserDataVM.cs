﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class RecordUserDataVM
{

    [JsonProperty(PropertyName = "RID")]
    public int RecordDataID { get; set; }

    [JsonProperty(PropertyName = "SRVID")]
    public int SystemRecordVaccineID { get; set; }

    [JsonProperty(PropertyName = "SRVC")]
    public string SystemRecordVaccineCode {
        get {
            return SystemRecordVaccine.GetName(SystemRecordVaccineID);
        }
    }

    [JsonProperty(PropertyName = "ID")]
    public DateTime InoculationDate { get; set; }

    [JsonProperty(PropertyName = "CD")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty(PropertyName = "OI")]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "CT")]
    public int CreateType { get; set; }

    [JsonProperty(PropertyName = "VB")]
    public int VaccineBatchID { get; set; }


}