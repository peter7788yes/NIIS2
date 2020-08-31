using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using WayneEntity;

public class SystemRole
{
    public static List<SystemRoleVM> list = new List<SystemRoleVM>();
    public static List<SystemRoleVM> AllList = new List<SystemRoleVM>();

    public static void Update()
    {
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetAllRole", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        AllList = new List<SystemRoleVM>();
        EntityS.FillModel(AllList, dt);
        SystemRole.list = AllList.Where(item => item.RoleCateID == Convert.ToInt32(WebConfigurationManager.AppSettings["RoleCateID"])).ToList();
        

    }


    public static SystemRoleVM GetVM(int value)
    {
        return SystemRole.AllList.Find(item => item.ID == value);
    }


    public static SystemRoleVM GetVM(string Name)
    {
        return SystemRole.AllList.Find(item => item.RoleName.Equals(Name));
    }


    public static string GetName(int value)
    {
        string rtn = "";
        var VM = GetVM(value);
        if (VM != null)
        {
            rtn = VM.RoleName;
        }
        return rtn;
    }


    public static int GetID(string key, string Name)
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