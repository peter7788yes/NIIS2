using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WayneEntity;

public partial class UC_SelectSingleOrg : BasePage
{
    public string MyTreeData = "[]";
    public string PageUrl = "";
    public string EncryptPageUrl = "";
    public bool HasSearchPower = false;
    public bool HasViewPower = false;

    public UC_SelectSingleOrg()
    {
        base.BreakCheckPower = true;
    }
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");

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

            var user = AuthServer.GetLoginUser();
            DataTable dt = new DataTable();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetOrgByOrgID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<SystemOrgVM> list = new List<SystemOrgVM>();
            EntityS.FillModel(list, dt);

            //var all = SystemOrg.list;
            //int minValue = 0;
            //int maxValue = 0;
            //if (all.Count > 0)
            //{
            //    minValue = all[0].PID;
            //    maxValue = all[all.Count-1].PID;
            //}
            //IEnumerable<SystemOrgVM> conditionList = new List<SystemOrgVM>() { SystemOrg.GetVM(AuthServer.GetLoginUser().OrgID) };
            //IEnumerable<SystemOrgVM> queryList = new List<SystemOrgVM>();
            //List<SystemOrgVM> FinalList = new List<SystemOrgVM>() { SystemOrg.GetVM(AuthServer.GetLoginUser().OrgID) };
            //int maxLevel = 10;
            //int i = 0;
            //do
            //{
            //    queryList = GetChild(all, conditionList, minValue, maxValue);
            //    FinalList.AddRange(queryList);
            //    conditionList = queryList;
            //    i++;
            //} while (queryList.Count() > 0 && i < maxLevel);

            list = list.Where(item => item.OrgLevel < 5).ToList();
            MyTreeData = JsonConvert.SerializeObject(list);
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