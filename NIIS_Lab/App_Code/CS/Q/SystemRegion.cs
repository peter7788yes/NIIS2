using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WayneEntity;

/// <summary>
/// SystemAreaCode 的摘要描述
/// </summary>
public class SystemRegion
{
    public static  List<SystemRegionVM>  RegionList;

    public static void Update()
    {
        List<SystemRegionVM> list = new List<SystemRegionVM>();
        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand("dbo.usp_Region_xGetRegionList");
        cmd.CommandType = CommandType.StoredProcedure;  
        dt= DB.GetDataTable(cmd, "ConnDB"); 
        EntityS.FillModel(list, dt); 
        SystemRegion.RegionList = list ;
        dt.Dispose();
    }

    

    public static string GetName(int RegionID)
    { 
        string RegionName = "";
        if (SystemRegion.RegionList.Find(item => item.RegionID.Equals(RegionID)) != null)
            {
                RegionName = SystemRegion.RegionList.Find(item => item.RegionID.Equals(RegionID)).RegionName;
        
            }
         
        return RegionName;
          
    }
      


}
public class SystemRegionVM
{
    /// <summary>
    /// 轄區名稱
    /// </summary> 
    public string RegionName { get; set; }
      
    /// <summary>
    /// 轄區代碼
    /// </summary> 
    public int RegionID { get; set; }


}