using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WayneEntity;
using WayneTools;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        string txtUser = tbUser.Text.Trim();
        string txtPassword = tbPassword.Text.Trim();
        string txtCode = tbCode.Text.ToLower().Trim();
        int UserCount = 0;
        DataTable dt = new DataTable();

        //if (SessionS.GetSessionValue("CheckCode") == null || SessionS.GetSessionValue("CheckCode").ToString().Equals(txtCode) == false)
        //{
        //    Panel1.Visible = true;
        //    lblError.Text = "驗證碼錯誤";
        //    return;
        //}

        EncryptT enc = new EncryptT();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xCheckLogin", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", txtUser);
                cmd.Parameters.AddWithValue("@LoginPassword", enc.ToSHA256(txtPassword));
                cmd.Parameters.AddWithValue("@FunctionIndex",1);
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
            user.LoginDate = DateTime.Now;
            HttpContext.Current.Session["LoginUser"] = user;

            string tempUrl=SessionS.GetSessionValue("tempUrl") as string;
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
            Panel1.Visible = true;
            lblError.Text = "帳號密碼錯誤";
        }

       
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
}
