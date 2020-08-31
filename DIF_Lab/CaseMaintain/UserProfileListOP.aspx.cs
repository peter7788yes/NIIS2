using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class CaseMaintain_UserProfileListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
        //base.DisableTop(true);
        int pgNow;
        int pgSize;
        int CountyID=0;
        int TownID=0; 
        string BirthDateS;
        string BirthDateE; 
        string CaseName;
        string CaseIdNo;
        string SearchReason;
        string SearchConditions="";
        int SearchKind = 1;
        int IsSearch = 0; 

        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? ""; 
         if (BirthDateS!="")
         {
             try
             {
                // BirthDateS = Convert.ToDateTime(BirthDateS).ToString("yyyyMMdd");
              BirthDateS=   TaiwanYear.ToDateTime(BirthDateS).ToString("yyyyMMdd");
             }
             catch {
                 BirthDateS = "";
             }
         }
         if (BirthDateE != "")
         {
             try
             {
                 //BirthDateE = Convert.ToDateTime(BirthDateE).ToString("yyyyMMdd");
                 BirthDateE = TaiwanYear.ToDateTime(BirthDateE).ToString("yyyyMMdd");
             }
             catch
             {
                 BirthDateE = "";
             }
         }

        CaseName = Request.Form["CaseName"] ?? "";
        CaseIdNo = Request.Form["CaseIdNo"] ?? "";
      

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["CountyID"], out CountyID);
        int.TryParse(Request.Form["TownID"], out TownID);
        int.TryParse(Request.Form["IsSearch"], out IsSearch);
        SearchReason = Request.Form["SearchReason"] ?? "";

        int.TryParse(Request.Form["SearchKind"], out SearchKind);

        List<UserProfileListVM> list = new List<UserProfileListVM>();
        PageVM rtn = new PageVM();
        DataSet ds = new DataSet();

        int SearcResultCount = 0; 

        try
        {
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetUserList", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                    cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                    cmd.Parameters.AddWithValue("@BirthDateS", BirthDateS);
                    cmd.Parameters.AddWithValue("@BirthDateE", BirthDateE);
                    cmd.Parameters.AddWithValue("@CaseName", CaseName);
                    cmd.Parameters.AddWithValue("@CaseIdNo", CaseIdNo);
                    cmd.Parameters.AddWithValue("@CountyID", CountyID);
                    cmd.Parameters.AddWithValue("@TownID", TownID);
                    //cmd.Parameters.AddWithValue("@IsSearch", IsSearch);
                    //cmd.Parameters.AddWithValue("@SearchReason", SearchReason); 

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }

            SearcResultCount= Convert.ToInt32(ds.Tables[1].Rows[0][0]);
           // SearcResultCount = 0;

            EntityS.FillModel(list, ds.Tables[0]);
            EntityS.FillModel(rtn, ds.Tables[1]);
        }
        catch
        {
        }
        finally { 
        //記下查詢紀錄

            if (IsSearch == 1)
            {
                if (CaseName != "") SearchConditions += "姓名：" + CaseName;
                if (CaseIdNo != "") SearchConditions += "身份證號：" + CaseIdNo;
                if (BirthDateS != "") SearchConditions += "生日起日：" + BirthDateS;
                if (BirthDateE != "") SearchConditions += "生日迄日：" + BirthDateE;
                if (CountyID != 0) SearchConditions += "戶籍縣市：" + SystemAreaCode.GetName(CountyID);
                if (TownID != 0) SearchConditions += "戶籍鄉鎮：" + SystemAreaCode.GetName(TownID);

                Session["SearchID"] = Convert.ToInt32(DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xAddSearchLog] {0}, {1}, {2} ,{3} ,{4}  ", new string[] { AuthServer.GetLoginUser().ID.ToString(), SearchConditions, SearchReason, SearcResultCount.ToString(), SearchKind.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));

            } 

        }
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}