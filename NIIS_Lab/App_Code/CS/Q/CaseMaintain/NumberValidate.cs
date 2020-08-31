using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

 
 
    /// <summary>
    /// 號碼驗證
    /// </summary>
public static class NumberValidate
    { 
        /// <summary>
        /// 居留證號驗證
        /// </summary>
        /// <param name="arg_ResNo"></param>
        /// <returns></returns>
        public static bool IsResNo(string arg_ResNo)
        {

            var d = false;
            if (arg_ResNo.Length == 10)
            {

                arg_ResNo = arg_ResNo.ToUpper();
                if (arg_ResNo[0] >= 0x41 && arg_ResNo[0] <= 0x5A)  //同身份證號
                {
                    if (arg_ResNo[1] >= 0x41 && arg_ResNo[1] <= 0x44)  //第二碼必是 ABCD
                    {
                        //首二個英文字母同身分證字號檢核轉換成數字，取第一個英文字母轉換後的全部數字及第二個英文字母轉換後的個位數字，將此三個數字再與證號後八碼數字，可串成11個數字。
                     
                        var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                        var b = new int[11];

                        b[0] = a[(arg_ResNo[0]) - 65] / 10;
                        b[1] = a[(arg_ResNo[0]) - 65] % 10;
                        b[2] = a[(arg_ResNo[1]) - 65] % 10;

                        var c = b[0];  //*1  
                        c += b[1] * 9;  //*9

                        for (var i = 2; i <= 9; i++)   //*8-1
                        {
                            b[i + 1] = arg_ResNo[i] - 48;
                            c += b[i] * (10 - i);
                        }
                        //1,9,8,7,6,5,4,3,2,1,1
                        if (((c % 10) + b[10]) % 10 == 0)
                        {
                            d = true;
                        }
                    }
                }
            }

            return d;


        }

        /// <summary>
        /// 身份證號驗證
        /// </summary>
        /// <param name="arg_Identify"></param>
        /// <returns></returns>
        public static bool IsIdNo(string arg_Identify)
        {
            var d = false;
            if (arg_Identify.Length == 10)
            {
                arg_Identify = arg_Identify.ToUpper();
                if (arg_Identify[0] >= 0x41 && arg_Identify[0] <= 0x5A)
                {
                    var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                    var b = new int[11];
                    b[1] = a[(arg_Identify[0]) - 65] % 10;
                    var c = b[0] = a[(arg_Identify[0]) - 65] / 10;
                    for (var i = 1; i <= 9; i++)
                    {
                        b[i + 1] = arg_Identify[i] - 48;
                        c += b[i] * (10 - i);
                    }
                    if (((c % 10) + b[10]) % 10 == 0)
                    {
                        d = true;
                    }
                }
            }
            return d;
        }
    
 
}