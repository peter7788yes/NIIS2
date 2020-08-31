<%@ WebHandler Language="C#" Class="CheckLoginOP" %>
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.SessionState;

/// <summary>
/// 產生驗證碼
/// </summary>
public class CheckLoginOP : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        int Rtn = 0;
        if (HttpContext.Current.Session["LoginUser"] != null)
            Rtn = 1;
        HttpContext.Current.Response.Write(Rtn);
        HttpContext.Current.Response.End();
       
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
