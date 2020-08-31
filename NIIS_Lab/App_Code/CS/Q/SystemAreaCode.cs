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
public class SystemAreaCode
{
    public static Dictionary<string, List<SystemAreaCodeVM>> dict;

    public static void Update()
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        DataTable dt = new DataTable();
         
        SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetSystemAreaCode");
        cmd.CommandType = CommandType.StoredProcedure; 

        dt= DB.GetDataTable(cmd, "ConnDB"); 
        EntityS.FillModel(list, dt); 
        SystemAreaCode.dict = list.OrderBy(item => item.OrderNumber)
                  .GroupBy(item => item.CodeKey)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
        dt.Dispose();
    }


    public static List<SystemAreaCodeVM> GetCountyList()
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("County"))
            list = SystemAreaCode.dict["County"];

        return list;
    }
    public static List<SystemAreaCodeVM> GetTownList(int CountyID)
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("Town"))
            list = SystemAreaCode.dict["Town"].Where(item => item.AreaParent == CountyID).ToList();

        return list;
    }
    public static List<SystemAreaCodeVM> GetVillageList(int TownID)
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("Village"))
            list = SystemAreaCode.dict["Village"].Where(item => item.AreaParent == TownID).ToList();

        return list;
    }


    public static string GetName(int value)
    { 
        string AreaName = "";
        foreach (string area in new string[] { "County", "Town", "Village" })
        {
            if (SystemAreaCode.dict[area].Find(item => item.ID.Equals(value)) != null)
            {
                AreaName = SystemAreaCode.dict[area].Find(item => item.ID.Equals(value)).AreaName;
                break; 
            }

        }
        return AreaName;
         
    }


    public static List<SystemAreaCodeVM> GetRegionCountyList(int OrgID)
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("County"))
            list = SystemAreaCode.dict["County"];

        return list;
    }
    public static List<SystemAreaCodeVM> GetRegionTownList(int OrgID,int CountyID)
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("Town"))
            list = SystemAreaCode.dict["Town"].Where(item => item.AreaParent == CountyID).ToList();

        return list;
    }
    public static List<SystemAreaCodeVM> GetRegionVillageList(int OrgID,int TownID)
    {
        List<SystemAreaCodeVM> list = new List<SystemAreaCodeVM>();
        if (SystemAreaCode.dict.ContainsKey("Village"))
            list = SystemAreaCode.dict["Village"].Where(item => item.AreaParent == TownID).ToList();

        return list;
    }



}