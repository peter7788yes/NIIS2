using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;

public class SystemElementarySchool
{
    public static List<SystemElementarySchoolVM> list = new List<SystemElementarySchoolVM>();

    public static void Update()
    {
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetEnabledElementarySchool", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemElementarySchoolVM> list = new List<SystemElementarySchoolVM>();
        EntityS.FillModel(list, dt);
        SystemElementarySchool.list = list;
    }


    public static SystemElementarySchoolVM GetVM(int value)
    {
        return SystemElementarySchool.list.Find(item => item.ElementarySchoolID == value);
    }


    public static SystemElementarySchoolVM GetVM(string Name)
    {
        return SystemElementarySchool.list.Find(item => item.SchoolName.Equals(Name));
    }


    public static string GetName(int value)
    {
        string rtn = "";
        var VM = GetVM(value);
        if (VM != null)
        {
            rtn = VM.SchoolName;
        }
        return rtn;
    }


    public static int GetID(string key, string Name)
    {

        int rtn = 0;
        var VM = GetVM(Name);
        if (VM != null)
        {
            rtn = VM.ElementarySchoolID;
        }
        return rtn;
    }
}