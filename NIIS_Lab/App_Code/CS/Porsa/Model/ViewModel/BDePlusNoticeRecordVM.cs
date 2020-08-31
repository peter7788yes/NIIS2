using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class BDePlusNoticeRecordVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "I")]
    public string IDNO { get; set; }
    
    [JsonProperty(PropertyName = "BPreMainID")]
    public int BPreMainID { get; set; }    
    
    [JsonProperty(PropertyName = "ED")]
    public string ExpDate { get; set; }

    [JsonProperty(PropertyName = "BD")]
    public string BirthDate { get; set; }

    [JsonProperty(PropertyName = "DD")]
    public string DrawDate { get; set; }

    [JsonProperty(PropertyName = "DN")]
    public string TelDayNo { get; set; }

    [JsonProperty(PropertyName = "NN")]
    public string TelNightNo { get; set; }

    [JsonProperty(PropertyName = "PID")]
    public string progress { get; set; }

    [JsonProperty(PropertyName = "MN")]
    public string MobileNo { get; set; }

    [JsonProperty(PropertyName = "P")]
    public string progressString {
        get
        {
            switch (progress) { 
                case "1":
                    return "已完成";
                case "2":
                    return "不需通知";
                default:
                    return "未完成";
            }
        }
    }

    [JsonProperty(PropertyName = "PIMG")]
    public string progressImg
    {
        get
        {
            switch (progress)
            {
                case "1":
                    return "/images/icon_browse.png";
                case "2":
                    return "/images/icon_maintain.png";
                default:
                    return "/images/icon_maintain.png";
            }
        }
    }

    [JsonProperty(PropertyName = "sAg")]
    public string HBsAg { get; set; }

    [JsonProperty(PropertyName = "eAg")]
    public string HBeAg { get; set; }

    [JsonProperty(PropertyName = "PA")]
    public string preAddr { get; set; }

    [JsonProperty(PropertyName = "PN")]
    public string preName { get; set; }

}