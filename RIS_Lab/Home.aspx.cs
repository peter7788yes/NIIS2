using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneTools;

public partial class Home : System.Web.UI.Page
{
    public string UrlParameter { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        AllowHttpMethod("GET");
        //UserVM user = AuthServer.GetLoginUser();
        ////使用者姓名(使用者帳號, 使用者單位)於YYY.MM.DD HH:MM:SS由##使用者IP##登入
        //txtLongin = string.Format("{0}({1}, {2})於{3}由##{4}##登入"
        //                           , user.UserName
        //                           , user.LoginName
        //                           , user.RoleName
        //                           , user.LoginDate
        //                           , IpAddressS.GetIP());


            UrlParameter = Request.QueryString.ToString();
       
            //string encryptedJsonString = Request["o"] ?? "";
            //string decryptedJsonString = QueryStringEncryptToolS.Decrypt(encryptedJsonString);
            //HomeUrlVM VM = JsonConvert.DeserializeObject<HomeUrlVM>(decryptedJsonString);
            //string HomeUrlValidPeriodMinute = WebConfigurationManager.AppSettings["HomeUrlValidPeriodMinute"].ToString();
            //int minute = 0;
            //int.TryParse(HomeUrlValidPeriodMinute, out minute);

           
            //if (DateTime.Compare(VM.date.Add(TimeSpan.FromMinutes(minute)), DateTime.Now) <= 0)
            //{
            //    if(PageUrl.Equals("")==false)
            //        PageUrl = VM.PageUrl;
            //}


           
    }

    //protected void btnLogout_Click(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    Session.Clear();
    //    Response.Redirect("~/Login.aspx");
    //}

    private void AllowHttpMethod(params string[] methods)
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
            Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }
}