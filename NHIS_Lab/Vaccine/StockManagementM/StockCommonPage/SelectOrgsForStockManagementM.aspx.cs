using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccine_StockManagementM_StockCommonPage_SelectOrgsForStockManagementM : System.Web.UI.Page
{
    public string MyTreeData = "";
    public int MyLevel = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        MyLevel = SystemOrg.GetVM(user.OrgID).OrgLevel;

        int PID = SystemOrg.GetVM(user.OrgID).PID;
        int OrgLevel = SystemOrg.GetVM(user.OrgID).OrgLevel;

        //DataTable dt = new DataTable();

        //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        //{
        //    using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetOrgForStockManagementM", sc))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
        //        cmd.Parameters.AddWithValue("@PID", PID);
        //        cmd.Parameters.AddWithValue("@OrgCateID", OrgCateID);
        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            sc.Open();
        //            da.Fill(dt);
        //        }
        //    }
        //}

        List<SystemOrgVM> list = new List<SystemOrgVM>();
        //EntityS.FillModel(list, dt);
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
        //MyTreeData = SystemOrg.JsonList;

    }
}