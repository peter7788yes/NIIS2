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

public partial class CaseMaintain_ParentChildListOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        int pgNow;
        int pgSize; 

        string CaseName;
        string CaseIdNo;
        string SearchReason;
        int IsSearch = 0;
        string SearchConditions = "";
        int SearcResultCount = 0; 


        CaseIdNo = Request.Form["CaseIdNo"] ?? ""; 

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["IsSearch"], out IsSearch);
        SearchReason = Request.Form["SearchReason"] ?? "";

        int SearchKind = 4;

        DataSet ds = new DataSet();
        List<UserChildListVM> list = new List<UserChildListVM>();
        PageVM rtn = new PageVM();
        try
        {
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetUserChildList", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                    cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize); 
                    cmd.Parameters.AddWithValue("@CaseIdNo", CaseIdNo);  
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }

            SearcResultCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            EntityS.FillModel(list, ds.Tables[0]);
            EntityS.FillModel(rtn, ds.Tables[1]);
        }
        catch
        {
        }
        finally
        {
            //記下查詢紀錄
            if (IsSearch == 1)
            { 
                if (CaseIdNo != "") SearchConditions += "身份證號：" + CaseIdNo;

                Session["SearchID"] = Convert.ToInt32(DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xAddSearchLog] {0}, {1}, {2} ,{3} ,{4} ", new string[] { AuthServer.GetLoginUser().ID.ToString(), SearchConditions, SearchReason, SearcResultCount.ToString(), SearchKind.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
  
            }
        
        }
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}