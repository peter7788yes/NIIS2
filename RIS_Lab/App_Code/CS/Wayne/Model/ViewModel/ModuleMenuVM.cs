using Newtonsoft.Json;
using System.Collections.Generic;

public class ModuleMenuVM
{
    //[JsonProperty(PropertyName = "i")]
    public int ID { get; set; }

    //[JsonProperty(PropertyName = "l")]
    public string ModuleName { get; set; }

    //[JsonProperty(PropertyName = "p")]
    public string PageUrl { get; set; }

    [JsonIgnore]
    public int OrderNum { get; set; }

    [JsonIgnore]
    public string ModuleDescript { get; set; }

    
    //[JsonProperty(PropertyName = "P")]
    public int PID { get; set; }

    [JsonIgnore]
    public bool IsShow { get; set; }

    [JsonIgnore]
    public List<ModuleMenuVM> Children { get; set; }

    //[JsonProperty(PropertyName = "L")]
    //public int Level { get; set; }

    public ModuleMenuVM()
    {    
        Children = new List<ModuleMenuVM>();
    }


   
}