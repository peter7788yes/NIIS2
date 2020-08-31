using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Configuration;
using WayneEntity;

public partial class LeftMenu : BasePage
{
    public string MyTreeData = "";
    public string PageUrl = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass ="leftblock";
        
        UserVM user = AuthServer.GetLoginUser();

        if (user == null)
        {
            return;
        }

        if (WebConfigurationManager.AppSettings["HasCacheMenu"] == null || Convert.ToBoolean(WebConfigurationManager.AppSettings["HasCacheMenu"]) == false)
        {
            MyTreeData = GetMenu(user);
        }
        else
        {
            if (user.MenuJson != null && user.MenuJson.Trim().Length > 0)
            {
                MyTreeData = user.MenuJson;
            }
            else
            {
                MyTreeData = GetMenu(user);
                user.MenuJson = MyTreeData;
                AuthServer.SetLoginUser(user);
            }
        }

        string encryptedJsonString = Request["o"] ?? "";
        string decryptedJsonString = QueryStringEncryptToolS.Decrypt(encryptedJsonString);
        HomeUrlVM VM = JsonConvert.DeserializeObject<HomeUrlVM>(decryptedJsonString);
        string HomeUrlValidPeriodMinute = WebConfigurationManager.AppSettings["HomeUrlValidPeriodMinute"];
        int minute = 0;
        int.TryParse(HomeUrlValidPeriodMinute, out minute);

        if (VM != null && VM.date != null && DateTime.Compare(DateTime.Now.Add(TimeSpan.FromMinutes(minute)),VM.date) >= 0)
        {
            if (VM.PageUrl.Length > 0)
                PageUrl = VM.PageUrl;
        }

        //Response.ContentType = "application/json; charset=utf-8";
        //Response.Write(Rtn);
        //Response.End();
    }

    private string GetMenu(UserVM user)
    {
        //ModuleMenuVM lastNode = null;
        List<ModuleMenuVM> list = new List<ModuleMenuVM>();
        //List<ModuleMenuVM> Rtn = new List<ModuleMenuVM>();

        int ModuleCateID = 1;

        if (WebConfigurationManager.AppSettings["ModuleCateID"] != null)
        {
            ModuleCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["ModuleCateID"]);
        }

        DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_SystemM_xGetModuleMenu"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@UserID", user.ID },
                                              { "@ModuleCateID", ModuleCateID }
                                        });

        EntityS.FillModel(list, dt);

        //foreach (ModuleMenuVM item in list)
        //{

        //    if (item.PID == 0)
        //    {
        //        Rtn.Add(item);
        //        lastNode = item;
        //    }
        //    else
        //    {
        //        if (lastNode != null)
        //        {

        //            if (lastNode.ID == item.PID)
        //            {
        //                lastNode.children.Add(item);
        //            }
        //            else
        //            {

        //                GenMenuRecursive(item, lastNode);
        //            }
        //        }
        //    }
        //}

        //return JsonConvert.SerializeObject(Rtn);
        return JsonConvert.SerializeObject(list);
    }

    private void GenMenuRecursive(ModuleMenuVM nowNode, ModuleMenuVM innerNode)
    {
        ModuleMenuVM myParent = innerNode.Children.Find(x => nowNode.PID == x.ID);
        if (myParent != null)
        {
            myParent.Children.Add(nowNode);
        }
        else
        {
            foreach (ModuleMenuVM item in innerNode.Children)
            {
                GenMenuRecursive(nowNode, item);
            }
        }
    }

    //protected override void OnInitComplete(EventArgs e)
    //{
    //    Master.BodyCssClass = "leftblock";
    //    Master.BanSingleUsed = 1;
    //}
}