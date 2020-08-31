using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using WayneEntity;

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
}