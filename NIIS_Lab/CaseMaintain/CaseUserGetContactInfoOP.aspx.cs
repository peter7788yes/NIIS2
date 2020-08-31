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

        int ContactID = 0;
        int.TryParse(Request["ContactID"], out ContactID);
        StringBuilder sb = new StringBuilder("");

        JsonReply r = new JsonReply();
        
         int ContactCaseID = 0;
        int CaseID = 0;
         

            //SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetCaseUserContact");
            //cmd.CommandType = CommandType.StoredProcedure; 
            //cmd.Parameters.AddWithValue("@ContactID", ContactID);
           
            //DataTable dt = DB.GetDataTable(cmd, "ConnDB");
            //if (dt.Rows.Count > 0)
            //{
            //        sb.Append("<table>");
            //        sb.AppendFormat("<tr><td style='width:90px'>{0}</td><td>{1}</td></tr>", "電話(日)：", dt.Rows[0]["TelDayArea"].ToString() + ' ' + dt.Rows[0]["TelDayNo"].ToString() + "分機" + dt.Rows[0]["TelDayExt"].ToString());
            //        sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電話(夜)：", dt.Rows[0]["TelNightArea"].ToString() + ' ' + dt.Rows[0]["TelNightNo"].ToString() + "分機" + dt.Rows[0]["TelNightExt"].ToString());

            //        sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "行動電話：", BindMobile(ContactCaseID));
            //        sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電子郵件：", BindEmail(ContactCaseID));

            //       sb.Append("</table>");
            //}
 r = new JsonReply();


 if (ContactID != 0)
 {

     UserContact uc = new UserContact(ContactID);
     ContactCaseID = uc.ContactCaseID;
     CaseID = uc.CaseID;
 }

 CaseUserProfile c = new CaseUserProfile(ContactCaseID);
 if (c.CaseID != 0)
 {

     sb.Append("<table>");
     sb.AppendFormat("<tr><td style='width:90px'>{0}</td><td>{1}</td></tr>", "姓名：", c.ChName);

     sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電話(日)：", c.TelDayArea + ' ' + c.TelDayNo + (c.TelDayExt!="" ?  "分機" + c.TelDayExt :"" ));

     sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "電話(夜)：", c.TelNightArea + ' ' + c.TelNightNo + (c.TelNightExt != "" ? "分機" + c.TelNightExt : "")  );

     sb.AppendFormat("<tr><td>{0}</td><td style='white-space: pre-wrap;'>{1}</td></tr>", "行動電話：", String.Join("\n", c.Mobiles.Select(m => m.Mobile).ToArray()));
     sb.AppendFormat("<tr><td>{0}</td><td style='white-space: pre-wrap;'>{1}</td></tr>", "電子郵件：", String.Join("\n", c.Emails.Select(m => m.Email).ToArray()));

     sb.Append("</table>");

     //ltBirthDate.Text = c.BirthDate;
     //ltIdNo.Text = c.IdNo;
     //ltName.Text = c.ChName;

     ;


 }

            r.RetCode = 1;
            r.Content = sb.ToString();
       
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        Response.End();
    }

 
}