using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using WayneTools;

public partial class Login : Page
{
    int SystemPowerCateID = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        AllowHttpMethod("GET", "POST");

        NameValueCollection NVC = Request.QueryString;
        if(NVC!=null  && NVC.Count>0)
        {
            //throw new HttpException(404, "Not found");
            Response.Redirect("/");
        }

        if (WebConfigurationManager.AppSettings["SystemPowerCateID"] != null)
        {
            SystemPowerCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["SystemPowerCateID"]);
        }

        if (this.IsPostBack == true)
        {
            UserVM user = SessionS.GetSessionValue("LoginUser") as UserVM;
            if (user != null)
            {
                int Chk = 0;
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xUpdateLogoutDate", sc))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.ID);
                        cmd.Parameters.AddWithValue("@LoginDate", user.LoginDate);
                        cmd.Parameters.AddWithValue("@SystemPowerCateID", SystemPowerCateID);
                        SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                        sp.Direction = ParameterDirection.Output;

                        sc.Open();
                        cmd.ExecuteNonQuery();

                        Chk = (int)sp.Value;
                    }
                }
                Session.Clear();
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string script = "";
        string txtUser = tbUser.Text.Trim();
        string txtPassword = tbPassword.Text.Trim();
        string txtCode = tbCode.Text.ToLower().Trim();
        int UserCount = 0;
        DataTable dt = new DataTable();

        if (SessionS.GetSessionValue("CheckCode") == null || SessionS.GetSessionValue("CheckCode").ToString().Equals(txtCode) == false)
        {
            tbCode.Text = "";
            script = "<script>alert('驗證碼錯誤');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        EncryptT enc = new EncryptT();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xCheckLogin", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", txtUser);
                cmd.Parameters.AddWithValue("@LoginPassword", enc.ToSHA256(txtPassword));
                cmd.Parameters.AddWithValue("@FunctionIndex", Convert.ToInt32(WebConfigurationManager.AppSettings["SystemFunctionIndex"]));
                SqlParameter sp = cmd.Parameters.AddWithValue("@UserCount", UserCount);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                UserCount = (int)sp.Value;
            }
        }

        if (UserCount > 0)
        {
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xGetUserByLoginName", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoginName", txtUser);
                
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            UserVM user = new UserVM();
            EntityS.FillModel<UserVM>(user, dt);
            //user.LoginDate = DateTime.Now;

            var org = SystemOrg.GetVM(user.OrgID);
            var clientIP = IpAddressS.GetIP();
            IpT ipt = new IpT(clientIP);
            NameValueCollection rRequest = Request.ServerVariables;
            bool yesOrNo = false;

            if (clientIP != null && org != null)
            {
                if (clientIP.Equals("::1") || clientIP.Equals("127.0.0.1") || string.IsNullOrEmpty(org.IpStart) || string.IsNullOrEmpty(org.IpEnd) || org.IpStart.Equals("0.0.0.0") && org.IpEnd.Equals("0.0.0.0"))
                {
                    yesOrNo = true;
                }
                else
                {
                    yesOrNo = ipt.CheckInNowWifi(string.Format("{0}-{1}", org.IpStart, org.IpEnd));
                }
            }
            else if(user.OrgID == 1)
            {
                yesOrNo = true;
            }
            else if (org == null)
            {
                script = "<script>alert('無權限登入');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            if (yesOrNo == false)
            {
                script = "<script>alert('非允許IP位置');</script>";
            }
            else
            {
                int Chk = 0;
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xUpdateLoginDate", sc))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.ID);
                        cmd.Parameters.AddWithValue("@LoginIP", IpAddressS.GetIP());
                        cmd.Parameters.AddWithValue("@SystemPowerCateID", SystemPowerCateID);

                        SqlParameter sp1 = cmd.Parameters.AddWithValue("@LoginDateOut", user.LoginDate);
                        sp1.Direction = ParameterDirection.Output;
                        SqlParameter sp2 = cmd.Parameters.AddWithValue("@Chk", Chk);
                        sp2.Direction = ParameterDirection.Output;

                        sc.Open();
                        cmd.ExecuteNonQuery();

                        user.LoginDate = (DateTime)sp1.Value;
                        Chk = (int)sp2.Value;
                    }
                }

                if (Chk > 0)
                {
                    //Session.Abandon();
                    //Session.Clear();

                    HttpContext.Current.Session["LoginUser"] = user;

                    string tempUrl = SessionS.GetSessionValue("tempUrl") as string;
                    if (tempUrl != null)
                    {
                        SessionS.RemoveSession("tempUrl");
                        Response.Redirect(tempUrl);
                    }
                    else
                    {
                        Response.Redirect("~/Home.aspx");
                    }
                }
                else
                {
                    script = "<script>alert('帳號密碼錯誤');</script>";
                }
            }
        }
        else
        {
            script = "<script>alert('帳號密碼錯誤');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        //Exception ex = Server.GetLastError();
        //if (ex is HttpRequestValidationException)
        //{
        //    Server.ClearError(); // 如果不ClearError()這個異常會繼續傳到Application_Error()。
        //    Response.Write("請您輸入合法字串。");
        //    Response.End();
        //}
    }

    public void RemoveCssClass(WebControl controlInstance, String css)
    {
        controlInstance.CssClass = String.Join(" ", controlInstance.CssClass.Split(' ').Where(x => x != css).ToArray());
    }

    protected void AllowHttpMethod(params string[] methods)
    {
        bool HasPower = false;

        string myMethod = Request.HttpMethod;

        List<string> list = new List<string>(methods);

        for (int i = 0; i <= methods.Length - 1; i++)
        {
            if (methods[i].Trim().ToUpper().Equals(myMethod))
            {
                HasPower = true;
                break;
            }
        }

        if (HasPower == false)
        {
            throw new HttpException(404, "Not found");
            //Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
        {
            base.Render(htmlwriter);
            string html = htmlwriter.InnerWriter.ToString();

            // Trim the whitespace from the 'html' variable
            html = RemoveWhitespaceFromHtml(html);
            writer.Write(html);
        }
    }

    private static readonly Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
    private static readonly Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);
    //This method uses the two regular expressions to remove any whitespace from a string of HTML.

    public static string RemoveWhitespaceFromHtml(string html)
    {
        html = RegexBetweenTags.Replace(html, ">");
        html = RegexLineBreaks.Replace(html, "<");

        return html.Trim();
    }
}
