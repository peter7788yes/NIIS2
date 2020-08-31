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

public partial class LogManage_LogMainListOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        int pgNow;
        int pgSize;
        int UrgeType=0;
        int UrgeStatus=0;
        string CreateDateS;
        string CreateDateE;


        CreateDateS = Request.Form["CreateDateS"] ?? "";
        CreateDateE = Request.Form["CreateDateE"] ?? "";
        if (CreateDateS != "") CreateDateS = (Convert.ToInt32(CreateDateS.Substring(0, 3)) + 1911).ToString() + "/" + CreateDateS.Substring(3, 2) + "/" + CreateDateS.Substring(5, 2);
        if (CreateDateE != "") CreateDateE = (Convert.ToInt32(CreateDateE.Substring(0, 3)) + 1911).ToString() + "/" + CreateDateE.Substring(3, 2) + "/" + CreateDateE.Substring(5, 2);
 

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["UrgeType"], out UrgeType);
        int.TryParse(Request.Form["UrgeStatus"], out UrgeStatus);


        SystemCode.Update();
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUrge_xGetUrgeSettingList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                cmd.Parameters.AddWithValue("@CreateDateS", CreateDateS );
                cmd.Parameters.AddWithValue("@CreateDateE", CreateDateE);
                cmd.Parameters.AddWithValue("@UrgeType", UrgeType);
                cmd.Parameters.AddWithValue("@UrgeStatus", UrgeStatus); 
                  
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<CaseUrgeSettingListVM> list = new List<CaseUrgeSettingListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}