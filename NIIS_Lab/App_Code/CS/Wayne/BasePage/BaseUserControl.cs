using System;
using System.Web;
using System.Collections.Generic;

public class BaseUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
