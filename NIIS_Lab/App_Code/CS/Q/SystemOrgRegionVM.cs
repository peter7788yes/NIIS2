using Newtonsoft.Json;
using System;
using System.ComponentModel;
/// <summary>
/// 轄區設定
/// </summary>
public class SystemOrgRegionVM
{

    /// <summary>
    /// Seq
    /// </summary>
    [JsonProperty(PropertyName = "S")]
    public int ID { get; set; }

    /// <summary>
    /// OrgID
    /// </summary>
     [JsonProperty(PropertyName = "O")]      
    public int OrgID { get; set; }
     
      
     /// <summary>
     /// 轄區代碼
     /// </summary>
     [JsonProperty(PropertyName = "R")]
     public int  RegionID { get; set; }
     
     
}