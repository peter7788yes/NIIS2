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
/// SystemCode 的摘要描述
/// </summary>
public class SystemOrg
{
    public static List<SystemOrgVM> list = new List<SystemOrgVM>();
    public static string JsonList="[]";
    public static void Update()
    {
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetAllOrg", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemOrgVM> list = new List<SystemOrgVM>();
        EntityS.FillModel(list, dt);
        SystemOrg.list = list;
        if(list.Count>0)
            SystemOrg.JsonList = JsonConvert.SerializeObject(list.Where(item=>item.OrgCateID== Convert.ToInt32(WebConfigurationManager.AppSettings["OrgCateID"])));
    }


    public static SystemOrgVM GetVM(int value)
    {
        return SystemOrg.list.Find(item => item.ID == value);
    }


    public static SystemOrgVM GetVM(string Name)
    {
        return SystemOrg.list.Find(item => item.OrgName.Equals(Name));
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