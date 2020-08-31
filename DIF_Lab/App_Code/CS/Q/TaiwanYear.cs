using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
public class TaiwanYear
{
    public TaiwanYear()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public static DateTime ToDateTime(string TaiwanYearDate)
    {
        DateTime ReturnVal = new DateTime() ;
        if (TaiwanYearDate.Length == 6)  TaiwanYearDate = "0" + TaiwanYearDate;
        if (TaiwanYearDate.Length == 7)
        {
            try
            {
                DateTime.TryParse((Convert.ToInt32(TaiwanYearDate.Substring(0, 3)) + 1911).ToString() + "/" + TaiwanYearDate.Substring(3, 2) + "/" + TaiwanYearDate.Substring(5, 2), out ReturnVal);
            }
            catch
            {
            }
        }
        return ReturnVal;

    }
    public static string ToDateString(string TaiwanYearDate)
    {
        string ReturnVal = "";
        if (TaiwanYearDate.Length == 6) TaiwanYearDate = "0" + TaiwanYearDate;
        if (TaiwanYearDate.Length == 7)
        {
        try
        {
           ReturnVal= Convert.ToDateTime((Convert.ToInt32(TaiwanYearDate.Substring(0, 3)) + 1911).ToString() + "/" + TaiwanYearDate.Substring(3, 2) + "/" + TaiwanYearDate.Substring(5, 2)).ToString("yyyy/MM/dd");
        }
        catch
        {
        }
        }
        return ReturnVal;

    }
}