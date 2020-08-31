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
public class SystemYCard
{
    public static Dictionary<int, List<SystemYCardVM>> dict = new Dictionary<int, List<SystemYCardVM>>();

    public static void Update()
    {
        List<SystemYCardVM> list = new List<SystemYCardVM>();
        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetSystemYCard", sc))
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


        SystemYCard.dict = list.OrderBy(item => item.Period).ThenBy(item=>item.DoseID)
                  .GroupBy(item => item.YCardMID)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
    }



    public static List<SystemYCardVM> GetDict(int key)
    {

        List<SystemYCardVM> list = new List<SystemYCardVM>();
        if(SystemYCard.dict.ContainsKey(key))
        {
            list = SystemYCard.dict[key];
        }

        return list;
    }

    //public static SystemCodeVM GetVM(int key ,int value)
    //{

    //    List<SystemYCardVM> list = new List<SystemYCardVM>();
    //    if(SystemYCard.dict.ContainsKey(key))
    //    {
    //        list = SystemYCard.dict[key];
    //    }

    //    return list.Find(item => item.YCardMID == value);
    //}


    //public static SystemCodeVM GetVM(string key, string Name)
    //{

    //    List<SystemCodeVM> list = new List<SystemCodeVM>();
    //    if (SystemCode.dict.ContainsKey(key))
    //    {
    //        list = SystemCode.dict[key];
    //    }

    //    return list.Find(item => item.EnumName.Equals(Name));
       
    //}


    //public static string GetName(string key, int value)
    //{
    //    string rtn = "";
    //    var VM = GetVM(key, value);
    //    if (VM != null)
    //    {
    //        rtn = VM.EnumName;
    //    }
    //    return rtn;
    //}


    //public static int GetValue(string key, string Name)
    //{

    //    int rtn = 0;
    //    var VM = GetVM(key, Name);
    //    if (VM != null)
    //    {
    //        rtn = VM.EnumValue;
    //    }
    //    return rtn; 
    //}
}