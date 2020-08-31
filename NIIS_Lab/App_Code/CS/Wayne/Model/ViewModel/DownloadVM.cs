using Newtonsoft.Json;
using System;

public class DownloadVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "d")]
    public DateTime date { get; set; }

    public DownloadVM(int ID)
    {
        this.ID = ID;
        date = DateTime.Now;
    }
}