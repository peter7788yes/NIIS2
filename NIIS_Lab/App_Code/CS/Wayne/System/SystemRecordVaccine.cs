using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;

public class SystemRecordVaccine
{
    public static List<SystemRecordVaccineVM> list = new List<SystemRecordVaccineVM>();

    public static void Update()
    {
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetSystemRecordVaccine", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemRecordVaccineVM> list = new List<SystemRecordVaccineVM>();
        EntityS.FillModel(list, dt);
        SystemRecordVaccine.list = list;
    }


    public static SystemRecordVaccineVM GetVM(int value)
    {
        return SystemRecordVaccine.list.Find(item => item.ID == value);
    }


    public static SystemRecordVaccineVM GetVM(string Name)
    {
        return SystemRecordVaccine.list.Find(item => item.SystemRecordVaccineCode.Equals(Name));
    }


    public static string GetName(int value)
    {
        string rtn = "";
        var VM = GetVM(value);
        if (VM != null)
        {
            rtn = VM.SystemRecordVaccineCode;
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