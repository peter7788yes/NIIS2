using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WayneEntity;

/// <summary>
/// SystemOrgByOrgID 的摘要描述
/// </summary>
public class SystemOrgByOrgID
{
	public static List<SystemOrgVM> list = new List<SystemOrgVM>();
    public static string JsonList="[]";
    public static void Update(int OrgID)
    {

        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetOrgByOrgID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemOrgVM> list = new List<SystemOrgVM>();
        EntityS.FillModel(list, dt);

        if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
        {
            list.RemoveAll(item => item.OrgLevel == 2);
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].OrgLevel == 3)
                {
                    list[i].PID = 1;
                }
            }
        }

        SystemOrgByOrgID.list = list;
        if (list.Count > 0)
            SystemOrgByOrgID.JsonList = JsonConvert.SerializeObject(list.Where(item => item.OrgCateID == Convert.ToInt32(WebConfigurationManager.AppSettings["OrgCateID"])));
    }

    public static SystemOrgVM GetVM(int value)
    {
        return SystemOrgByOrgID.list.Find(item => item.ID == value);
    }


    public static SystemOrgVM GetVM(string Name)
    {
        return SystemOrgByOrgID.list.Find(item => item.OrgName.Equals(Name));
    }


    public static string GetName(int value)
    {
        string rtn = "";
        var VM = GetVM(value);
        if (VM != null)
        {
            rtn = VM.OrgName;
        }
        return rtn;
    }


    public static int GetID(string Name)
    {

        int rtn = 0;
        var VM = GetVM(Name);
        if (VM != null)
        {
            rtn = VM.ID;
        }
        return rtn;
    }
}