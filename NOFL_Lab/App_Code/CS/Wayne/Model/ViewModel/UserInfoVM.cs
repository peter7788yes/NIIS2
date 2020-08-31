using Newtonsoft.Json;
using System;

public class UserInfoVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "L")]
    public string LoginName { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "R")]
    public string RoleName { get; set; }

    [JsonProperty(PropertyName = "IE")]
    public bool IsEnabled { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int CheckState { get; set; }

    [JsonIgnore]
    public DateTime LastLoginDate { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string NoLoginTime
    {
        get
        {
            string rtn = "";

            if (LastLoginDate != default(DateTime))
            {
                TimeSpan ts = DateTime.Now.Subtract(LastLoginDate);
                int dd = ts.Days;
                int HH = ts.Hours;
                int mm = ts.Minutes;

                if (dd > 0)
                {
                    rtn = string.Format("{0}日{1}時{2}分", dd, HH, mm);
                }
                else if (HH > 0)
                {
                    rtn = string.Format("{0}時{1}分", HH, mm);
                }
                else
                {
                    rtn = string.Format("{0}分", mm);
                }
            }
            return rtn;

        }
    }
}