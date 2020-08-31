using Newtonsoft.Json;
using System;
using System.ComponentModel;

public class SystemRegionSettingVM
{
    
     [JsonProperty(PropertyName = "S")]
      public int Setting { get; set; }
     
     [JsonProperty(PropertyName = "R")]
     public int RegionID { get; set; }

     [JsonProperty(PropertyName = "C")]
     public int CountyID { get; set; }

     [JsonProperty(PropertyName = "T")]
     public int TownID { get; set; }

     [JsonProperty(PropertyName = "V")]
     public int VillageID { get; set; }
   
     
}