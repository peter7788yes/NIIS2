using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class DayTimeVM
{
    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "d1")]
    public bool Monday { get; set; }

    [JsonProperty(PropertyName = "d2")]
    public bool Tuesday { get; set; }

    [JsonProperty(PropertyName = "d3")]
    public bool Wednesday { get; set; }

    [JsonProperty(PropertyName = "d4")]
    public bool Thursday { get; set; }

    [JsonProperty(PropertyName = "d5")]
    public bool Friday { get; set; }

    [JsonProperty(PropertyName = "d6")]
    public bool Saturday { get; set; }

    [JsonProperty(PropertyName = "d7")]
    public bool Sunday { get; set; }

    [JsonProperty(PropertyName = "timeAry")]
    public List<Dictionary<string,string>> TimeAry { get; set; }

    public List<Dictionary<string, TimeSpan>> OutTimeAry {
        get
        {
            List<Dictionary<string, TimeSpan>> list = new List<Dictionary<string, TimeSpan>>();
            if (TimeAry != null)
            {
                foreach (var item in TimeAry)
                {
                    Dictionary<string, TimeSpan> dict = new Dictionary<string, TimeSpan>();
                    if (item.ContainsKey("ss"))
                    {
                        var ary = item["ss"].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (ary.Length > 1)
                        {
                            int hour = 0;
                            int minute = 0;
                            bool success = int.TryParse(ary[0], out hour);
                            if (success == false)
                                continue;
                            success = int.TryParse(ary[1], out minute);
                            if (success == false)
                                continue;
                            TimeSpan ts = new TimeSpan(hour, minute, 0);
                            dict["ss"] = ts;
                        }
                    }

                    if (item.ContainsKey("ee"))
                    {
                        var ary = item["ee"].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (ary.Length > 1)
                        {
                            int hour = 0;
                            int minute = 0;
                            bool success = int.TryParse(ary[0], out hour);
                            if (success == false)
                                continue;
                            success = int.TryParse(ary[1], out minute);
                            if (success == false)
                                continue;
                            TimeSpan ts = new TimeSpan(hour, minute, 0);
                            dict["ee"] = ts;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    list.Add(dict);
                }

            }
            return list;
        }
    }
}