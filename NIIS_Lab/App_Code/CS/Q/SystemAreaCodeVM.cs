using Newtonsoft.Json;
using System;
using System.ComponentModel;

public class SystemAreaCodeVM
{
     

     [DefaultValue(0)]
     [JsonProperty(PropertyName = "I")]
      public int ID { get; set; }
     
     [JsonProperty(PropertyName = "N")]
     public string AreaName { get; set; }

     [JsonProperty(PropertyName = "C")]
     public string AreaCode { get; set; }

     [JsonProperty(PropertyName = "K")]
     public string CodeKey { get; set; }

     [JsonProperty(PropertyName = "P")]
     public int AreaParent { get; set; }

     [DefaultValue(0)]
     [JsonProperty(PropertyName = "O",DefaultValueHandling = DefaultValueHandling.Ignore)]
     public int OrderNumber {get;set;}
     
     
}