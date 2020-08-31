using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class AccountDetailVM
{

    [JsonProperty(PropertyName = "LN")]
    public string LoginName { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string UserName { get; set; }

    [JsonIgnore]
    public bool IsEnabled { get; set; }

    [JsonIgnore]
    public bool IsLock { get; set; }

    [JsonProperty(PropertyName = "VC")]
    public string IsEnabledString
    {
        get
        {
            if(IsLock==false)
            {
                if(IsEnabled==true)
                {
                    return SystemCode.GetName("AccountM_EnableState", 1);
                }
                 else
                {
                    return SystemCode.GetName("AccountM_EnableState", 2);
                }
                
            }
            else
            {

                return SystemCode.GetName("AccountM_EnableState", 4);
            }
        }
    }

    [JsonProperty(PropertyName = "AD")]
    public DateTime ApplyDate { get; set; }

}