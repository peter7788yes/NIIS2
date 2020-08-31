using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class SystemYCardVM
{
    [JsonProperty(PropertyName = "YMID")]
    public int YCardMID { get; set; }

    [JsonProperty(PropertyName = "AE")]
    public string AgeEngilsh { get; set; }

    [JsonProperty(PropertyName = "DID")]
    public string DoseID { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int Period { get; set; }

}