using System;
using System.Collections.Generic;
using System.Web;

namespace WayneTools
{
    public class IpAddressS
    {
        public static string GetIP(ClientIpType clientIpType)
        {
            string IP = "";
            try
            {
                switch (clientIpType)
                {
                    case ClientIpType.SearchProxy: 
                        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == "")
                            IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        else 
                            IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        break;
                    case ClientIpType.LastPage:
                        IP = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                        break;
                    default: 
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
                IP = "";
            }
            return IP;
        }
    }

    public enum ClientIpType
    {
        SearchProxy = 0, LastPage, REMOTE_ADDR
    }
}
