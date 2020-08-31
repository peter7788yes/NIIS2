using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccine_StockManagementM_StockCommonPage_SelectOrgForStockManagementM : System.Web.UI.Page
{
    public string MyTreeData = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int PID = SystemOrg.GetVM(user.OrgID).PID;
        int OrgLevel = SystemOrg.GetVM(user.OrgID).OrgLevel;

        List<SystemOrgVM> list = new List<SystemOrgVM>();

        list.AddRange(SystemOrg.list);

        if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
        {
            if (OrgLevel == 1)
            {
                //移除:與登入者平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 2));
            }
            else if (OrgLevel == 3)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 2 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 2 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
            else if (OrgLevel == 4)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
            else if (OrgLevel == 5)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
        }
        else if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 1)
        {
            if (OrgLevel == 1)
            {
                //移除:與登入者平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
            }
            else if (OrgLevel == 2)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
            else if (OrgLevel == 3)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
            else if (OrgLevel == 4)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
            else if (OrgLevel == 5)
            {
                //移除:與登入者上一層、平層、下一層以外的單位
                list.RemoveAll(item => !(item.OrgLevel == OrgLevel - 1 || item.OrgLevel == OrgLevel || item.OrgLevel == OrgLevel + 1));
                //移除:與登入者上一層同，卻不是登入者的上層
                list.RemoveAll(item => item.OrgLevel == OrgLevel - 1 && item.ID != PID);
                //移除:與登入者同一層，且直屬單位不同
                list.RemoveAll(item => item.OrgLevel == OrgLevel && item.PID != PID);
                //移除:與登入者下一層，且不屬於登入者下層單位
                list.RemoveAll(item => item.OrgLevel == OrgLevel + 1 && item.PID != user.OrgID);
            }
        }

        MyTreeData = JsonConvert.SerializeObject(list.Where(item => item.OrgCateID == Convert.ToInt32(WebConfigurationManager.AppSettings["OrgCateID"])));

    }
}