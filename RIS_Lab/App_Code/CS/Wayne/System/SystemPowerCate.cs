using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;

public class SystemPowerCate
{
    public static List<SystemPowerCateVM> list = new List<SystemPowerCateVM>();

    public static void Update()
    {

        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetSystemPowerCate", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemPowerCateVM> list = new List<SystemPowerCateVM>();
        EntityS.FillModel(list, dt);
        SystemPowerCate.list = list;
    }


    public static SystemPowerCateVM GetVM(int value)
    {
        return SystemPowerCate.list.Find(item => item.ID == value);
    }


    public static SystemPowerCateVM GetVM(string Name)
    {
        return SystemPowerCate.list.Find(item => item.SystemPowerCateName.Equals(Name));
    }


    public static string GetName(int value)
    {
        string rtn = "";
        var VM = GetVM(value);
        if (VM != null)
        {
            rtn = VM.SystemPowerCateName;
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