using System.Web;
using WayneTools;

public class AuthServer
{
    public static void CheckLogin()
    {
        if (SessionS.GetSessionValue("LoginUser") == null)
        {
            HttpContext.Current.Response.Redirect("~/Login.aspx");
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