using Newtonsoft.Json;
using System;

public class OutSecretVM
{
    [JsonProperty(PropertyName = "M")]
    public string Message { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int Checksum { get; set; }
}
