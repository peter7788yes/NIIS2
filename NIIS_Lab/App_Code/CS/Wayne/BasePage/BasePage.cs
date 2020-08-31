using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Collections.Generic;

public class BasePage : System.Web.UI.Page
{
    public bool BreakCheckPower { get; set; }   
    public string HeadScript { get; set; }
    public string BodyClass { get; set; }
    public Dictionary<MyPowerVM, MyPowerVM> dict { get; set; }
    public PowerLogicType powerLogicType { get; set; }
    public bool isSharedPage { get; set; }
    public string SharedPageCheckUrl { get; set; }

    public BasePage()
    {
        isSharedPage = false;
        SharedPageCheckUrl = "";
        dict = new Dictionary<MyPowerVM, MyPowerVM>();
        BodyClass = "bodybg";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected MyPowerVM AddPower(string pageUrl, MyPowerEnum myPowerEnum)
    {
        MyPowerVM VM = new MyPowerVM(pageUrl, myPowerEnum);
        if (dict.ContainsKey(VM) == false)
            dict.Add(VM,VM);

        return VM;
    }

    protected List<MyPowerVM> AddPower(string pageUrl, params MyPowerEnum[] myPowerEnum)
    {
        List<MyPowerVM> list = new List<MyPowerVM>();
        foreach (var item in myPowerEnum)
        {
            MyPowerVM VM = new MyPowerVM(pageUrl, item);
            if (dict.ContainsKey(VM) == false)
                dict.Add(VM, VM);
            list.Add(VM);
        }
        return list;
    }

    protected MyPowerVM AddPower(MyPowerVM VM)
    {
        if (dict.ContainsKey(VM) == false)
            dict.Add(VM, VM);

        return VM;
    }

    protected MyPowerVM GetPower(MyPowerVM VM)
    {
        if (dict.ContainsKey(VM) == false)
            return dict[VM];
        else
            return VM;
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
        }
    }

    protected void DisableTop(bool flag)
    {
        if (flag == true)
        {
            HeadScript += "<script>if(self==top){location.href = '/Login.aspx';}</script>";
        }
 
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnInitComplete(EventArgs e)
    {
        if (BreakCheckPower == true)
            return;

        bool HasPower = false;

        if (Request.UrlReferrer == null || Request.Url.Host.Equals(Request.UrlReferrer.Host) == false)
        {
            //throw new HttpException(404, "Not found");
            Response.Redirect("/Login.aspx");
            Response.End();
        }

        UserVM user = AuthServer.GetLoginUser();
        if(user!=null)
        {
            string path = Request.FilePath.ToLower();

            if (path.Equals("/home.aspx") == true || path.Equals("/leftmenu.aspx") == true || path.Equals("/topheader.aspx") == true)
            {
                HasPower = true;
            }
            else
            {
                    if (dict.Count > 0)
                    {
                        var dictHasPowerCount = 0;

                        List<string> allUrlList = new List<string>();
                        List<string> notAddUrlList = new List<string>();
                        List<string> PageUrlList = new List<string>();
                        List<string> FunctionIndexList = new List<string>();

                        foreach (var item in dict)
                        {
                            PageUrlList.Add(item.Key.PageUrl);
                            FunctionIndexList.Add(((int)item.Key.myPowerEnum).ToString());

                            if (allUrlList.Contains(item.Key.PageUrl) == false)
                            {
                                allUrlList.Add(item.Key.PageUrl);
                            }

                            if (item.Key.myPowerEnum == MyPowerEnum.瀏覽)
                            {
                                if (notAddUrlList.Contains(item.Key.PageUrl) == false)
                                {
                                    notAddUrlList.Add(item.Key.PageUrl);
                                }
                            }
                        }

                        var needAddUrlList = allUrlList.Except(notAddUrlList);

                        foreach (var item in needAddUrlList)
                        {
                            PageUrlList.Add(item);
                            FunctionIndexList.Add(((int)MyPowerEnum.瀏覽).ToString());
                        }

                        var dt = MSDB.GetDataTable("ConnUser", "dbo.usp_SystemM_xCheckPowerList"
                                          , new Dictionary<string, object>()
                                          {
                                                    { "@UserID", user.ID },
                                                    { "@PageUrls", string.Join(",",PageUrlList) },
                                                    { "@FunctionIndexs", string.Join(",",FunctionIndexList) },
                                                    { "@ModuleCateID", Convert.ToInt32(WebConfigurationManager.AppSettings["ModuleCateID"]) }
                                         });

                        int i = 0;
                        foreach (var item in dict)
                        {
                            bool itemPower = (bool)dt.Rows[i][0];
                            if (isSharedPage == false && item.Key.myPowerEnum == MyPowerEnum.瀏覽 && itemPower == false) 
                            {
                                HasPower = false;
                                break;
                            }
                            else if(isSharedPage == true)
                            {
                                if (SharedPageCheckUrl.Length > 0)
                                {
                                    HasPower = CheckPower(SharedPageCheckUrl, MyPowerEnum.瀏覽);
                                }
                                else
                                {
                                    HasPower = CheckPower(Request.Path, MyPowerEnum.瀏覽);
                                }

                                if (HasPower == false)
                                    break;
                            }
                            else
                            { 
                                item.Value.HasPower = itemPower;
                                HasPower = true;
                            }

                            if (itemPower == true)
                            {
                                dictHasPowerCount++;
                            }

                            i++;
                        }

                        if (HasPower == true)
                        {
                            if ((dictHasPowerCount == dict.Count) || (powerLogicType == PowerLogicType.OR && dictHasPowerCount >= 1))
                            {
                                HasPower = true;
                            }
                            else
                            {
                                HasPower = false;
                            }
                        }
                    }
                    else
                    {
                        HasPower = CheckPower(Request.Path, MyPowerEnum.瀏覽);
                    }
            }

            if (HasPower == false)
            {
               throw new HttpException(404, "Not found");
            }
        }
        else
        {
            throw new HttpException(404, "Not found");
        }

       
    }

    protected bool CheckPower(string pageUrl, MyPowerEnum myPowerEnum)
    {
        UserVM user = AuthServer.GetLoginUser();
        bool HasPower = false;
        if (user != null)
        {
            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@HasPower", HasPower } };

            MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_SystemM_xCheckPower"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@UserID", user.ID },
                                                    { "@PageUrl", pageUrl },
                                                    { "@FunctionIndex", myPowerEnum },
                                                    { "@ModuleCateID",Convert.ToInt32(WebConfigurationManager.AppSettings["ModuleCateID"]) }
                                            });

