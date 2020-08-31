using System.Web;
using WayneTools;

public class AuthServer
{

    /// <summary>
    /// 驗證是否登錄，如會話過期會跳轉到登錄頁面
    /// </summary>
    public static void CheckLogin()
    {
        if (SessionS.GetSessionValue("LoginUser") == null)
        {
            
            HttpContext.Current.Response.Redirect("~/Login.aspx");

            //不要用Server導向，因為怕對方猜到未導向的網址
            //HttpContext.Current.Server.Transfer("~/Login.aspx");
        }
    }

    public static UserVM GetLoginUser()
    {
        UserVM user = SessionS.GetSessionValue("LoginUser") as UserVM;
        if (user == null)
            HttpContext.Current.Response.Redirect("~/Login.aspx");
        return user;
    }

    public static void SetLoginUser(UserVM user)
    {
        SessionS.AddSession("LoginUser",user); 
    }
}