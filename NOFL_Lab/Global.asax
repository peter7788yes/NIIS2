<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="WayneBundles" %>
<%@ Import Namespace="WayneTools" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="WayneEntity" %>

<script runat="server">

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    void Application_Start(object sender, EventArgs e)
    {
        // 在應用程式啟動時執行的程式碼
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        JsCssBundleConfig.RegisterBundles(BundleTable.Bundles);

        //UnityContainer container = new UnityContainer();
        //Application["Container"] = container; // 把容器物件保存在共用變數裡

        // 註冊型別
        //container.RegisterType<IHelloService, HelloService>();

        SystemCode.Update();
        SystemRole.Update();
        SystemOrg.Update();
        SystemAreaCode.Update();
        SystemRecordVaccine.Update();
        SystemYCard.Update();
        SystemElementarySchool.Update();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在應用程式關閉時執行的程式碼

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在發生未處理的錯誤時執行的程式碼
        string err="";
        try
        {
            Exception ex = Server.GetLastError().InnerException;
            err = LogS.GetErrorMessage(ex);
            //清除錯誤，不會顯示錯誤頁面，再頁面層級比較好用
            //Server.ClearError();
        }
        catch
        {
        }

        logger.Debug(err);

        try
        {
            Response.Filter = null;
        }
        catch
        {
        }

    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新的工作階段啟動時執行的程式碼

    }

    void Session_End(object sender, EventArgs e)
    {
        // 在工作階段結束時執行的程式碼
        // 注意: 只有在  Web.config 檔案中將 sessionstate 模式設定為 InProc 時，
        // 才會引起 Session_End 事件。如果將 session 模式設定為 StateServer 
        // 或 SQLServer，則不會引起該事件。

    }

    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        //IIS 有啟用就不用了
        //#if DEBUG
        CompressS.CompressPage(sender as HttpApplication);
        //#endif
    }

    void Application_PostReleaseRequestState(object sender, EventArgs e)
    {
        //IIS 有啟用就不用了
        //CompressS.CompressCssAndJavascript(sender as HttpApplication);
    }

    protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        //HttpResponse response = HttpContext.Current.Response;
        //// 隱藏.Net版本
        //response.Headers.Remove("Server");
        //response.Headers.Remove("X-AspNet-Version");
        //response.Headers.Remove("X-Powered-By");
        //// 隱藏MVC版本(2)
        // (1)和(2)是相同的選一個即可
        //response.Headers.Remove("X-AspNetMvc-Version");
        // 移除IIS版本
        //response.Headers.Remove("Server");
        //response.Headers.Set("Server", "Apache/2.4.7 (Unix) OpenSSL/1.0.1e");
    }


    void Application_AcquireRequestState(object sender, EventArgs e)
    {
        HttpRequest request = HttpContext.Current.Request;
        string url = request.RawUrl.ToLower();
        string path = request.Path.ToLower();
        if (url.Contains("/home.aspx?"))
        {
            SessionS.AddSession("tempUrl", url);
        }

        if(path.IndexOf("/login.aspx") < 0 && path.EndsWith(".aspx"))
        {
            AuthServer.CheckLogin();
        }
    }
</script>
