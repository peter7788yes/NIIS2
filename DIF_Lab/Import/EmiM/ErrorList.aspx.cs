using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Import_EmiM_ErrorList : BasePage
{
    public int MasterID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        MasterID = GetNumber<int>("id", MyHttpMethod.GET);
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_EmiLog_xGetErrorList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MasterID", MasterID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        Response.Clear();
        Response.AppendHeader("Accept-Language", "zh-tw");
        Response.AddHeader("content-disposition", "attachment;filename=\"ErrorList.txt\"");
        Response.ContentType = "application/vnd.Text";
        Response.Write("異常資料清單" + Environment.NewLine);
        Response.Write("序號,身分證號,出生日期,出境日期,姓名" + Environment.NewLine);
        if (dt != null && dt.Rows.Count > 0)
        {
            for(int i=0;i<dt.Rows.Count;i++)
            {
                Response.Write(dt.Rows[i]["Seq"].ToString() + "," + dt.Rows[i]["CaseID"].ToString() + "," + dt.Rows[i]["Birthday"].ToString() + "," + dt.Rows[i]["EmiDate"].ToString() + "," + dt.Rows[i]["UserName"].ToString() + Environment.NewLine);
            }
        }
        Response.End();
    }
}