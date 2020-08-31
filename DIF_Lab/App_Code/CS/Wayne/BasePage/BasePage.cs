using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// BasePage 的摘要描述
/// </summary>
public class BasePage : System.Web.UI.Page
{
    
    public string HeadScript { get; set; }
    public string BodyClass { get; set; }
    //public List<MyPowerVM> CheckPowerList { get; set; }
    public Dictionary<MyPowerVM, MyPowerVM> dict { get; set; }
    //public Dictionary<MyPowerEnum,bool> HasPowerDict { get; set; }

    public PowerLogicType powerLogicType { get; set; }

    public BasePage()
    {
        // CheckPowerList = new List<MyPowerVM>();
        dict = new Dictionary<MyPowerVM, MyPowerVM>();
        BodyClass = "class='bodybg'";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    //public bool HasPower(MyPowerEnum myPowerEnum)
    //{
    //    bool rtn = false;
    //    if (HasPowerDict.ContainsKey(myPowerEnum))
    //        rtn = HasPowerDict[myPowerEnum];

    //    return rtn;
    //}

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
        //CheckPowerList.Add(myPowerVM);
        if (dict.ContainsKey(VM) == false)
            dict.Add(VM, VM);

        return VM;
    }


    protected MyPowerVM GetPower(MyPowerVM VM)
    {
        //CheckPowerList.Add(myPowerVM);
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
            //Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }

    protected void DisableTop(bool flag)
    {
        
        if (flag == true)
        {
            HeadScript += "<script>if(self==top){location.href = '/Login.aspx';}</script>";
        }
        //if (ScriptManager.GetCurrent(this.Page) == null)
        //{
        //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", script, true);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "DisableTop", script, true);
        //}
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnInitComplete(EventArgs e)
    {
        bool HasPower = false;

        if (Request.UrlReferrer==null || Request.Url.Host.Equals(Request.UrlReferrer.Host)== false)
        {
            throw new HttpException(404, "Not found");
        }

        var dictHasPowerCount = 0;

        //List<string> urls = new List<string>();
        Dictionary<string, bool> urls = new Dictionary<string, bool>();
        foreach (var item in dict)
        {
            if(urls.ContainsKey(item.Key.PageUrl)==false)
            {
                urls[item.Key.PageUrl]=false;
            }

            item.Value.HasPower = CheckPower(item.Key.PageUrl, item.Key.myPowerEnum);

            if (item.Value.HasPower == true)
            {
                dictHasPowerCount++;
            }

            if(item.Key.myPowerEnum == MyPowerEnum.瀏覽)
            {
                urls[item.Key.PageUrl] = true;
            }
        }

       

       

        if (Request.Path.ToLower().Equals("/home.aspx") == true || Request.Path.ToLower().Equals("/leftmenu.aspx") == true || Request.Path.ToLower().Equals("/topheader.aspx") == true)
        {
            HasPower = true;
        }
        else
        {
            if(dict.Count > 0)
            {
                
                if ( (dictHasPowerCount == dict.Count) || (powerLogicType == PowerLogicType.OR && dictHasPowerCount >= 1))
                {
                    HasPower = true;
                }

                foreach (var item in urls)
                {
                    if (item.Value == false)
                    {
                        HasPower = CheckPower(item.Key, MyPowerEnum.瀏覽);
                        if (powerLogicType == PowerLogicType.OR && HasPower == true)
                        {
                            break;
                        }
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
            UserVM user = AuthServer.GetLoginUser();
            if(user!=null)
            {
                throw new HttpException(404, "Not found");
                //Response.Redirect("~/Login.aspx");
            }
            else
            {
                throw new HttpException(404, "Not found");
                //Response.Redirect("~/html/ErrorPage/NoPower.html");
            }
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }

    protected bool CheckPower(string pageUrl, MyPowerEnum myPowerEnum)
    {
        UserVM user = AuthServer.GetLoginUser();
        bool HasPower = false;
        if (user != null)
        {
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xCheckPower", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", user.ID);
                    cmd.Parameters.AddWithValue("@PageUrl", pageUrl);
                    cmd.Parameters.AddWithValue("@FunctionIndex", myPowerEnum);
                    cmd.Parameters.AddWithValue("@ModuleCateID", WebConfigurationManager.AppSettings["ModuleCateID"]);
                    SqlParameter sp = cmd.Parameters.AddWithValue("@HasPower", HasPower);
                    sp.Direction = ParameterDirection.Output;

                    sc.Open();
                    cmd.ExecuteNonQuery();

                    HasPower = (bool)sp.Value;

                }
            }
        }
        //HasPowerDict[myPowerEnum] = HasPower;
        return HasPower;
    }

   


    public delegate bool TryParser<T>(string input, out T result);

    public static bool TryParse<T>(string toConvert, out T result, TryParser<T> tryParser = null)
    {
        if (toConvert == null)
            throw new ArgumentNullException("toConvert");

        // This whole block is only if you really need
        // it to work in a truly dynamic way. You can additionally consider 
        // memoizing the default try-parser on a per-type basis.
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
        return Server.HtmlEncode(text.Trim());
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

        return Server.HtmlEncode(rtn.Trim());
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

    //protected int GetInt(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    string rtn = GetParameter(key, myHttpMethod);

    //    if (rtn == null)
    //        return 0;

    //    int tmp = 0;
    //    int.TryParse(rtn, out tmp);
    //    return tmp;
    //}

    //protected float GetFloat(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    string rtn = GetParameter(key, myHttpMethod);

    //    if (rtn == null)
    //        return 0;

    //    float tmp = 0;
    //    float.TryParse(rtn, out tmp);
    //    return tmp;
    //}

    //protected double GetDouble(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    string rtn = GetParameter(key, myHttpMethod);

    //    if (rtn == null)
    //        return 0;

    //    double tmp = 0;
    //    double.TryParse(rtn, out tmp);
    //    return tmp;
    //}

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


    //protected List<string> GetStringList(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    return new List<string>();
    //}

    //protected List<int> GetIntList(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    return new List<int>();
    //}

    //protected List<float> GetFloatList(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    return new List<float>();
    //}

    //protected List<double> GetDoubleList(string key, MyHttpMethod myHttpMethod = MyHttpMethod.POST)
    //{
    //    return new List<double>();
    //}


    protected DataTable GetDataTable(string Conn,string ProcName, Dictionary<string,object> dict=null)
    {
        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(ProcName, sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if(dict!=null)
                {
                    foreach(var item in dict)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
               
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }


    //protected DataSet GetDataSet(string Conn, string ProcName, Dictionary<string, object> dict = null)
    //{
    //    DataSet ds = new DataSet();

    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(ProcName, sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            if (dict != null)
    //            {
    //                foreach (var item in dict)
    //                {
    //                    cmd.Parameters.AddWithValue(item.Key, item.Value);
    //                }
    //            }

    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                sc.Open();
    //                da.Fill(ds);
    //            }
    //        }
    //    }

    //    return ds;
    //}

    //protected int ExecuteNonQuery(string Conn, string ProcName, Dictionary<string, object> dict = null)
    //{
    //    int rtn = 0;
    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(ProcName, sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            if (dict != null)
    //            {
    //                foreach (var item in dict)
    //                {
    //                    cmd.Parameters.AddWithValue(item.Key, item.Value);
    //                }
    //            }

    //            sc.Open();
    //            rtn = cmd.ExecuteNonQuery();
               
    //        }
    //    }

    //    return rtn;
    //}

    //protected DataTable GetDataTable(string Conn, string ProcName,ref Dictionary<string, object> OutDict, Dictionary<string, object> dict = null)
    //{
    //    DataTable dt = new DataTable();

    //    Dictionary<string, object> tmpOutDict = new Dictionary<string, object>();
    //    Dictionary<string, SqlParameter> paramDict = new Dictionary<string, SqlParameter>();

    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(ProcName, sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
               

    //            if (dict != null)
    //            {

    //                foreach (var item in dict)
    //                {
    //                    cmd.Parameters.AddWithValue(item.Key, item.Value);
    //                }
    //            }

    //            if (OutDict != null)
    //            {

    //                foreach (var item in OutDict)
    //                {
    //                    SqlParameter sp = cmd.Parameters.AddWithValue(item.Key, OutDict[item.Key]);
    //                    sp.Direction = ParameterDirection.Output;
    //                    paramDict[item.Key] = sp;
    //                }
    //            }

    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                sc.Open();
    //                da.Fill(dt);
    //            }

    //            foreach (var item in OutDict)
    //            {
    //                //OutDict[item.Key] = Convert.ChangeType(paramDict[item.Key].Value, OutDict[item.Key].GetType()); 
    //                tmpOutDict[item.Key] = paramDict[item.Key].Value;
    //            }

    //            OutDict = tmpOutDict;

    //        }
    //    }

    //    return dt;
    //}

    //protected DataSet GetDataSet(string Conn, string ProcName, ref Dictionary<string, object> OutDict, Dictionary<string, object> dict = null)
    //{
    //    DataSet ds = new DataSet();

    //    Dictionary<string, object> tmpOutDict = new Dictionary<string, object>();
    //    Dictionary<string, SqlParameter> paramDict = new Dictionary<string, SqlParameter>();

    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(ProcName, sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            if (dict != null)
    //            {

    //                foreach (var item in dict)
    //                {
    //                    cmd.Parameters.AddWithValue(item.Key, item.Value);
    //                }
    //            }

    //            if (OutDict != null)
    //            {

    //                foreach (var item in OutDict)
    //                {
    //                    SqlParameter sp = cmd.Parameters.AddWithValue(item.Key, OutDict[item.Key]);
    //                    sp.Direction = ParameterDirection.Output;
    //                    paramDict[item.Key] = sp;
    //                }
    //            }

    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                sc.Open();
    //                da.Fill(ds);
    //            }

    //            foreach (var item in OutDict)
    //            {
    //                tmpOutDict[item.Key] = paramDict[item.Key].Value;
    //            }

    //            OutDict = tmpOutDict;

    //        }
    //    }

    //    return ds;
    //}

    //protected int ExecuteNonQuery(string Conn, string ProcName, ref Dictionary<string, object> OutDict, Dictionary<string, object> dict = null)
    //{
    //    int rtn = 0;

    //    Dictionary<string, object> tmpOutDict = new Dictionary<string, object>();
    //    Dictionary<string, SqlParameter> paramDict = new Dictionary<string, SqlParameter>();

    //    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings[Conn].ToString()))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(ProcName, sc))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
              
    //            if (dict != null)
    //            {
    //                foreach (var item in dict)
    //                {
    //                    cmd.Parameters.AddWithValue(item.Key, item.Value);
    //                }
    //            }

    //            if (OutDict != null)
    //            {

    //                foreach (var item in OutDict)
    //                {
    //                    SqlParameter sp = cmd.Parameters.AddWithValue(item.Key, OutDict[item.Key]);
    //                    sp.Direction = ParameterDirection.Output;
    //                    paramDict[item.Key] = sp;
    //                }
    //            }

    //            sc.Open();
    //            rtn = cmd.ExecuteNonQuery();

    //            foreach (var item in OutDict)
    //            {
    //                tmpOutDict[item.Key] = paramDict[item.Key].Value;
    //            }

    //            OutDict = tmpOutDict;
    //        }
    //    }

    //    return rtn;
    //}
}
