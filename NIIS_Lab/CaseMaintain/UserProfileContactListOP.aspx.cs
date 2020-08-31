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

public partial class CaseMaintain_UserProfileContactListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.Form["action"] ?? "";
        JsonReply jr = new JsonReply();
        try
        {
            if (action == "GetContactTr")
            {
                int ContactID;
                int.TryParse(Request.Form["ContactID"], out ContactID);

                StringBuilder sb = new StringBuilder("");
                UserContact uc = new UserContact(ContactID);
                CaseUserProfile c = new CaseUserProfile(uc.ContactCaseID);

                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><a href=\"javascript:void(0);\" id=\"ModifyContact_{5}\" class=\"ModifyContact\" ><img src=\"/images/icon_maintain.png\"></a></td><td> <a  href=\"javascript:void(0);\"  id=\"DeleteContact_{5}\" class=\"DeleteContact\"><img src=\"/images/icon_del01.gif\"></a></td></tr>", uc.RelationShipName, c.ChName, c.IdNo, c.BirthDate, (uc.IsMain ? "<img src=\"/images/icon_tick.png\" >" : ""), uc.ContactID);

                jr.Content = sb.ToString();
                jr.RetCode = 1;
            }
            else if (action == "Delete")
            {
                int ContactID;
                int.TryParse(Request.Form["ContactID"], out ContactID);
                UserContact uc = new UserContact(ContactID);
                uc.Delete();
                jr.Content = "成功";
                jr.RetCode = 1;
            }
            else if (action == "LoadContactList")
            {
                int CaseID;
                int.TryParse(Request.Form["CaseID"], out CaseID);
                StringBuilder sb = new StringBuilder("");
                sb.Append("<table id=\"Contact_TB\">");
                sb.Append("<tr><th scope=\"col\">關係</th><th scope=\"col\">姓名</th><th scope=\"col\">身分證號</th><th scope=\"col\">出生日期</th><th scope=\"col\">主要聯絡人</th><th scope=\"col\"  style=\"width:1%\">維護</th><th scope=\"col\" style=\"width:1%\">移除</th></tr>");

                DataTable dt = new DataTable();
                if (CaseID != 0)
                {
                 dt= (DataTable)DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xGetCaseUserContactList {0} "
                        , new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

               }
                else
                {
                    //if (Session["NewCaseContacts"] != null)
                    //{
                    //    string NewCaseContacts = Session["NewCaseContacts"].ToString();
                    //    dt = (DataTable)DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xGetCaseUserContactList {0} "
                    // , new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

                    //}
                }
                  foreach (DataRow r in dt.Rows)  sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><a href=\"javascript:void(0);\" id=\"ModifyContact_{5}\" class=\"ModifyContact\" ><img src=\"/images/icon_maintain.png\"></a></td><td> <a  href=\"javascript:void(0);\"  id=\"DeleteContact_{5}\" class=\"DeleteContact\"><img src=\"/images/icon_del01.gif\"></a></td></tr>", r["RS"], r["ChName"], r["IdNo"], r["BirthDate"], (r["IsMain"].ToString() == "1" ? "<img src=\"/images/icon_tick.png\" >" : ""), r["ContactID"]);
             
                sb.Append("</table>");
                jr.Content = sb.ToString();
                jr.RetCode = 1;

            }
            else if (action == "isHaveParent")
            {
                jr.Content = "無父母";
                jr.RetCode = 0;

                int CaseID;
                int.TryParse(Request.Form["CaseID"], out CaseID);
                if (Convert.ToInt32(DBUtil.DBOp("ConnDB", " SELECT  count([ContactID])  FROM [dbo].[C_CaseUserContact] where [CaseID]={0} and ([ContactRelationShip]=2 or [ContactRelationShip]=3) and LogicDel=0 "
                        , new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar)) > 0)
                {
                    jr.Content = "有父或母";
                    jr.RetCode = 1;
                } 
                 
                 
            }
            else if (action == "IsHaveParentWithContactIDs")
            {
                jr.Content = "無父母";
                jr.RetCode = 0;
                string ContactIDs = Request.Form["ContactIDs"] ?? "";
                 
                if (ContactIDs != "" && Convert.ToInt32(DBUtil.DBOp("ConnDB", " SELECT  count([ContactID])  FROM [dbo].[C_CaseUserContact]   where exists (  select data from dbo.fn_slip_str({0},',') where data = [ContactID]) and ([ContactRelationShip]=2 or [ContactRelationShip]=3) and LogicDel=0 "
                            , new string[] { ContactIDs }, NSDBUtil.CmdOpType.ExecuteScalar)) > 0)
                    {
                        jr.Content = "有父或母";
                        jr.RetCode = 1;
                    }  
                

            }
        }
        catch {
            jr.RetCode = 0;
        }

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jr));
        Response.End();
    }
}