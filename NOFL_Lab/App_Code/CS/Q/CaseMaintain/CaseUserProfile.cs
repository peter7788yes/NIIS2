using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// CaseUserProfile 的摘要描述
/// </summary>
public class CaseUserProfile
{
    public CaseUserProfile()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //


     CaseID  = 0 ;
     BirthDate  = "";
     IdNo  = "";
     PassportNo  = "";
     ResNo  = "";
     OtherNo  = "";
     Name  = "";
     EngName  = "";
     HouseNo  = "";
     Gender  = "";
     GenderName  = ""; 
     ONationality  = "";
     ONationalityName  = ""; 
    Language  = new string []{};
     LanguageName  = ""; 
   Capacity  =  new string []{};
     CapacityName  = ""; 
     ConCounty  = "";
     ConCountyName  = ""; 
     ConTown  = "";
     ConTownName  = ""; 
     ConVillage  = "";
     ConVillageName  = ""; 
     ConAddr  = ""; 
     ResCounty  = "";
     ResCountyName  = "";
     ResTown  = "";
     ResTownName  = "";
     ResVillage  = "";
     ResVillageName  = ""; 
     ResAddr  = ""; 
     MotherName  = "";
     MotherIdNo  = "";
     MotherBirthDate  = ""; 

    }


    public int CaseID { get; set; }
    public string BirthDate { get; set; }
    public string IdNo { get; set; }
    public string PassportNo { get; set; }
    /// <summary>
    /// 居留證號
    /// </summary>
    public string ResNo { get; set; }
    /// <summary>
    /// 其他證號
    /// </summary>
    public string OtherNo { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }

    /// <summary>
    /// 戶號
    /// </summary>
    public string HouseNo { get; set; }

    public string Gender { get; set; }
    public string GenderName { get; set; }
    /// <summary>
    /// 原屬國籍
    /// </summary>
    public string ONationality { get; set; }
    public string ONationalityName { get; set; }

    public string[] Language { get; set; }
    public string LanguageName { get; set; }

    public string[] Capacity { get; set; }

    public string CapacityName { get; set; }

    /// <summary>
    /// 通訊地-縣市值
    /// </summary>
    public string ConCounty { get; set; }
    /// <summary>
    /// 通訊地-縣市名
    /// </summary>
    public string ConCountyName { get; set; }

    /// <summary>
    /// 通訊地-鄉鎮值
    /// </summary>
    public string ConTown { get; set; }
    /// <summary>
    /// 通訊地-鄉鎮名
    /// </summary>
    public string ConTownName { get; set; }

    /// <summary>
    /// 通訊地-村里值
    /// </summary>
    public string ConVillage { get; set; }
    /// <summary>
    /// 通訊地-村里名
    /// </summary>
    public string ConVillageName { get; set; }
    /// <summary>
    /// 通訊地-地址
    /// </summary>
    public string ConAddr { get; set; }
    /// <summary>
    /// 通訊地-完整地址
    /// </summary>
    public string ConFullAddress
    {
        get { return ConCountyName + ConTownName + ConVillageName + ConAddr; }
         
    }

    /// <summary>
    /// 戶籍地-縣市值
    /// </summary>
    public string ResCounty { get; set; }
    /// <summary>
    /// 戶籍地-縣市名
    /// </summary>
    public string ResCountyName { get; set; }
    /// <summary>
    /// 戶籍地-鄉鎮值
    /// </summary>
    public string ResTown { get; set; }
    /// <summary>
    /// 戶籍地-鄉鎮名
    /// </summary>
    public string ResTownName { get; set; }
    /// <summary>
    /// 戶籍地-村里值
    /// </summary>
    public string ResVillage { get; set; }
    /// <summary>
    /// 戶籍地-村里名
    /// </summary>
    public string ResVillageName { get; set; }

    /// <summary>
    /// 戶籍地-地址
    /// </summary>
    public string ResAddr { get; set; }
    /// <summary>
    /// 戶籍地-完整地址
    /// </summary>
    public string ResFullAddress
    {
        get { return ResCountyName + ResTownName + ResVillageName + ResAddr; }

    }


    public string MotherName { get; set; }
    public string MotherIdNo { get; set; }
    public string MotherBirthDate { get; set; }


    public CaseUserProfile(int CaseUserID):base()
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                       "dbo.usp_CaseUser_xGetCaseUserWithMother {0}",
                       new string[] { CaseUserID.ToString() },
                        NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        SystemCode.Update();
        SystemAreaCode.Update();

        if (dt.Rows.Count > 0)
        {
            CaseID = CaseUserID;
            BirthDate = dt.Rows[0]["BirthDateSimple"].ToString();
            IdNo = dt.Rows[0]["IdNo"].ToString();
            PassportNo = dt.Rows[0]["PassportNo"].ToString();
            ResNo = dt.Rows[0]["ResNo"].ToString();
            OtherNo = dt.Rows[0]["OtherNo"].ToString();
            Name = dt.Rows[0]["ChName"].ToString();
            EngName = dt.Rows[0]["EnName"].ToString();
            Gender = dt.Rows[0]["Gender"].ToString();
            GenderName = SystemCode.GetName("CaseUser_Gender", Convert.ToInt32(Gender));
            HouseNo = dt.Rows[0]["HouseNo"].ToString();
            ONationality = dt.Rows[0]["ONationality"].ToString();

            Language = dt.Rows[0]["Language"].ToString().Split(',');
            foreach (string s in Language)
                if (s != "") LanguageName += SystemCode.GetName("CaseUser_Language", Convert.ToInt32(s)) + ",";
           
            LanguageName = (LanguageName!=null ? LanguageName.TrimEnd(',') :"");

            Capacity = dt.Rows[0]["Capacity"].ToString().Split(',');
            foreach (string s in Capacity)
                if (s != "") CapacityName += SystemCode.GetName("CaseUser_Capacity", Convert.ToInt32(s))+",";
            CapacityName = (CapacityName != null ? CapacityName.TrimEnd(',') : "");

            ConCounty = dt.Rows[0]["ConCounty"].ToString();
            if (ConCounty != "") ConCountyName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ConCounty"]));

            ConTown = dt.Rows[0]["ConTown"].ToString();
            if (ConTown != "")  ConTownName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ConTown"]));

            ConVillage = dt.Rows[0]["ConVillage"].ToString();
            if (ConVillage!="") ConVillageName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ConVillage"]));

            ConAddr = dt.Rows[0]["ConAddr"].ToString(); 

            ResCounty = dt.Rows[0]["ResCounty"].ToString();
            if (ResCounty != "") ConCountyName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ResCounty"]));

            ResTown = dt.Rows[0]["ResTown"].ToString();
            if (ResTown != "") ResTownName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ResTown"]));

            ResVillage = dt.Rows[0]["ResVillage"].ToString();
            if (ResVillage != "") ResVillageName = SystemAreaCode.GetName(Convert.ToInt32(dt.Rows[0]["ResVillage"]));

            ResAddr = dt.Rows[0]["ResAddr"].ToString();

            MotherName= dt.Rows[0]["MotherName"].ToString();
            MotherIdNo= dt.Rows[0]["MotherIdNo"].ToString();
            MotherBirthDate = dt.Rows[0]["MotherBirthDateSimple"].ToString();

             


            // PregWeek = dt.Rows[0]["PregWeek"].ToString();
            // BirthNum = dt.Rows[0]["BirthNum"].ToString();
            // BirthSeq = dt.Rows[0]["BirthSeq"].ToString();
            //BirthWeight  = dt.Rows[0]["BirthWeight"].ToString();
            //BirthPlace = dt.Rows[0]["BirthPlace"].ToString();
            // Deliver  = dt.Rows[0]["Deliver"].ToString();
            //  DeliverOrg = dt.Rows[0]["DeliverOrg"].ToString();
            //  MarryStatus  = dt.Rows[0]["MarryStatus"].ToString();

            //  EduLevel = dt.Rows[0]["EduLevel"].ToString();
            //  ElemSchool = dt.Rows[0]["ElemSchool"].ToString();
            //Occupation = dt.Rows[0]["Occupation"].ToString();
            // EduLevel = dt.Rows[0]["EduLevel"].ToString();
            //EduLevel = dt.Rows[0]["EduLevel"].ToString();

            //  ResAddr  = dt.Rows[0]["ResAddr"].ToString();
        
            //  BirthMulti = dt.Rows[0]["BirthMulti"].ToString();





        }
    }



}