            HasPower = (bool)OutDict["@HasPower"];
        }

        return HasPower;
    }

    public delegate bool TryParser<T>(string input, out T result);

    public static bool TryParse<T>(string toConvert, out T result, TryParser<T> tryParser = null)
    {
        if (toConvert == null)
            throw new ArgumentNullException("toConvert");

        if (tryParser == null)
        {
            var method = typeof(T).GetMethod ("TryParse", new[] { typeof(string), typeof(T).MakeByRefType() });

            if (method == null)
                throw new InvalidOperationException("Type does not have a built in try-parser.");

            tryParser = (TryParser<T>)Delegate.CreateDelegate (typeof(TryParser<T>), method);
        }

        return tryParser(toConvert, out result);
    }

    protected string PureString(string text)
    {
        return HttpUtility.HtmlEncode(text.Trim());
    }

    protected T PureNumber<T>(string text)
    {
        string rtn = PureString(text);

        if (rtn == null)
            return default(T);

        T tmp = default(T);
        TryParse(rtn, out tmp);

        return tmp;
    }

    protected List<T> PureList<T>(string text)
    {
        List<T> list = new List<T>();

        string rtn = PureString(text);

        if (rtn == null)
            return list;

        foreach (var item in rtn.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            T tmp = default(T);
            TryParse(rtn, out tmp);
            list.Add(tmp);
        }

        return list;
    }

    protected string GetString(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    {
        string rtn = "";
        switch (myHttpMethod)
        {
            case MyHttpMethod.GET:
                rtn = HttpContext.Current.Request.QueryString[key];
                break;
            case MyHttpMethod.POST:
                rtn = HttpContext.Current.Request.Form[key];
                break;
        }

        if (rtn == null)
            return "";

        return HttpUtility.HtmlEncode(rtn.Trim());
    }

    protected T GetNumber<T>(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    {
        string rtn = GetString(key, myHttpMethod);

        if (rtn == null)
            return default(T);

        T tmp = default(T);
        TryParse(rtn, out tmp);

        return tmp;
    }

    protected List<T> GetList<T>(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    {
        List<T> list = new List<T>();

        string rtn = GetString(key, myHttpMethod);

        if (rtn == null)
            return list; 

        foreach(var item in rtn.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries))
        {
            T tmp = default(T);
            if (typeof(T).Equals(typeof(string)))
            {
                list.Add((T)Convert.ChangeType(item, typeof(T)));
            }
            else
            {
                if(TryParse(item, out tmp))
                    list.Add(tmp);
            }
        }

        return list;
    }
}
