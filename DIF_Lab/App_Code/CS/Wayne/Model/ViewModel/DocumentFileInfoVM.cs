using Newtonsoft.Json;
using System;
using System.Collections.Generic;


public class DocumentFileInfoVM
{
    [JsonIgnore]
    public int DocumentInfoID { get; set; }

    [JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }

    [JsonProperty(PropertyName = "F")]
    public int FileInfoID { get; set; }
}