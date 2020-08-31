using System;
using System.Collections.Generic;

public static class StringExtensions
{
            public static string RepublicToAD(this string RepublicDate)
            {
                try
                {
                    if (RepublicDate != null && RepublicDate.Length > 0)
                    {
                        List<string> list = new List<string>();
                        RepublicDate = RepublicDate.Trim();
                        list.Add(RepublicDate.Substring(RepublicDate.Length - 2, 2));
                        list.Add(RepublicDate.Substring(RepublicDate.Length - 4, 2));
                        list.Add(RepublicDate.Substring(0, RepublicDate.Length - 4));
                        list.Reverse();

                        RepublicDate = string.Format("{0}{1}{2}",(Convert.ToInt32(list[0]) + 1911).ToString("00"), Convert.ToInt32(list[1]).ToString("00"), Convert.ToInt32(list[2]).ToString("00"));
                    }
                }
                catch
                {
                }

                return RepublicDate;
            }
}
