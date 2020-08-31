using System;
using System.Web;
namespace WayneTools
{

    public class SessionS
    {
        public static void AddSession(string strSessionName, object sessionValue)
        {
            HttpContext.Current.Session[strSessionName] = sessionValue;
        }

        public static void RemoveSession(string strSessionName)
        {
            HttpContext.Current.Session.Contents.Remove(strSessionName);
        }

        public static object GetSessionValue(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] != null)
            {
                return HttpContext.Current.Session[strSessionName];
            }
            return null;
        }
    }
}