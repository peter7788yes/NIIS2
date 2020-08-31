using System;

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

        if (month == 0)
        {
            if (month > 0 && now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
                month--;
        }
        else
        {
            month = now.Month - BirthDate.Month + 12;
            if (now.Day < BirthDate.Day)
                month--;
        }

        if (12 + month > 12)
        {
            age++;
            month -= 12;
        }
        rtn = string.Format("滿{0}歲{1}個月", age >= 0 ? age : 0 , month >= 0 ? month : 12 + month);

        return rtn;
    }

}