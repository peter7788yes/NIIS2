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
     ChName  = "";
     EnName = "";
     HouseNo  = "";
     Gender = 0;
     GenderName  = "";
     ONationality = 0;
     ONationalityName  = ""; 
    Language  = new string []{};
     LanguageName  = ""; 
   Capacity  =  new string []{};
     CapacityName  = "";
     ConCounty = 0;
     ConCountyName  = "";
     ConTown = 0;
     ConTownName  = "";
     ConVillage = 0;
     ConVillageName  = ""; 
     ConAddr  = "";
     ResCounty = 0;
     ResCountyName  = "";
     ResTown = 0;
     ResTownName  = "";
     ResVillage = 0;
     ResVillageName  = ""; 
     ResAddr  = ""; 
     MotherName  = "";
     MotherIdNo  = "";
     MotherBirthDate  = "";
     Mobiles = new List<UserMobile>();
     Emails = new List<UserEmail>();
     BirthMulti = 0;

     PregWeek = 0;
     BirthNum = 0;
     BirthSeq = 0;
     BirthWeight = 0;
     BirthPlace = 0;
     Deliver = 0;
     MainContactID = 0; 
    
    }

    
    public int CaseID { get; set; }
    /// <summary>
    /// 生日
    /// </summary>
    public string BirthDate { get; set; }

    /// <summary>
    /// 身份證號
    /// </summary>
    public string IdNo { get; set; }

    /// <summary>
    /// 護照號碼
    /// </summary>
    public string PassportNo { get; set; }
    /// <summary>
    /// 居留證號
    /// </summary>
    public string ResNo { get; set; }
    /// <summary>
    /// 其他證號
    /// </summary>
    public string OtherNo { get; set; }

    /// <summary>
    /// 中文名
    /// </summary>
    public string ChName { get; set; }


    /// <summary>
    /// 英文名
    /// </summary>
    public string EnName { get; set; }

    /// <summary>
    /// 戶號
    /// </summary>
    public string HouseNo { get; set; }

    /// <summary>
    /// 性別代碼
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 性別名稱
    /// </summary>
    public string GenderName { get; set; }
    /// <summary>
    /// 原屬國籍代碼
    /// </summary>
    public int ONationality { get; set; }

    /// <summary>
    /// 原屬國籍名稱
    /// </summary>
    public string ONationalityName { get; set; }

    /// <summary>
    /// 出入境代碼
    /// </summary>
    public int ImmiType { get; set; }
    /// <summary>
    /// 出入境名稱
    /// </summary>
    public string ImmiTypeName { get; set; }



    public string[] Language { get; set; }

    public string LanguageName { get; set; }


    public string[] Capacity { get; set; }

    public string CapacityName { get; set; }



    public string TelDayArea { get; set; }
    public string TelDayNo { get; set; }
    public string TelDayExt { get; set; }

       public string FullTelDay
       {
           get { return TelDayArea.ToString() + "-" + TelDayNo.ToString() + "#" + TelDayExt.ToString(); }

       }

       public string TelNightArea { get; set; }
       public string TelNightNo { get; set; }
       public string TelNightExt { get; set; }

       public string FullTelNight
       {
           get { return TelNightArea.ToString() + "-" + TelNightNo.ToString() + "#" + TelNightExt.ToString(); }

       }



    /// <summary>
    /// 通訊地-縣市值
    /// </summary>
       public int ConCounty { get; set; }
    /// <summary>
    /// 通訊地-縣市名
    /// </summary>
       public string ConCountyName { get; set; }

    /// <summary>
    /// 通訊地-鄉鎮值
    /// </summary>
    public int ConTown { get; set; }
    /// <summary>
    /// 通訊地-鄉鎮名
    /// </summary>
    public string ConTownName { get; set; }

    /// <summary>
    /// 通訊地-村里值
    /// </summary>
    public int ConVillage { get; set; }
    /// <summary>
    /// 通訊地-村里名
    /// </summary>
    public string ConVillageName { get; set; }

    /// <summary>
    /// 通訊地-鄰值
    /// </summary>
    public int ConNei { get; set; }
    /// <summary>
    /// 通訊地-鄰名
    /// </summary>
    public string ConNeiName { get { return (ConNei  ==  0  ? "" : ConNei + "鄰"); } }


    /// <summary>
    /// 通訊地-地址
    /// </summary>
    public string ConAddr { get; set; }
    /// <summary>
    /// 通訊地-完整地址
    /// </summary>
    public string ConFullAddress
    {
        get { return ConCountyName + ConTownName + ConVillageName + ConNeiName + ConAddr; }
         
    }

    /// <summary>
    /// 戶籍地-縣市值
    /// </summary>
    public int ResCounty { get; set; }
    /// <summary>
    /// 戶籍地-縣市名
    /// </summary>
    public string ResCountyName { get; set; }
    /// <summary>
    /// 戶籍地-鄉鎮值
    /// </summary>
    public int ResTown { get; set; }
    /// <summary>
    /// 戶籍地-鄉鎮名
    /// </summary>
    public string ResTownName { get; set; }
    /// <summary>
    /// 戶籍地-村里值
    /// </summary>
    public int ResVillage { get; set; }
    /// <summary>
    /// 戶籍地-村里名
    /// </summary>
    public string ResVillageName { get; set; }


    /// <summary>
    /// 戶籍地-鄰值
    /// </summary>
    public int ResNei { get; set; }
    /// <summary>
    /// 戶籍地-鄰名
    /// </summary>
    public string ResNeiName { get { return (ResNei== 0  ? "" : ResNei + "鄰"); } }



    /// <summary>
    /// 戶籍地-地址
    /// </summary>
    public string ResAddr { get; set; }
    /// <summary>
    /// 戶籍地-完整地址
    /// </summary>
    public string ResFullAddress
    {
        get { return ResCountyName + ResTownName + ResVillageName + ResNeiName + ResAddr; }

    }


    public string BirthPlaceOther { get; set; }
    public int BirthMulti { get; set; }
    public int PregWeek { get; set; }
    public int BirthNum { get; set; }
    public int BirthSeq { get; set; }
    public int BirthWeight { get; set; }
    public int BirthPlace { get; set; }
    public int Deliver { get; set; }
    public string DeliverOrg { get; set; }
    public string MarryStatus { get; set; }
    public string EduLevel { get; set; }
    public string ElemSchool { get; set; }
    public string Occupation { get; set; }



    /// <summary>
    ///  轄區代碼
    /// </summary>
    public int RegionID { get; set; }
    public string RegionName { get; set; }

    /// <summary>
    /// 主要聯絡人CaseID
    /// </summary>
    public int MainContactCaseID { get; set; }

    public string MotherName { get; set; }
    public string MotherIdNo { get; set; }
    public string MotherBirthDate { get; set; }



    public string AgeTip { get; set; }



    public List<UserMobile> Mobiles { get; set; }
    public List<UserEmail> Emails { get; set; }


    public int MainContactID { get; set; }


    //public int CreateOrgID { get; set; }
    //public int CreateUserID { get; set; }
    //public DateTime CreateDate { get; set; }
    //public int ModifyOrgID { get; set; }
    //public int ModifyUserID { get; set; }
    //public DateTime ModifyDate { get; set; }

    public string CreateInfo { get; set; }
    public string ModifyInfo { get; set; }


    /// <summary>
    /// 個人詳細資料(含媽媽資料)
    /// </summary>
    /// <param name="CaseUserID"></param>
    public  void GetProfileWithMother(int CaseUserID) 
        
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                   "dbo.usp_CaseUser_xGetCaseUserWithMother {0}",
                   new string[] { CaseUserID.ToString() },
                    NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        


          SetAttr(dt);






   //     if (dt.Rows.Count > 0)
   //     {
   //         int iGender, iONationality, iImmiType
   //            , iConCounty, iConTown, iConVillage, iConNei
   //            , iResCounty, iResTown, iResVillage, iResNei
   //            , iPregWeek, iBirthNum, iBirthSeq, iBirthWeight
   //            , iBirthPlace, iDeliver, iBirthMulti
   //            , iRegionID;
             


   //         CaseID = CaseUserID;
   //         BirthDate = dt.Rows[0]["BirthDateSimple"].ToString();
   //         IdNo = dt.Rows[0]["IdNo"].ToString();
   //         PassportNo = dt.Rows[0]["PassportNo"].ToString();
   //         ResNo = dt.Rows[0]["ResNo"].ToString();
   //         OtherNo = dt.Rows[0]["OtherNo"].ToString();
   //         ChName = dt.Rows[0]["ChName"].ToString();
   //         EnName = dt.Rows[0]["EnName"].ToString(); 
   //         HouseNo = dt.Rows[0]["HouseNo"].ToString(); 
   //         Language = dt.Rows[0]["Language"].ToString().Split(',');
   //         foreach (string s in Language)
   //             if (s != "") LanguageName += SystemCode.GetName("CaseUser_Language", Convert.ToInt32(s)) + ",";

   //         LanguageName = (LanguageName != null ? LanguageName.TrimEnd(',') : "");

   //         Capacity = dt.Rows[0]["Capacity"].ToString().Split(',');
   //         foreach (string s in Capacity)
   //             if (s != "") CapacityName += SystemCode.GetName("CaseUser_Capacity", Convert.ToInt32(s)) + ",";
   //         CapacityName = (CapacityName != null ? CapacityName.TrimEnd(',') : "");




   //         TelDayArea = dt.Rows[0]["TelDayArea"].ToString();
   //         TelDayNo = dt.Rows[0]["TelDayNo"].ToString();
   //         TelDayExt = dt.Rows[0]["TelDayExt"].ToString();


   //         TelNightArea = dt.Rows[0]["TelNightArea"].ToString();
   //         TelNightNo = dt.Rows[0]["TelNightNo"].ToString();
   //         TelNightExt = dt.Rows[0]["TelNightExt"].ToString(); 
   //         ConAddr = dt.Rows[0]["ConAddr"].ToString(); 
   //         ResAddr = dt.Rows[0]["ResAddr"].ToString(); 
   //         MotherName = dt.Rows[0]["MotherName"].ToString();
   //         MotherIdNo = dt.Rows[0]["MotherIdNo"].ToString();
   //         MotherBirthDate = dt.Rows[0]["MotherBirthDateSimple"].ToString(); 
   //         DeliverOrg = dt.Rows[0]["DeliverOrg"].ToString();
   //         MarryStatus = dt.Rows[0]["MarryStatus"].ToString(); 
   //         ElemSchool = dt.Rows[0]["ElemSchool"].ToString();
   //         Occupation = dt.Rows[0]["Occupation"].ToString();
   //         EduLevel = dt.Rows[0]["EduLevel"].ToString();
             

   //         int.TryParse(dt.Rows[0]["ONationality"].ToString (), out iONationality);
   //         int.TryParse(dt.Rows[0]["ImmiType"].ToString(), out iImmiType);
   //         int.TryParse(dt.Rows[0]["Gender"].ToString(), out iGender);

   //         int.TryParse(dt.Rows[0]["ConCounty"].ToString(), out iConCounty);
   //         int.TryParse(dt.Rows[0]["ConTown"].ToString(), out iConTown);
   //         int.TryParse(dt.Rows[0]["ConVillage"].ToString(), out iConVillage);
   //         int.TryParse(dt.Rows[0]["ConNei"].ToString(), out iConNei);

   //         int.TryParse(dt.Rows[0]["ResCounty"].ToString(), out iResCounty);
   //         int.TryParse(dt.Rows[0]["ResTown"].ToString(), out iResTown);
   //         int.TryParse(dt.Rows[0]["ResVillage"].ToString(), out iResVillage);
   //         int.TryParse(dt.Rows[0]["ResNei"].ToString(), out iResNei);

   //         int.TryParse(dt.Rows[0]["PregWeek"].ToString(), out iPregWeek);
   //         int.TryParse(dt.Rows[0]["BirthNum"].ToString(), out iBirthNum);
   //         int.TryParse(dt.Rows[0]["BirthSeq"].ToString(), out iBirthSeq);
   //         int.TryParse(dt.Rows[0]["BirthWeight"].ToString(), out iBirthWeight);
   //         int.TryParse(dt.Rows[0]["BirthPlace"].ToString(), out iBirthPlace);
   //         int.TryParse(dt.Rows[0]["Deliver"].ToString(), out iDeliver);
   //         int.TryParse(dt.Rows[0]["BirthMulti"].ToString(), out iBirthMulti);
   //         int.TryParse(dt.Rows[0]["RegionID"].ToString(), out iRegionID);

   //         ONationality = iONationality;
   //         ImmiType = iImmiType;
   //         Gender = iGender;

   //         ConCounty = iConCounty;
   //         ConTown = iConTown;
   //         ConVillage = iConVillage;
   //         ConNei = iConNei;

   //         ResCounty = iResCounty;
   //         ResTown = iResTown;
   //         ResVillage = iResVillage;
   //         ResNei = iResNei;


   //         PregWeek = iPregWeek;
   //         BirthNum = iBirthNum;
   //         BirthSeq = iBirthSeq;
   //         BirthWeight = iBirthWeight;
   //         BirthPlace = iBirthPlace;
   //         Deliver =iDeliver;
   //         BirthMulti = iBirthMulti; 
   //         RegionID = iRegionID;


   //          RegionName = dt.Rows[0]["RegionName"].ToString(); 
   //ImmiTypeName = SystemCode.GetName("CaseUser_ImmiType", ImmiType);
 
   //         GenderName = SystemCode.GetName("CaseUser_Gender",  Gender );
   //      ConCountyName = SystemAreaCode.GetName(ConCounty); 
   //         ConTownName = SystemAreaCode.GetName(ConTown); 
   //          ConVillageName = SystemAreaCode.GetName(ConVillage);  
   //          ResCountyName = SystemAreaCode.GetName(ResCounty);
   //          ResTownName = SystemAreaCode.GetName(ResTown);
   //          ResVillageName = SystemAreaCode.GetName(ResVillage);
   //         GetMobiles();
   //         GetEmails();

            
   //         try
   //         {
   //             AgeCalculatorT AgeCal = new AgeCalculatorT(); 
   //             DateTime b = new DateTime();
   //             DateTime.TryParse(dt.Rows[0]["BirthDate"].ToString (), out b);
   //             AgeTip = AgeCal.GetAge(b);
   //         }
   //         catch { 
   //         }



         
    }
     
    private void GetMobiles()
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                       "SELECT MobileID,[MobileNo]  FROM [C_CaseUserMobile] where [LogicDel]=0 and [CaseID]={0} order by [MobileID]  ",
                       new string[] { CaseID.ToString() },
                        NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
         Mobiles = new List<UserMobile>();

        foreach (DataRow r in dt.Rows)   Mobiles.Add (new UserMobile(Convert.ToInt32(r["MobileID"]), r["MobileNo"].ToString()));
         
    
    }
    private void GetEmails()
    {
        DataTable dt  = (DataTable)DBUtil.DBOp("ConnDB",
                  "SELECT [EmailID],[Email]  FROM [C_CaseUserEmail] where [LogicDel]=0 and [CaseID]={0} order by [EmailID]  ",
                  new string[] { CaseID.ToString() },
                   NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        Emails = new List<UserEmail>();
        foreach (DataRow r in dt.Rows) Emails.Add(new UserEmail(Convert.ToInt32(r["EmailID"]), r["Email"].ToString()));
         

    }


    /// <summary>
    /// 個人詳細資料(沒有媽媽資料喔)
    /// </summary>
    /// <param name="CaseUserID"></param>
    public CaseUserProfile(int CaseUserID):base()
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                       "dbo.usp_CaseUser_xGetCaseUser {0}",
                       new string[] { CaseUserID.ToString() },
                        NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

         
        if (dt.Rows.Count > 0)
        {
            SetAttr(dt);
            //CaseID = CaseUserID;
            //BirthDate = dt.Rows[0]["BirthDateSimple"].ToString();
            //IdNo = dt.Rows[0]["IdNo"].ToString();
            //PassportNo = dt.Rows[0]["PassportNo"].ToString();
            //ResNo = dt.Rows[0]["ResNo"].ToString();
            //OtherNo = dt.Rows[0]["OtherNo"].ToString();
            //ChName = dt.Rows[0]["ChName"].ToString();
            //EnName = dt.Rows[0]["EnName"].ToString();
            //Gender = Convert.ToInt32(dt.Rows[0]["Gender"]);
            //GenderName = SystemCode.GetName("CaseUser_Gender", Gender);
            //HouseNo = dt.Rows[0]["HouseNo"].ToString();
            //ONationality = Convert.ToInt32(dt.Rows[0]["ONationality"]);

            //ImmiType = Convert.ToInt32(dt.Rows[0]["ImmiType"]);
            //ImmiTypeName = SystemCode.GetName("CaseUser_ImmiType", ImmiType);

            //Language = dt.Rows[0]["Language"].ToString().Split(',');
            //foreach (string s in Language)
            //    if (s != "") LanguageName += SystemCode.GetName("CaseUser_Language", Convert.ToInt32(s)) + ",";

            //LanguageName = (LanguageName != null ? LanguageName.TrimEnd(',') : "");

            //Capacity = dt.Rows[0]["Capacity"].ToString().Split(',');
            //foreach (string s in Capacity)
            //    if (s != "") CapacityName += SystemCode.GetName("CaseUser_Capacity", Convert.ToInt32(s)) + ",";
            //CapacityName = (CapacityName != null ? CapacityName.TrimEnd(',') : "");




            //TelDayArea = dt.Rows[0]["TelDayArea"].ToString();
            //TelDayNo = dt.Rows[0]["TelDayNo"].ToString();
            //TelDayExt = dt.Rows[0]["TelDayExt"].ToString();


            //TelNightArea = dt.Rows[0]["TelNightArea"].ToString();
            //TelNightNo = dt.Rows[0]["TelNightNo"].ToString();
            //TelNightExt = dt.Rows[0]["TelNightExt"].ToString();





            //ConCounty = Convert.ToInt32(dt.Rows[0]["ConCounty"]);
            //if (ConCounty != 0) ConCountyName = SystemAreaCode.GetName(ConCounty);

            //ConTown = Convert.ToInt32(dt.Rows[0]["ConTown"]);
            //if (ConTown != 0) ConTownName = SystemAreaCode.GetName(ConTown);

            //ConVillage = Convert.ToInt32(dt.Rows[0]["ConVillage"]);
            //if (ConVillage != 0) ConVillageName = SystemAreaCode.GetName(ConVillage);


            //ConNei = Convert.ToInt32(dt.Rows[0]["ConNei"]);


            //ConAddr = dt.Rows[0]["ConAddr"].ToString();

            //ResCounty = Convert.ToInt32(dt.Rows[0]["ResCounty"]);
            //if (ResCounty != 0) ResCountyName = SystemAreaCode.GetName(ResCounty);

            //ResTown = Convert.ToInt32(dt.Rows[0]["ResTown"]);
            //if (ResTown != 0) ResTownName = SystemAreaCode.GetName(ResTown);

            //ResVillage = Convert.ToInt32(dt.Rows[0]["ResVillage"]);
            //if (ResVillage != 0) ResVillageName = SystemAreaCode.GetName(ResVillage);

            //ResNei = Convert.ToInt32(dt.Rows[0]["ResNei"]);

            //ResAddr = dt.Rows[0]["ResAddr"].ToString();


            ////MotherName = dt.Rows[0]["MotherName"].ToString();
            ////MotherIdNo = dt.Rows[0]["MotherIdNo"].ToString();
            ////MotherBirthDate = dt.Rows[0]["MotherBirthDateSimple"].ToString();

            //PregWeek = Convert.ToInt32(dt.Rows[0]["PregWeek"]);
            //BirthNum = Convert.ToInt32(dt.Rows[0]["BirthNum"]);
            //BirthSeq = Convert.ToInt32(dt.Rows[0]["BirthSeq"]);
            //BirthWeight = Convert.ToInt32(dt.Rows[0]["BirthWeight"]);
            //BirthPlace = Convert.ToInt32(dt.Rows[0]["BirthPlace"]);
            //Deliver = Convert.ToInt32(dt.Rows[0]["Deliver"]);
            //DeliverOrg = dt.Rows[0]["DeliverOrg"].ToString();
            //MarryStatus = dt.Rows[0]["MarryStatus"].ToString();

            //ElemSchool = dt.Rows[0]["ElemSchool"].ToString();
            //Occupation = dt.Rows[0]["Occupation"].ToString();
            //EduLevel = dt.Rows[0]["EduLevel"].ToString();


            //BirthMulti = Convert.ToInt32(dt.Rows[0]["BirthMulti"]);


            //RegionID = Convert.ToInt32(dt.Rows[0]["RegionID"]);
            //RegionName = dt.Rows[0]["RegionName"].ToString();


            




        }
    }
    protected void SetAttr(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            int iGender, iONationality, iImmiType
                          , iConCounty, iConTown, iConVillage, iConNei
                          , iResCounty, iResTown, iResVillage, iResNei
                          , iPregWeek, iBirthNum, iBirthSeq, iBirthWeight
                          , iBirthPlace, iDeliver, iBirthMulti
                          , iRegionID, iMainContactCaseID;



            CaseID = Convert.ToInt32(dt.Rows[0]["CaseID"]);
            BirthDate = dt.Rows[0]["BirthDateSimple"].ToString();
            IdNo = dt.Rows[0]["IdNo"].ToString();
            PassportNo = dt.Rows[0]["PassportNo"].ToString();
            ResNo = dt.Rows[0]["ResNo"].ToString();
            OtherNo = dt.Rows[0]["OtherNo"].ToString();
            ChName = dt.Rows[0]["ChName"].ToString();
            EnName = dt.Rows[0]["EnName"].ToString();
            HouseNo = dt.Rows[0]["HouseNo"].ToString();
            Language = dt.Rows[0]["Language"].ToString().Split(',');
            foreach (string s in Language)
                if (s != "") LanguageName += SystemCode.GetName("CaseUser_Language", Convert.ToInt32(s)) + ",";

            LanguageName = (LanguageName != null ? LanguageName.TrimEnd(',') : "");

            Capacity = dt.Rows[0]["Capacity"].ToString().Split(',');
            foreach (string s in Capacity)
                if (s != "") CapacityName += SystemCode.GetName("CaseUser_Capacity", Convert.ToInt32(s)) + ",";
            CapacityName = (CapacityName != null ? CapacityName.TrimEnd(',') : "");




            TelDayArea = dt.Rows[0]["TelDayArea"].ToString();
            TelDayNo = dt.Rows[0]["TelDayNo"].ToString();
            TelDayExt = dt.Rows[0]["TelDayExt"].ToString();
            TelNightArea = dt.Rows[0]["TelNightArea"].ToString();
            TelNightNo = dt.Rows[0]["TelNightNo"].ToString();
            TelNightExt = dt.Rows[0]["TelNightExt"].ToString();
            ConAddr = dt.Rows[0]["ConAddr"].ToString();
            ResAddr = dt.Rows[0]["ResAddr"].ToString();
         
            DeliverOrg = dt.Rows[0]["DeliverOrg"].ToString();
            MarryStatus = dt.Rows[0]["MarryStatus"].ToString();
            ElemSchool = dt.Rows[0]["ElemSchool"].ToString();
            Occupation = dt.Rows[0]["Occupation"].ToString();
            EduLevel = dt.Rows[0]["EduLevel"].ToString();

            BirthPlaceOther = dt.Rows[0]["BirthPlaceOther"].ToString();

            int.TryParse(dt.Rows[0]["ONationality"].ToString(), out iONationality);
            int.TryParse(dt.Rows[0]["ImmiType"].ToString(), out iImmiType);
            int.TryParse(dt.Rows[0]["Gender"].ToString(), out iGender);

            int.TryParse(dt.Rows[0]["ConCounty"].ToString(), out iConCounty);
            int.TryParse(dt.Rows[0]["ConTown"].ToString(), out iConTown);
            int.TryParse(dt.Rows[0]["ConVillage"].ToString(), out iConVillage);
            int.TryParse(dt.Rows[0]["ConNei"].ToString(), out iConNei);

            int.TryParse(dt.Rows[0]["ResCounty"].ToString(), out iResCounty);
            int.TryParse(dt.Rows[0]["ResTown"].ToString(), out iResTown);
            int.TryParse(dt.Rows[0]["ResVillage"].ToString(), out iResVillage);
            int.TryParse(dt.Rows[0]["ResNei"].ToString(), out iResNei);

            int.TryParse(dt.Rows[0]["PregWeek"].ToString(), out iPregWeek);
            int.TryParse(dt.Rows[0]["BirthNum"].ToString(), out iBirthNum);
            int.TryParse(dt.Rows[0]["BirthSeq"].ToString(), out iBirthSeq);
            int.TryParse(dt.Rows[0]["BirthWeight"].ToString(), out iBirthWeight);
            int.TryParse(dt.Rows[0]["BirthPlace"].ToString(), out iBirthPlace);
            int.TryParse(dt.Rows[0]["Deliver"].ToString(), out iDeliver);
            int.TryParse(dt.Rows[0]["BirthMulti"].ToString(), out iBirthMulti);
            int.TryParse(dt.Rows[0]["RegionID"].ToString(), out iRegionID);
            int.TryParse(dt.Rows[0]["MainContactID"].ToString(), out iMainContactCaseID);
            

            ONationality = iONationality;
            ImmiType = iImmiType;
            Gender = iGender;

            ConCounty = iConCounty;
            ConTown = iConTown;
            ConVillage = iConVillage;
            ConNei = iConNei;

            ResCounty = iResCounty;
            ResTown = iResTown;
            ResVillage = iResVillage;
            ResNei = iResNei;


            PregWeek = iPregWeek;
            BirthNum = iBirthNum;
            BirthSeq = iBirthSeq;
            BirthWeight = iBirthWeight;
            BirthPlace = iBirthPlace;
            Deliver = iDeliver;
            BirthMulti = iBirthMulti;
            RegionID = iRegionID;
            MainContactCaseID = iMainContactCaseID;

        //    RegionName = dt.Rows[0]["RegionName"].ToString();
           // SystemRegion.Update();
            RegionName = SystemRegion.GetName(RegionID);
            ImmiTypeName = SystemCode.GetName("CaseUser_ImmiType", ImmiType);
            GenderName = SystemCode.GetName("CaseUser_Gender", Gender);
            ConCountyName = SystemAreaCode.GetName(ConCounty);
            ConTownName = SystemAreaCode.GetName(ConTown);
            ConVillageName = SystemAreaCode.GetName(ConVillage);
            ResCountyName = SystemAreaCode.GetName(ResCounty);
            ResTownName = SystemAreaCode.GetName(ResTown);
            ResVillageName = SystemAreaCode.GetName(ResVillage);


            




            if (dt.Columns.Contains("CreateInfo"))
            CreateInfo = dt.Rows[0]["CreateInfo"].ToString();
            if (dt.Columns.Contains("ModifyInfo"))
            ModifyInfo = dt.Rows[0]["ModifyInfo"].ToString();



            if (dt.Columns.Contains("MotherName"))
            MotherName = dt.Rows[0]["MotherName"].ToString();
            if (dt.Columns.Contains("MotherIdNo"))
            MotherIdNo = dt.Rows[0]["MotherIdNo"].ToString();
            if (dt.Columns.Contains("MotherBirthDateSimple"))
            MotherBirthDate = dt.Rows[0]["MotherBirthDateSimple"].ToString();




            GetMobiles();
            GetEmails();




            try
            {
                AgeCalculatorT AgeCal = new AgeCalculatorT();
                DateTime b = new DateTime();
                DateTime.TryParse(dt.Rows[0]["BirthDate"].ToString(), out b);
                AgeTip = AgeCal.GetAge(b);
            }
            catch
            {
            }
        }
    }



    /// <summary>
    /// 新增Email
    /// </summary>
    /// <param name="caseid"></param>
    /// <param name="emails"></param>
    public void AddEmail(int caseid,string emails)
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xAddCaseUserEmail {0},{1},{2} "
            , new string[] { caseid.ToString(),  AuthServer.GetLoginUser().ID.ToString() ,emails}, NSDBUtil.CmdOpType.ExecuteNonQuery);
     
    }
    /// <summary>
    /// 新增mobile
    /// </summary>
    /// <param name="caseid">15</param>
    /// <param name="emails">a@b.com,q@hyweb.com.tw,qq@msn.com</param>
    public void AddMobile(int caseid, string mobiles)
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xAddCaseUserMobile {0},{1},{2} "
            , new string[] { caseid.ToString(), AuthServer.GetLoginUser().ID.ToString(), mobiles }, NSDBUtil.CmdOpType.ExecuteNonQuery);

    }

    /// <summary>
    /// 修改主資料因減少正規化把mobile欄位放到case_user
    /// </summary>
    /// <param name="caseid"></param>
    public void UpdateMainMobileCol(int caseid )
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xUpdateCaseUserMobiles {0}  "
            , new string[] { caseid.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);

    }

    /// <summary>
    /// 修改主資料因減少正規化把MotherID欄位移出
    /// </summary>
    /// <param name="caseid"></param>
    public void UpdateMainMotherIDCol(int caseid)
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xUpdateCaseUserMotherID {0}  "
            , new string[] { caseid.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);

    } 

    public int Add ()
    {  int NewID = 0;
       
        
        if (BirthDate.Length == 6)  BirthDate = "0" + BirthDate ;
        string ADBirthDate =  (Convert.ToInt32 (BirthDate.Substring(0, 3))+1911).ToString ()  + "/" + BirthDate.Substring(3, 2) + "/" + BirthDate.Substring(5, 2) ;

            
            //if (CaseID != 0) sqlpoName = "dbo.usp_CaseUser_xModifyCaseUser";
     //if (CaseID != 0)
                //cmd.Parameters.AddWithValue("@CaseID", CaseID);

             SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xAddCaseUser");
              cmd.CommandType = CommandType.StoredProcedure;
           

            cmd.Parameters.AddWithValue("@BirthDate", ADBirthDate);
            cmd.Parameters.AddWithValue("@IdNo", IdNo); 
            cmd.Parameters.AddWithValue("@PassportNo", PassportNo); 
            cmd.Parameters.AddWithValue("@ResNo", ResNo); 
            cmd.Parameters.AddWithValue("@OtherNo",OtherNo);
            cmd.Parameters.AddWithValue("@ChName", ChName);
            cmd.Parameters.AddWithValue("@EnName", EnName);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@HouseNo", HouseNo);
            cmd.Parameters.AddWithValue("@ONationality",  ONationality);
            cmd.Parameters.AddWithValue("@Language", String.Join(",", Language));
               //全部加起來  到DB再處理 因為 有些身份別是不能改的
            cmd.Parameters.AddWithValue("@Capacity",  String.Join(",",Capacity) ); 
            cmd.Parameters.AddWithValue("@ResCounty",ResCounty );
            cmd.Parameters.AddWithValue("@ResTown", ResTown);
            cmd.Parameters.AddWithValue("@ResVillage", ResVillage); 
            cmd.Parameters.AddWithValue("@ConCounty",ConCounty);
            cmd.Parameters.AddWithValue("@ConTown", ConTown);
            cmd.Parameters.AddWithValue("@ConVillage", ConVillage); 
            cmd.Parameters.AddWithValue("@PregWeek",PregWeek);
            cmd.Parameters.AddWithValue("@BirthNum",BirthNum );
            cmd.Parameters.AddWithValue("@BirthSeq",BirthSeq  );
            cmd.Parameters.AddWithValue("@BirthWeight",BirthWeight );
            cmd.Parameters.AddWithValue("@BirthPlace",BirthPlace );
            cmd.Parameters.AddWithValue("@Deliver",Deliver );
            cmd.Parameters.AddWithValue("@DeliverOrg",DeliverOrg );
            cmd.Parameters.AddWithValue("@MarryStatus",MarryStatus );
            cmd.Parameters.AddWithValue("@EduLevel", EduLevel );
            cmd.Parameters.AddWithValue("@ElemSchool",  ElemSchool );
            cmd.Parameters.AddWithValue("@Occupation", Occupation  ); 
            cmd.Parameters.AddWithValue("@ModifyUserID", AuthServer.GetLoginUser().ID); 
            cmd.Parameters.AddWithValue("@TelDayArea", TelDayArea);
            cmd.Parameters.AddWithValue("@TelDayNo",TelDayNo);
            cmd.Parameters.AddWithValue("@TelDayExt", TelDayExt);
            cmd.Parameters.AddWithValue("@TelNightArea", TelNightArea);
            cmd.Parameters.AddWithValue("@TelNightNo",TelNightNo);
            cmd.Parameters.AddWithValue("@TelNightExt", TelNightExt); 
            cmd.Parameters.AddWithValue("@ResAddr", ResAddr  );
            cmd.Parameters.AddWithValue("@ConAddr",ConAddr   );
            cmd.Parameters.AddWithValue("@BirthMulti", BirthMulti  ); 
            cmd.Parameters.AddWithValue("@ResNei", ResNei);
            cmd.Parameters.AddWithValue("@ConNei", ConNei);
            cmd.Parameters.AddWithValue("@BirthPlaceOther", BirthPlaceOther);
        DataTable dt = DB.GetDataTable(cmd,"ConnDB");
       
          if (dt.Rows .Count >0)  NewID =Convert.ToInt32 (dt.Rows[0][0]);

        return NewID;
    }


    /// <summary>
    /// 完整更新 提供CDC使用
    /// </summary>
    /// <returns></returns>
    public int Update()
    {
        int RowCount = 0;


        if (BirthDate.Length == 6) BirthDate = "0" + BirthDate;
        string ADBirthDate = (Convert.ToInt32(BirthDate.Substring(0, 3)) + 1911).ToString() + "/" + BirthDate.Substring(3, 2) + "/" + BirthDate.Substring(5, 2);
         

        SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xModifyCaseUser");
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        cmd.Parameters.AddWithValue("@BirthDate", ADBirthDate);
        cmd.Parameters.AddWithValue("@IdNo", IdNo);
        cmd.Parameters.AddWithValue("@PassportNo", PassportNo);
        cmd.Parameters.AddWithValue("@ResNo", ResNo);
        cmd.Parameters.AddWithValue("@OtherNo", OtherNo);
        cmd.Parameters.AddWithValue("@ChName", ChName);
        cmd.Parameters.AddWithValue("@EnName", EnName);
        cmd.Parameters.AddWithValue("@Gender", Gender);
        cmd.Parameters.AddWithValue("@HouseNo", HouseNo);
        cmd.Parameters.AddWithValue("@ONationality", ONationality);
        cmd.Parameters.AddWithValue("@Language", String.Join(",", Language));
        //全部加起來  到DB再處理 因為 有些身份別是不能改的
        cmd.Parameters.AddWithValue("@Capacity", String.Join(",", Capacity));
        cmd.Parameters.AddWithValue("@ResCounty", ResCounty);
        cmd.Parameters.AddWithValue("@ResTown", ResTown);
        cmd.Parameters.AddWithValue("@ResVillage", ResVillage);
        cmd.Parameters.AddWithValue("@ConCounty", ConCounty);
        cmd.Parameters.AddWithValue("@ConTown", ConTown);
        cmd.Parameters.AddWithValue("@ConVillage", ConVillage);
        cmd.Parameters.AddWithValue("@PregWeek", PregWeek);
        cmd.Parameters.AddWithValue("@BirthNum", BirthNum);
        cmd.Parameters.AddWithValue("@BirthSeq", BirthSeq);
        cmd.Parameters.AddWithValue("@BirthWeight", BirthWeight);
        cmd.Parameters.AddWithValue("@BirthPlace", BirthPlace);
        cmd.Parameters.AddWithValue("@Deliver", Deliver);
        cmd.Parameters.AddWithValue("@DeliverOrg", DeliverOrg);
        cmd.Parameters.AddWithValue("@MarryStatus", MarryStatus);
        cmd.Parameters.AddWithValue("@EduLevel", EduLevel);
        cmd.Parameters.AddWithValue("@ElemSchool", ElemSchool);
        cmd.Parameters.AddWithValue("@Occupation", Occupation);
        cmd.Parameters.AddWithValue("@ModifyUserID", AuthServer.GetLoginUser().ID);
        cmd.Parameters.AddWithValue("@TelDayArea", TelDayArea);
        cmd.Parameters.AddWithValue("@TelDayNo", TelDayNo);
        cmd.Parameters.AddWithValue("@TelDayExt", TelDayExt);
        cmd.Parameters.AddWithValue("@TelNightArea", TelNightArea);
        cmd.Parameters.AddWithValue("@TelNightNo", TelNightNo);
        cmd.Parameters.AddWithValue("@TelNightExt", TelNightExt);
        cmd.Parameters.AddWithValue("@ResAddr", ResAddr);
        cmd.Parameters.AddWithValue("@ConAddr", ConAddr);
        cmd.Parameters.AddWithValue("@BirthMulti", BirthMulti);
        cmd.Parameters.AddWithValue("@ResNei", ResNei);
        cmd.Parameters.AddWithValue("@ConNei", ConNei);
        cmd.Parameters.AddWithValue("@BirthPlaceOther", BirthPlaceOther);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        if (dt.Rows.Count > 0) RowCount = Convert.ToInt32(dt.Rows[0][0]);

        return RowCount;
    }


    /// <summary>
    /// 簡易更新 提供衛生所使用
    /// </summary>
    /// <returns></returns>
    public int SimpleUpdate()
    {
        int RowCount = 0;
         

        SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xModifyCaseUserSimple");
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        cmd.Parameters.AddWithValue("@ONationality", ONationality);
        cmd.Parameters.AddWithValue("@Language", String.Join(",", Language));
        cmd.Parameters.AddWithValue("@ConCounty", ConCounty);
        cmd.Parameters.AddWithValue("@ConTown", ConTown);
        cmd.Parameters.AddWithValue("@ConVillage", ConVillage); 
        cmd.Parameters.AddWithValue("@ConNei", ConNei);
        cmd.Parameters.AddWithValue("@ElemSchool", ElemSchool);
        cmd.Parameters.AddWithValue("@Occupation", Occupation);
        cmd.Parameters.AddWithValue("@ModifyUserID", AuthServer.GetLoginUser().ID);
        cmd.Parameters.AddWithValue("@TelDayArea", TelDayArea);
        cmd.Parameters.AddWithValue("@TelDayNo", TelDayNo);
        cmd.Parameters.AddWithValue("@TelDayExt", TelDayExt);
        cmd.Parameters.AddWithValue("@TelNightArea", TelNightArea);
        cmd.Parameters.AddWithValue("@TelNightNo", TelNightNo);
        cmd.Parameters.AddWithValue("@TelNightExt", TelNightExt);
           
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        if (dt.Rows.Count > 0) RowCount = Convert.ToInt32(dt.Rows[0][0]);

        return RowCount;
    }
}


