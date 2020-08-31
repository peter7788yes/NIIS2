using System;
using System.Web;

namespace WayneTools
{
    public class LogS
    {
        public static void Debug(Exception ex)
        {
            string err = string.Format(@"\r\n #Request.Path:{0}
                                      \r\n #Request.RawUrl:{1}
                                      \r\n #ex.GetType().Name:{2}
                                      \r\n #ex.Source:{3}
                                      \r\n #ex.TargetSite:{4}
                                      \r\n #ex.Message:{5}
                                      \r\n #ex.StackTrace:{6}
                                      \r\n"
                                      , HttpContext.Current.Request.Path
                                      , HttpContext.Current.Request.RawUrl
                                      , ex.GetType().Name
                                      , ex.Source
                                      , ex.TargetSite
                                      , ex.Message
                                      , ex.StackTrace);

            throw new Exception(err);
        }

        public static void Debug(string message)
        {
            throw new Exception(message);
        }

        public static string GetErrorMessage(Exception ex)
        {
            string err = "";
            try
            {
                err = string.Format(@"ClientIP=>{0}_#Request.Path:{1}_#Request.RawUrl:{2}_#ex.GetType().Name:{3}_#ex.Source:{4}_#ex.TargetSite:{5}_#ex.Message:{6}_#ex.StackTrace:{7}"
                                    , WayneTools.IpAddressS.GetIP()
                                    , HttpContext.Current.Request.Path
                                    , HttpContext.Current.Request.RawUrl
                                    , ex.GetType().Name
                                    , ex.Source
                                    , ex.TargetSite
                                    , ex.Message
                                    , ex.StackTrace).Replace("_#", "<br/>" + Environment.NewLine + "#");
            }
            catch (Exception ex2)
            {
                err = string.Format(@"ex.GetType().Name:{0}_#ex.Source:{1}_#ex.TargetSite:{2}_#ex.Message:{3}_#ex.StackTrace:{4}"
                                    , ex2.GetType().Name
                                    , ex2.Source
                                    , ex2.TargetSite
                                    , ex2.Message
                                    , ex2.StackTrace).Replace("_#", Environment.NewLine + "#");
            }

            return err;
        }
    }

}