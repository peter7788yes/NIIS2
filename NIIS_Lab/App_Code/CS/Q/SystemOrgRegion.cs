using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WayneEntity;

/// <summary>
/// SystemOrgRegion 組織轄區相關
/// </summary>
public class SystemOrgRegion
{
    public static Dictionary<int, List<SystemOrgRegionVM>> dictOrgRegion;
    public static Dictionary<int, List<SystemOrgRegionCountyVM>> dictOrgCounty;
    public static Dictionary<int, List<SystemRegionSettingVM>> dictRegionSetting;
    public static void Update()
    {
        SystemAreaCode.Update();

        List<SystemOrgRegionVM> Regionlist = new List<SystemOrgRegionVM>();
        DataTable dt = DB.GetDataTable("SELECT ID,[OrgID],[RegionID]  FROM [dbo].[R_RegionAndOrg]", "ConnDB");
        EntityS.FillModel(Regionlist, dt);
        SystemOrgRegion.dictOrgRegion = Regionlist.OrderBy(item => item.OrgID)
                  .GroupBy(item => item.OrgID)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
        dt.Dispose();

        List<SystemOrgRegionCountyVM> Countylist = new List<SystemOrgRegionCountyVM>();
          dt = DB.GetDataTable(" SELECT [id] ,[OrgID] ,[CountyID] FROM [dbo].[R_RegionCountyAndOrg] ", "ConnDB");
          EntityS.FillModel(Countylist, dt);
          SystemOrgRegion.dictOrgCounty = Countylist.OrderBy(item => item.OrgID)
                  .GroupBy(item => item.OrgID)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
         
        dt.Dispose();


        List<SystemRegionSettingVM> Settinglist = new List<SystemRegionSettingVM>();
          dt = DB.GetDataTable(" SELECT [RegionID] , [CountyID] ,[TownID] ,[VillageID],SettingID FROM [dbo].[R_RegionSetting] ", "ConnDB");
          EntityS.FillModel(Settinglist, dt);
          SystemOrgRegion.dictRegionSetting = Settinglist.OrderBy(item => item.RegionID)
                  .GroupBy(item => item.RegionID)
                  .ToDictionary(IGrouping => IGrouping.Key, IGrouping => IGrouping.ToList());
         
        dt.Dispose();
         

    } 

    public static List<SystemOrgRegionVM> GetOrgRegion(int OrgID)
    {
        List<SystemOrgRegionVM> r = new List<SystemOrgRegionVM>();
        if (SystemOrgRegion.dictOrgRegion != null && SystemOrgRegion.dictOrgRegion.ContainsKey(OrgID))
            r = SystemOrgRegion.dictOrgRegion[OrgID];

        return r;
    }
    public static List<SystemOrgRegionCountyVM> GetOrgRegionCounty(int OrgID)
    {
        List<SystemOrgRegionCountyVM> rc = new List<SystemOrgRegionCountyVM>();
        if (SystemOrgRegion.dictOrgCounty != null && SystemOrgRegion.dictOrgCounty.ContainsKey(OrgID))
            rc = SystemOrgRegion.dictOrgCounty[OrgID];

        return rc;
    }

    public static List<SystemRegionSettingVM> GetOrgRegionSetting(int RegionID)
    {
        List<SystemRegionSettingVM> s = new List<SystemRegionSettingVM>();
        if (SystemOrgRegion.dictRegionSetting !=null && SystemOrgRegion.dictRegionSetting.ContainsKey(RegionID))
            s = SystemOrgRegion.dictRegionSetting[RegionID];

        return s;
    }
      
     /// <summary>
    /// 轄區所屬County
     /// </summary>
     /// <param name="OrgID"></param>
     /// <returns></returns>
    public static List<SystemAreaCodeVM> GetRegionCountyList(int OrgID)
    { 
        List<SystemAreaCodeVM> list = SystemAreaCode.GetCountyList();

        SystemOrgVM OrgVM = SystemOrg.GetVM(OrgID);
        //判斷是哪種level
        if (OrgVM.OrgLevel == 4)  //衛生所
        {       List<int> RegionCountyIDs = new List<int>(); 
            List<SystemOrgRegionVM> RegionList = GetOrgRegion(OrgID);

            foreach (SystemOrgRegionVM r in RegionList) 
             {
                List<SystemRegionSettingVM> SettingList = GetOrgRegionSetting(r.RegionID);
                foreach (SystemRegionSettingVM s in SettingList)   RegionCountyIDs.Add(s.CountyID);
               
             }   
             
            list = list.Where(ac => RegionCountyIDs.Contains(ac.ID)).ToList();

        }else
            if (OrgVM.OrgLevel == 3 || OrgVM.OrgLevel == 2)//衛生局 管制中心
            {
                List<SystemOrgRegionCountyVM> CountyList = GetOrgRegionCounty(OrgID);
                List<int> RegionCountyIDs = new List<int>(); 
                foreach (SystemOrgRegionCountyVM rc in CountyList) RegionCountyIDs.Add(rc.CountyID);
                list = list.Where(ac => RegionCountyIDs.Contains(ac.ID)).ToList();
            }
           //level 1全部


        return list;
    }

