using System;
using System.Collections.Generic;
using System.Web;

namespace WayneTools
{
    /// <summary>
    /// GetClientIP 的摘要描述
    /// </summary>
    public class IpAddressS
    {
        public static string GetIP(ClientIpType clientIpType)
        {
            string IP = "";
            try
            {
                switch (clientIpType)
                {
                    case ClientIpType.SearchProxy: // 判斷是否有使用 Proxy
                        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == "")
                            IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        else // proxy
                            IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        break;
                    case ClientIpType.LastPage:  // 上一頁的網址（從哪來）
                        IP = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                        break;
                    default: // 直接回傳抓到的 client IP
                        IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        break;
                }
            }
            catch
            {
            }
            return IP;
        }

        public static string GetIP()
        {

            string IP = "";
            try
            {
                IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(IP))
                {
                    IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch
            {
            }

            return IP;

        }
    }

    public enum ClientIpType
    {
        SearchProxy = 0, LastPage, REMOTE_ADDR
    }
}
