using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using WayneEntity;

public class SystemOrg
{
    public static List<SystemOrgVM> list = new List<SystemOrgVM>();
    public static List<SystemOrgVM> ZoneList = new List<SystemOrgVM>();
    public static List<SystemOrgVM> AllList = new List<SystemOrgVM>();

    public static string JsonList = "[]";

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

        AllList = new List<SystemOrgVM>();
        EntityS.FillModel(AllList, dt);
        SystemOrg.list = AllList.Where(item => item.OrgCateID == Convert.ToInt32(WebConfigurationManager.AppSettings["OrgCateID"])).ToList();

        if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
        {
            SystemOrg.list.RemoveAll(item => item.OrgLevel == 2);
            for (var i = 0; i < SystemOrg.list.Count; i++)
            {
                if (SystemOrg.list[i].OrgLevel == 3)
                {
                    SystemOrg.list[i].PID = 1;
                }
            }
        }

        ZoneList = list.Where(item => item.OrgLevel < 5).ToList();

        if (SystemOrg.list.Count > 0)
            SystemOrg.JsonList = JsonConvert.SerializeObject(ZoneList);
    }

    public static SystemOrgVM GetVM(int value)
    {
        return SystemOrg.AllList.Find(item => item.ID == value);
    }

    public static SystemOrgVM GetVM(string Name)
    {
        return SystemOrg.AllList.Find(item => item.OrgName.Equals(Name));
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