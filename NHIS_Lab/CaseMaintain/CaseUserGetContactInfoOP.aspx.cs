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
using System.Text;

public partial class CaseMaintain_CaseUserGetContactInfoOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
           
        int CaseID = 0;
        int.TryParse(Request["CaseID"], out CaseID);
        int ContactCaseID = 0;
        int.TryParse(Request["ContactCaseID"], out ContactCaseID);
        StringBuilder sb = new StringBuilder("");

        JsonReply r = new JsonReply();
       

            SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetCaseUserContact");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CaseID", CaseID);
            cmd.Parameters.AddWithValue("@ContactCaseID", ContactCaseID);
            r = new JsonReply();

            DataTable dt = DB.GetDataTable(cmd, "ConnDB");
            if (dt.Rows.Count > 0)
            {
                    sb.Append("<table>");
                    sb.AppendFormat("<tr><td style='width:90px'>{0}</td><td>{1}</td></tr>", "電話(日)：", dt.Rows[0]["TelDayArea"].ToString() + ' ' + dt.Rows[0]["TelDayNo"].ToString() + "分機" + dt.Rows[0]["TelDayExt"].ToString());
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電話(夜)：", dt.Rows[0]["TelNightArea"].ToString() + ' ' + dt.Rows[0]["TelNightNo"].ToString() + "分機" + dt.Rows[0]["TelNightExt"].ToString());

                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "行動電話：", BindMobile(ContactCaseID));
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電子郵件：", BindEmail(ContactCaseID));

                   sb.Append("</table>");
            }

            r.RetCode = 1;
            r.Content = sb.ToString();
       
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        Response.End();
    }

    protected string BindMobile(int ContactCaseID)
    { 
        SqlCommand cmd = new SqlCommand("SELECT MobileID,[MobileNo]  FROM [C_CaseUserMobile] where [LogicDel]=0 and [CaseID]=@CaseID order by [MobileID] ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");
        StringBuilder sb = new StringBuilder("<div id=\"MobileDetail\">");
        foreach (DataRow r in dt.Rows)
        {
            sb.AppendFormat ( "<div class=\"Mobile\">{0}</div>", r["MobileNo"] ); 
        }
        sb.Append("</div>");

        return sb.ToString ();


    }
    protected string BindEmail(int ContactCaseID)
    {
        string CommentAreaFormat = "<div class=\"Email\">{0}</div>";

        SqlCommand cmd = new SqlCommand("SELECT [EmailID],[Email]  FROM [C_CaseUserEmail] where [LogicDel]=0 and [CaseID]=@CaseID order by [EmailID] ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");
        StringBuilder sb = new StringBuilder("<div id=\"MobileDetail\">");
        foreach (DataRow r in dt.Rows)
        {
            sb.AppendFormat(CommentAreaFormat, r["Email"]);   
        }
        sb.Append("</div>");
        return sb.ToString();


    }
    public static Control GetControlFromTag(string controlTag)
    {
        Page p = new Page();
        p.AppRelativeVirtualPath = "~/";
        Control control = p.ParseControl(controlTag);
        return control;
    }
}