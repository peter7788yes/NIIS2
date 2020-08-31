using Newtonsoft.Json;
using System;
using System.ComponentModel;

public class ChooseVillageVM
{
     
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }


     [DefaultValue(0)]
     [JsonProperty(PropertyName = "I")]
    public int VillageID { get; set; }
     
     [JsonProperty(PropertyName = "CN")]
     public string CountyName { get; set; }

     [JsonProperty(PropertyName = "TN")]
     public string TownName { get; set; }

     [JsonProperty(PropertyName = "N")]
     public string VillageName { get; set; }
     
     
     
     
}