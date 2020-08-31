using Newtonsoft.Json;
using System;
using System.ComponentModel;

public class SystemCodeVM
{
     [DefaultValue(0)]
     [JsonProperty(PropertyName = "S", DefaultValueHandling = DefaultValueHandling.Ignore)]
     public int SID {get;set;}

     [DefaultValue(0)]
     [JsonProperty(PropertyName = "I", DefaultValueHandling = DefaultValueHandling.Ignore)]
     public int ID { get; set; }


     [JsonProperty(PropertyName = "EN")]
     public string EnumName {get;set;}

     [JsonProperty(PropertyName = "EV")]
     public int EnumValue {get;set;}

     [JsonIgnore]
     public string CodeKey {get;set;}

     [JsonIgnore]
     public string CodeDescription { get; set; }

     [DefaultValue(0)]
     [JsonProperty(PropertyName = "O",DefaultValueHandling = DefaultValueHandling.Ignore)]
     public int OrderNumber {get;set;}

     [JsonProperty(PropertyName = "IE")]
     public bool IsEnabled { get; set; }

     //[JsonIgnore]
     //public bool IsEnabled { get; set; }

     //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
     //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
     //public bool CanEdit {get;set;}

     //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
     //public DateTime CreatedDate {get;set;}

     //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
     //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
     //public int CreatedUserID {get;set;}
}