using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class UC_SelectOrgs : BasePage
{
    public string MyTreeData = "[]";
    public int MyLevel = 0;
    public bool OrgArea = true;
    public string PageUrl = "";
    public string EncryptPageUrl = "";
    public bool HasSearchPower = false;
    public bool HasViewPower = false;

    public UC_SelectOrgs()
    {
        base.BreakCheckPower = true;
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            PageUrl = QueryStringEncryptToolS.Decrypt(GetString("p"));
            EncryptPageUrl = QueryStringEncryptToolS.Encrypt(PageUrl);

            HasViewPower = CheckPower(PageUrl, MyPowerEnum.瀏覽);
            if (HasViewPower == false)
            {
                throw new HttpException(404, "Not found");
            }
            HasSearchPower = CheckPower(PageUrl, MyPowerEnum.查詢);


            var all = SystemOrg.list;

            //if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
            //{
            //    all.RemoveAll(item => item.OrgLevel == 2);
            //    all = all.Select(item =>
            //    {
            //        if (item.OrgLevel == 3)
            //        {
            //            item.PID = 1;
            //        }
            //        return item;
            //    }).ToList<SystemOrgVM>();
            //}

            int minValue = 0;
            int maxValue = 0;
            if (all.Count > 0)
            {
                minValue = all[0].PID;
                maxValue = all[all.Count-1].PID;
            }
            IEnumerable<SystemOrgVM> conditionList = new List<SystemOrgVM>() { SystemOrg.GetVM(AuthServer.GetLoginUser().OrgID) };
            IEnumerable<SystemOrgVM> queryList = new List<SystemOrgVM>();
            List<SystemOrgVM> FinalList = new List<SystemOrgVM>() { SystemOrg.GetVM(AuthServer.GetLoginUser().OrgID) };
            do
            {
                queryList = GetChild(all,conditionList,minValue,maxValue);
                FinalList.AddRange(queryList);
                conditionList = queryList;
            } while (queryList.Count() > 0);

            MyTreeData = JsonConvert.SerializeObject(FinalList);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

    protected IEnumerable<SystemOrgVM> GetChild(IEnumerable<SystemOrgVM> srcList, IEnumerable<SystemOrgVM> destList,int minValue ,int maxValue)
    {
        
        foreach (var item in srcList)
        {
            if (item.PID < minValue)
            {
                continue;
            }
            else if (item.PID>maxValue)
            {
                yield break;
            }

            foreach (var item2 in destList)
            {

                if (item2.ID == item.PID)
                {
                    yield return item;
                }
            }
        }
    }
}