public class UserMobile 
{
    public int ID {get;set;}
    public string Mobile { get; set; }
    public UserMobile()
    {
        ID = 0;
        Mobile = "";
    }
    public UserMobile(int id, string mobile)
    {
        ID = id;
        Mobile = mobile;
    }
    public void Update(int id, string mobile)
    {
        DBUtil.DBOp("ConnDB"
            , " Update  [C_CaseUserMobile] set  [MobileNo]={1},ModifyUserID ={2},ModifyDate =getdate()  where MobileID={0}  "
            , new string[] { id.ToString(), mobile, AuthServer.GetLoginUser().ID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteNonQuery);
         
    }
    public void Delete(int id)
    {
        DBUtil.DBOp("ConnDB"
            , " Update  [C_CaseUserMobile] set   LogicDel=1 ,ModifyUserID ={1},ModifyDate =getdate()  where MobileID={0}  "
            , new string[] { id.ToString(), AuthServer.GetLoginUser().ID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteNonQuery);
         
    }
    /// <summary>
    /// 可一次多筆 mobiles用逗號分開
    /// </summary>
    /// <param name="caseid"></param>
    /// <param name="mobiles"></param>
    public void Add(int caseid, string mobiles)
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xAddCaseUserMobile {0},{1},{2} "
             , new string[] { caseid.ToString(), AuthServer.GetLoginUser().ID.ToString(), mobiles }, NSDBUtil.CmdOpType.ExecuteNonQuery);

    }
}
public class UserEmail
{
    public int ID { get; set; }
    public string Email { get; set; }
    public UserEmail()
    {
        ID = 0;
        Email = "";
    }
    public UserEmail(int id, string email)
    {
        ID = id;
        Email = email;
    }
    public void Update(int id, string email)
    {
        DBUtil.DBOp("ConnDB"
            , " Update  [C_CaseUserEmail] set  [Email]={1} ,ModifyUserID ={2},ModifyDate =getdate() where EmailID={0}  "
            , new string[] { id.ToString(), email, AuthServer.GetLoginUser().ID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteNonQuery);

    }
    public void Delete(int id )
    {
        DBUtil.DBOp("ConnDB"
            , " Update  [C_CaseUserEmail] set   LogicDel=1 ,ModifyUserID ={1},ModifyDate =getdate()  where EmailID={0}  "
            , new string[] { id.ToString(), AuthServer.GetLoginUser().ID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteNonQuery);

    }
    /// <summary>
    /// 可一次多筆 emails用逗號分開
    /// </summary>
    /// <param name="caseid"></param>
    /// <param name="emails"></param>
    public void Add(int caseid, string emails)
    {
        DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xAddCaseUserEmail {0},{1},{2} "
            , new string[] { caseid.ToString(),  AuthServer.GetLoginUser().ID.ToString() ,emails}, NSDBUtil.CmdOpType.ExecuteNonQuery);
      
    }
}