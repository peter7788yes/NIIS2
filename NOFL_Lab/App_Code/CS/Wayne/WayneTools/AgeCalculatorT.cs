using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AgeCalculator 的摘要描述
/// </summary>
public class AgeCalculatorT
{
    public string GetAge(DateTime BirthDate)
    {
        string rtn = "";
        DateTime now = DateTime.Now;

        if (BirthDate == null || BirthDate == default(DateTime))
            return "";

        int age = now.Year - BirthDate.Year;
        if (age > 0 && now.Month <BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
            age--;

     
        int month = now.Month - BirthDate.Month;

        if (month > 0 && now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
            month--;

        rtn = string.Format("滿{0}歲{1}個月", age, month);

        return rtn;
    }

}