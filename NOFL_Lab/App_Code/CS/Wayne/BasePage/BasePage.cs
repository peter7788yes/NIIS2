using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

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
            Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }

    protected void DisableTop(bool flag)
    {
        
        if (flag == true)
        {
            HeadScript += "<script>if(self==top){location.href = '/login.aspx';}</script>";
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

        var dictHasPowerCount = 0;

        List<string> urls = new List<string>();
        foreach (var item in dict)
        {
            if(urls.Contains(item.Key.PageUrl)==false)
            {
                urls.Add(item.Key.PageUrl);
            }

            item.Value.HasPower = CheckPower(item.Key.PageUrl, item.Key.myPowerEnum);
            if (item.Value.HasPower == true)
            {
                dictHasPowerCount++;
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
                    HasPower = CheckPower(item, MyPowerEnum.瀏覽);
                    if(powerLogicType == PowerLogicType.OR && HasPower==true)
                    {
                        break;
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
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Response.Redirect("~/html/ErrorPage/NoPower.html");
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
    
}
