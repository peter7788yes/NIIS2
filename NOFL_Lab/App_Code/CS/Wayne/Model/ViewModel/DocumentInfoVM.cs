using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class DocumentInfoVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "P")]
    public DateTime PublishStartDate { get; set; }

    [JsonIgnore]
    public int publishState { get; set; }

    [JsonProperty(PropertyName = "p")]
    public string publishStateSting 
    { 
        get
        {
            return publishState == 1 ? "上架" : "下架";
        }
        
    }

    [JsonProperty(PropertyName = "D")]
    public string DocTitle { get; set; }


    [JsonIgnore]
    //[JsonProperty(PropertyName = "F")]
    public int FileInfoID { get; set; }

    [JsonIgnore]
    //[JsonProperty(PropertyName = "DF")]
    public string DisplayFileName { get; set; }

    [JsonIgnore]
    //[JsonProperty(PropertyName = "DD")]
    public string DocDescription { get; set; }

    [JsonIgnore]
    //[JsonProperty(PropertyName = "F")]
    public List<DocumentFileInfoVM> FileList { get; set; }

    public DocumentInfoVM()
    {
        FileList = new List<DocumentFileInfoVM>();
    }
}

