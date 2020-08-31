﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class SearchCheck_SearchLogDetailListOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        int pgNow;
        int pgSize;
        int OrgID = 0; 
        string SearchDateS;
        string SearchDateE;
        string UserName;  

        SearchDateS = Request.Form["SearchDateS"] ?? "";
        SearchDateE = Request.Form["SearchDateE"] ?? "";
        if (SearchDateS != "")
         {
             try
             {
                 SearchDateS = Convert.ToDateTime(SearchDateS).ToString("yyyy/MM/dd");
             }
             catch {
                 SearchDateS = "";
             }
         }
        if (SearchDateE != "")
         {
             try
             {
                 SearchDateE = Convert.ToDateTime(SearchDateE).ToString("yyyy/MM/dd");
             }
             catch
             {
                 SearchDateE = "";
             }
         }

        UserName = Request.Form["UserName"] ?? ""; 
      

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["OrgID"], out OrgID);

         
        //Response.Write("@SearchDateS="+ SearchDateS);
        //Response.Write("@SearchDateE=" + SearchDateE);
        //Response.Write("@UserName=" + UserName);
        //Response.Write("@OrgID=" + OrgID.ToString());
        //Response.End();

        List<SearchCheckListVM> list = new List<SearchCheckListVM>();
        PageVM rtn = new PageVM();
        DataSet ds = new DataSet();
         

        try
        {
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_SearchCheck_xGetSearchLogDetailList", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                    cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                    cmd.Parameters.AddWithValue("@SearchDateS", SearchDateS);
                    cmd.Parameters.AddWithValue("@SearchDateE", SearchDateE);
                    cmd.Parameters.AddWithValue("@UserName", UserName); 
                    cmd.Parameters.AddWithValue("@OrgID", OrgID); 
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }
             

            EntityS.FillModel(list, ds.Tables[0]);
            EntityS.FillModel(rtn, ds.Tables[1]);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message+ex.StackTrace );
        } 
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}