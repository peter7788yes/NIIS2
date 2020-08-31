using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WayneTools
{
    public class ValidT
    {
            public  bool CheckRocID(string id)
            {
                if (string.IsNullOrEmpty(id))
                    return false;  
                id = id.ToUpper();
                var regex = new Regex("^[A-Z]{1}[0-9]{9}$");
                if (!regex.IsMatch(id))
                    return false;   
 
                int[] seed = new int[10];    
                string[] charMapping = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
                string target = id.Substring(0, 1);
                for (int index = 0; index < charMapping.Length; index++)
                {
                    if (charMapping[index] == target)
                    {
                        index += 10;
                        seed[0] = index / 10;      
                        seed[1] = (index % 10) * 9; 
                        break;
                    }
                }
                for (int index = 2; index < 10; index++)
                {   
                    seed[index] = Convert.ToInt32(id.Substring(index - 1, 1)) * (10 - index);
                }
                return (10 - (seed.Sum() % 10)) % 10 == Convert.ToInt32(id.Substring(9, 1));
            }

            public bool CheckEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
    }
}