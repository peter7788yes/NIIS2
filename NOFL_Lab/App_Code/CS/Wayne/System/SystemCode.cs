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
public class SystemCode
{
    public static Dictionary<string, List<SystemCodeVM>> dict = new Dictionary<string, List<SystemCodeVM>>();

    public static void Update()
    {
        List<SystemCodeVM> list = new List<SystemCodeVM>();
        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetEnabledSystemCode", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        //list.Add(new RoleVM() {ID=0,RoleName="全部" });
        EntityS.FillModel(list, dt);


        SystemCode.dict = list.OrderBy(item => item.OrderNumber)
                  .GroupBy(item => item.CodeKey)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
    }



    public static List<SystemCodeVM> GetDict(string key)
    {

        List<SystemCodeVM> list = new List<SystemCodeVM>();
        if(SystemCode.dict.ContainsKey(key))
        {
            list = SystemCode.dict[key];
        }

        return list;
    }

    public static SystemCodeVM GetVM(string key ,int value)
    {

        List<SystemCodeVM> list = new List<SystemCodeVM>();
        if(SystemCode.dict.ContainsKey(key))
        {
            list = SystemCode.dict[key];
        }

        return list.Find(item => item.EnumValue == value);
    }


    public static SystemCodeVM GetVM(string key, string Name)
    {

        List<SystemCodeVM> list = new List<SystemCodeVM>();
        if (SystemCode.dict.ContainsKey(key))
        {
            list = SystemCode.dict[key];
        }

        return list.Find(item => item.EnumName.Equals(Name));
       
    }


    public static string GetName(string key, int value)
    {
        string rtn = "";
        var VM = GetVM(key, value);
        if (VM != null)
        {
            rtn = VM.EnumName;
        }
        return rtn;
    }


    public static int GetValue(string key, string Name)
    {

        int rtn = 0;
        var VM = GetVM(key, Name);
        if (VM != null)
        {
            rtn = VM.EnumValue;
        }

        return rtn; 

    }
}