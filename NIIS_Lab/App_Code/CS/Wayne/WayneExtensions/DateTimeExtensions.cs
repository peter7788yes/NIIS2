using System;
using System.Globalization;

    public static class DateTimeExtensions
    {
        public static string ToLongTaiwanDate(this DateTime dateTime,bool HasBlank=false)
        {
            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";

            TaiwanCalendar TC = new TaiwanCalendar();

            if (HasBlank)
            {
                return string.Format("民國 {0} 年 {1} 月 {2} 日"
                                    , TC.GetYear(dateTime)
                                    , dateTime.Month
                                    , dateTime.Day);
            }

            return string.Format("民國{0}年{1}月{2}日"
                                , TC.GetYear(dateTime)
                                , dateTime.Month
                                , dateTime.Day);
        }

        public static string ToShortTaiwanDate(this DateTime dateTime,string Split="")
        {

            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";
            TaiwanCalendar TC = new TaiwanCalendar();

            return string.Format("{0}" + Split + "{1}" + Split + "{2}"
                                 , TC.GetYear(dateTime)
                                 , dateTime.Month.ToString("00")
                                 , dateTime.Day.ToString("00"));
        }


        public static string ToShortTaiwanDateTime(this DateTime dateTime)
        {
            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";
            TaiwanCalendar TC = new TaiwanCalendar();

            return string.Format("{0}/{1}/{2} {3}"
                                 , TC.GetYear(dateTime)
                                 , dateTime.Month
                                 , dateTime.Day
                                 , dateTime.ToString("HH:mm:ss"));
        }
    }
