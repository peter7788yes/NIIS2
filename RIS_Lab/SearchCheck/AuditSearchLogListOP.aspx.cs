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

public partial class SearchCheck_AuditSearchLogListOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        int pgNow;
        int pgSize;
        int UserID = 0;
        string SearchDateS = "";
        string SearchDateE = ""; 
        int SearchKind = 0; 
        SearchDateS = Request.Form["SearchDateS"] ?? "";
        if (SearchDateS != "")
        {
            try
            {
                SearchDateS = Convert.ToDateTime((Convert.ToInt32(SearchDateS.Split('-')[0]) + 1911).ToString() + "/" + SearchDateS.Split('-')[1] + "/01").ToString("yyyy/MM/dd");
            }
            catch
            {
                SearchDateS = "";
            }
        }
        if (SearchDateS != "")
        {
            try
            {
                SearchDateE = Convert.ToDateTime(SearchDateS).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");
            }
            catch
            {
                SearchDateE = "";
            }
        }

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["UserID"], out UserID);
        int.TryParse(Request.Form["SearchKind"], out SearchKind);
         
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
                using (SqlCommand cmd = new SqlCommand("dbo.usp_SearchCheck_xGetAuditList", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                    cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                    cmd.Parameters.AddWithValue("@SearchDateS", SearchDateS);
                    cmd.Parameters.AddWithValue("@SearchDateE", SearchDateE);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@SearchKind", SearchKind); 
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