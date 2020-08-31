using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class ElementaryRecordDataVM
{

    [JsonProperty(PropertyName = "I")]
    public int ElementaryRecordDataID { get; set; }

    [JsonProperty(PropertyName = "VID")]
    public int VaccineDataID { get; set; }

    [JsonProperty(PropertyName = "VT")]
    public int VaccineType { get; set; }

    [JsonProperty(PropertyName = "IN")]
    public int InoculationNumber { get; set; }

    [JsonProperty(PropertyName = "SIN")]
    public int ShouldInoculationNumber { get; set; }


}