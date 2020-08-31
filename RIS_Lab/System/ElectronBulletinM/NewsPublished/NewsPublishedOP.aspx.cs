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

public partial class System_NewsPublishedM_NewsPublishedOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_NewsPublishedM_NewsPublishedOP()
    {
        SearchPower = base.AddPower("/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        UserVM user = AuthServer.GetLoginUser();
        int pgNow;
        int pgSize;
        int OrgID = 0;
        string Subject = "";
        int Status = 0;
        
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        if (SearchPower.HasPower == true)
        {
            int.TryParse(Request.Form["OrgID"], out OrgID);
            Subject = Request.Form["Subject"];
            int.TryParse(Request.Form["Status"], out Status);
            
            if (OrgID == -1)
            {
                OrgID = user.OrgID;
            }
        }

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@Subject", Subject);
                cmd.Parameters.AddWithValue("@Status", Status);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<NewsPublishedVM> list = new List<NewsPublishedVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}