    /// <summary>
    /// 轄區所屬Town
    /// </summary>
    /// <param name="OrgID"></param>
    /// <param name="CountyID"></param>
    /// <returns></returns>
    public static List<SystemAreaCodeVM> GetRegionTownList(int OrgID,int CountyID)
    {
        SystemOrgVM OrgVM = SystemOrg.GetVM(OrgID);
        List<SystemAreaCodeVM> list = SystemAreaCode.GetTownList(CountyID);
        List<int> RegionTownIDs = new List<int>();
         if (OrgVM.OrgLevel == 4)  //衛生所
        {
            List<SystemOrgRegionVM> RegionList = GetOrgRegion(OrgID); 

            foreach (SystemOrgRegionVM r in RegionList)
            {  
                if (SystemOrgRegion.dictRegionSetting.ContainsKey(r.RegionID))
                   RegionTownIDs.AddRange(dictRegionSetting[r.RegionID].Select(l => l.TownID).Distinct());
            }
        list = list.Where(ac => RegionTownIDs.Contains(ac.ID)).ToList();
        }
          
        return list;
         

    }
    /// <summary>
    /// 轄區所屬Village
    /// </summary>
    /// <param name="OrgID"></param>
    /// <param name="TownID"></param>
    /// <returns></returns>
    public static List<SystemAreaCodeVM> GetRegionVillageList(int OrgID,int TownID)
    {
        SystemOrgVM OrgVM = SystemOrg.GetVM(OrgID);
        List<SystemAreaCodeVM> list = SystemAreaCode.GetVillageList(TownID);
        if (OrgVM.OrgLevel == 4)  //衛生所
        {
            List<int> RegionVillageIDs = new List<int>();

            List<SystemOrgRegionVM> RegionList = GetOrgRegion(OrgID);
            foreach (SystemOrgRegionVM r in RegionList)
            {
                if (SystemOrgRegion.dictRegionSetting.ContainsKey(r.RegionID))
                    RegionVillageIDs.AddRange(dictRegionSetting[r.RegionID].Select(l => l.VillageID).Distinct());
            }
            list = list.Where(ac => RegionVillageIDs.Contains(ac.ID)).ToList();
        }
        return list;
    }


    /// <summary>
    /// 檢查個案戶籍是否為組織管轄內
    /// </summary>
    /// <param name="OrgID"></param>
    /// <param name="CaseID"></param>
    /// <returns>true false</returns>
    public static bool IsCaseInOrgRegion(int OrgID,int CaseID)
    {
        CaseUserProfile c = new CaseUserProfile(CaseID);
        SystemOrgVM OrgVM = SystemOrg.GetVM(OrgID); 
        if (OrgVM.OrgLevel == 4)  //衛生所
        {
        
            List<SystemOrgRegionVM> RegionList = GetOrgRegion(OrgID);
            if (RegionList.Find(r => r.RegionID == Convert.ToInt32(c.RegionID)) != null)
            {
                return true;
            }
        }else
            if (OrgVM.OrgLevel == 3 || OrgVM.OrgLevel == 2)//衛生局 管制中心
            {
                List<SystemOrgRegionCountyVM> CountyList = GetOrgRegionCounty(OrgID);

                if (CountyList.Find(rc => rc.CountyID == Convert.ToInt32(c.ResCounty)) != null)
                {
                    return true;
                }

            }
            else if (OrgVM.OrgLevel == 1)  //CDC
            {
                return true;

            }
        return false;
        
    }
    /// <summary>
    /// 檢查縣市鄉鎮是否為組織管轄內
    /// </summary>
    /// <param name="OrgID">組織</param>
    /// <param name="CountyID"></param>
    /// <param name="TownID"></param>
    /// <param name="VillageID"></param>
    /// <returns>true false</returns>
    public static bool IsInOrgRegion(int OrgID, int CountyID, int TownID, int VillageID)
    {
         SystemOrgVM OrgVM = SystemOrg.GetVM(OrgID); 
            if (OrgVM.OrgLevel == 4)  //衛生所
            {
                    List<SystemOrgRegionVM> RegionList = GetOrgRegion(OrgID);

                    foreach (SystemOrgRegionVM r in RegionList)
                    {
                        List<SystemRegionSettingVM> sl = GetOrgRegionSetting(r.RegionID);
                        SystemRegionSettingVM rs = sl.Find(s => (s.CountyID == CountyID) && (s.TownID == TownID) && (s.VillageID == VillageID));
                        if ( rs !=null ) return true; 
                    }
            }else
            if (OrgVM.OrgLevel == 3 || OrgVM.OrgLevel == 2)//衛生局 管制中心
            {
                List<SystemOrgRegionCountyVM> CountyList = GetOrgRegionCounty(OrgID);

                if (CountyList.Find(rc => rc.CountyID == CountyID) != null) return true;
               

            }
            else if (OrgVM.OrgLevel == 1)  //CDC
            {
                return true;

            }
        return false;
    }

}