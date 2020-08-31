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

public partial class System_FrequentlyAskedQuestionM_QnAData_QnADataOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_FrequentlyAskedQuestionM_QnAData_QnADataOP()
    {
        SearchPower = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        int pgNow;
        int pgSize;
        int QuestionType = 0;
        string Question = "";
        string PublishedStarDate = "";
        string PublishedEndDate = "";
        int PublishedStatus = 0;

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        if (SearchPower.HasPower == true)
        {
            int.TryParse(Request.Form["QuestionType"], out QuestionType);
            Question = Request.Form["Question"];
            PublishedStarDate = Request.Form["PublishedStarDate"];
            PublishedEndDate = Request.Form["PublishedEndDate"];
            int.TryParse(Request.Form["PublishedStatus"], out PublishedStatus);
            if (PublishedStarDate == "NaN")
            {
                PublishedStarDate = "";
            }
            if (PublishedEndDate == "NaN")
            {
                PublishedEndDate = "";
            }
        }
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAData_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@QaType", QuestionType);
                cmd.Parameters.AddWithValue("@Question", Question);
                cmd.Parameters.AddWithValue("@PublishedStarDate", PublishedStarDate);
                cmd.Parameters.AddWithValue("@PublishedEndDate", PublishedEndDate);
                cmd.Parameters.AddWithValue("@PublishedStatus", PublishedStatus);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<QnADataVM> list = new List<QnADataVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}