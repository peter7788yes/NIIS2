using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;

/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {
        //如果使用設計的元件，請取消註解下列一行
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SendTask(string ClassName,string MD5,int ReportCateID,int ReportType,int ApplyType, string JsonData)
    {
        Type type = Type.GetType(ClassName+"Cal");
        var calculator = Activator.CreateInstance(type);
        MethodInfo method = type.GetMethod("RunTask");
        return method.Invoke(calculator, new object[] { MD5, ReportCateID, ApplyType, JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonData) }) as string;
    }

    //private void Invoke(string typeName, string methodName)
    //{
    //    Type type = Type.GetType(typeName);
    //    object instance = Activator.CreateInstance(type);
    //    MethodInfo method = type.GetMethod(methodName);
    //    method.Invoke(instance, null);
    //}

    //private void Invoke<T>(string methodName) where T : new()
    //{
    //    T instance = new T();
    //    MethodInfo method = typeof(T).GetMethod(methodName);
    //    method.Invoke(instance, null);
    //}

}
