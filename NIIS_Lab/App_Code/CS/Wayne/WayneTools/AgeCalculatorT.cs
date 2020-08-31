using System;

public class AgeCalculatorT
{
    public string GetYearMonthAge(DateTime BirthDate)
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
            if (now.Year != BirthDate.Year)
            {
                age++;
            }
            month -= 12;
        }
        rtn = string.Format("滿{0}歲{1}個月", age >= 0 ? age : 0 , month >= 0 ? month : 12 + month);

        return rtn;
    }

    public string GetAge(DateTime BirthDate)
    {
        string rtn = "";
        DateTime Today = DateTime.Today;

        int age = 0, month = 0;

        if (BirthDate != null && BirthDate <= Today.AddDays(-1))
        {

            DateTime BirthDateThisYear = Convert.ToDateTime(Today.Year.ToString() + "/" + BirthDate.Month.ToString() + "/" + BirthDate.Day.ToString());

            if (Today <= BirthDateThisYear)
            {
                age = Today.Year - BirthDate.Year - 1;

            }
            else
            {

                age = Today.Year - BirthDate.Year;
            }



            for (int y = 12; y > -12; y--)
            {
                if (BirthDateThisYear.AddMonths(y) < Today)
                {


                    if (y < 0) month = 12 + y;
                    else
                        month = y;

                    break;

                }


            }
        }


        rtn = string.Format("滿{0}歲{1}個月", age, month);

        return rtn;
    }

}