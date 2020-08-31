using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CaseMaintain_CaseRemarkContent : System.Web.UI.Page
{
    protected int CaseID = 0;
    protected int RemarkID = 0;
    protected string action = "";
    protected void Page_Load(object sender, EventArgs e)
    { 
    }

     protected void Page_Init(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            action = Request.Form["action"] ?? action;
            Session["RemarkAction"] = action;
            int.TryParse(Request.Form["CaseID"], out CaseID);
            Session["RemarkCaseID"] = CaseID;
            int.TryParse(Request.Form["RemarkID"], out RemarkID);
            Session["RemarkID"] = RemarkID;

            ddlRemarkType.Items.Add(new ListItem("請選擇", ""));
            SystemCodeControl.ServerSelect(ref ddlRemarkType, "CaseUser_RemarkType");


            if (RemarkID != 0)
            {
                BindRemark();
            }
            else
            {//defulat
                trFile.Attributes.Add("style", "display:none");
            }
             

        }
        else
        {
            if (Session["RemarkAction"] != null)
                action = Session["RemarkAction"].ToString();
             if (Session["RemarkCaseID"]!=null)
                int.TryParse(Session["RemarkCaseID"].ToString(), out CaseID);
             if (Session["RemarkID"] != null)
                 int.TryParse(Session["RemarkID"].ToString(), out RemarkID);
        }
          


    }
     protected void BindRemark()
     {
         DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
                                                      " SELECT [ID] ,[RemarkType]  ,CaseRemark,isnull([FileID],0) FileID FROM [dbo].[C_CaseUserRemark] where [LogicDel]=0  and  [ID]={0} ",
                                                       new string[] { RemarkID.ToString() }
                                                        , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

         if (dt.Rows.Count > 0)
         {
             ddlRemarkType.SelectedValue = dt.Rows[0]["RemarkType"].ToString();
             if (dt.Rows[0]["RemarkType"].ToString() == "3")
             {
                 trText.Attributes.Add("style", "display:none");
                 linkFiles.NavigateUrl = "DownloadFileOP.aspx?i=" + dt.Rows[0]["FileID"].ToString();
                 linkFiles.Text =  Server.HtmlEncode(dt.Rows[0]["CaseRemark"].ToString());
                // ltFiles.Text = "<a href=\"DownloadFileOP.aspx?i=" + dt.Rows[0]["FileID"].ToString() + "\">" + dt.Rows[0]["CaseRemark"].ToString() + "</a>";
             }
             else
             {
                
                 trFile.Attributes.Add("style", "display:none");  
                 tbRemarkContent.Text = Server.HtmlEncode(dt.Rows[0]["CaseRemark"].ToString());
             }
         
         }

     }
     protected void btnAdd_Click(object sender, EventArgs e)
     {
         UserRemark ur = new UserRemark();  
         ur.CaseID = CaseID;
         ur.RemarkContent = tbRemarkContent.Text;
         ur.RemarkType = ddlRemarkType.SelectedValue;

         if (ddlRemarkType.SelectedValue == "3")
         { 
             ur.FileID = NewFile();
             if (ur.FileID > 0) ur.RemarkContent = fuRemark.FileName;
         }
         if (action == "Add")
         {
            ur.CreatedUserID = AuthServer.GetLoginUser().ID;
             RemarkID= ur.Add();
         }
         else  
         {  //modify
               if (RemarkID > 0)
               {
                   ur.ModifyUserID = AuthServer.GetLoginUser().ID;
                   ur.RemarkID = RemarkID; 
                   ur.Update();
                }
         }
          
         string openerfun =  (action == "Add" ? "AddRemarkTr(" + RemarkID.ToString () + ")" : "ReloadRemarkList()"); 

         Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');window.opener." + openerfun + ";window.close();", true);
     }

     public int NewFile( )
     {
         int OutFileInfoID = 0;
         NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient(); 
         try
         { 
             if (fuRemark.HasFile)
             { 
                 OutFileInfoID = WS.UploadFile(11, fuRemark.PostedFile.ContentType, fuRemark.FileName.Split('.').Last(), fuRemark.FileName, AuthServer.GetLoginUser().ID, AuthServer.GetLoginUser().OrgID, fuRemark.FileBytes);
              }

         }
         catch (Exception ex)
         { 
         }
         return OutFileInfoID;
     }

}