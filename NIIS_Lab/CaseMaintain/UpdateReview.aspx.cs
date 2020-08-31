using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Linq.Expressions;
using System.Dynamic;
using System.Reflection;

public partial class CaseMaintain_UpdateReview : System.Web.UI.Page
{
    protected  CaseUserProfile m =new CaseUserProfile();
    protected   CaseUserProfile c =new CaseUserProfile();
     List<CheckFieldVM> CheckFieldVMList= new List<CheckFieldVM>();
    protected void Page_Load(object sender, EventArgs e)
    {
        //這些欄位需要上傳file
        
          CompareCaseUser ();
         
        if (!Page.IsPostBack )
        {
          StringBuilder sb = new StringBuilder("<table><caption>您修改的以下欄位資料，需檢附證明文件</caption><tr><th>修改欄位</th><th>修改前內容</th><th>修改後內容</th><th>檢附文件</th><th>上傳文件</th></tr>");

        string UpdateFieldTableFormat = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><input type=\"file\" name=\"fu_{4}\"   multiple  id=\"fu_{4}\"></td></tr>";
        foreach (CheckFieldVM cf in CheckFieldVMList)
            sb.AppendFormat(UpdateFieldTableFormat, cf.FieldDiscription,  cf.ViewBefore,  cf.ViewAfter,  cf.FileCheck,  cf.ID);
          
        sb.Append("</table>");
        ltUpdateFields.Text = sb.ToString();

           BindCaseData(m.CaseID);
        }
         

    }

    protected void CompareCaseUser ()
    {
        string Col = "";

        if (Session["ModifiedCaseToCheck"] != null)
        {
            //欲修改資料
            m = (CaseUserProfile)Session["ModifiedCaseToCheck"];
            //原資料 
            c = new CaseUserProfile(m.CaseID);

        
            foreach (PropertyInfo prop in c.GetType().GetProperties())
            {
                string ValBefore = Convert.ToString(GetProperty(c, prop.Name));
                string ValAfter = Convert.ToString(GetProperty(m, prop.Name));
                if (ValBefore != ValAfter) Col += prop.Name + ",";
                //取得有變動的屬性
            }
            #region MyRegion


            //if (m.BirthDate != c.BirthDate) Col += GetPropertyName(() => c.BirthDate) + ",";
            //if (m.IdNo != c.IdNo) Col += GetPropertyName(() => c.IdNo) + ",";
            //if (m.PassportNo != c.PassportNo) Col += GetPropertyName(() => c.PassportNo) + ",";
            //if (m.ResNo != c.ResNo) Col += GetPropertyName(() => c.ResNo) + ",";
            //if (m.ChName != c.ChName) Col += GetPropertyName(() => c.ChName) + ",";
            //if (m.Gender != c.Gender) Col += GetPropertyName(() => c.Gender) + ",";
            //if (m.HouseNo != c.HouseNo) Col += GetPropertyName(() => c.HouseNo) + ",";
            //if (m.ResCounty != c.ResCounty) Col += GetPropertyName(() => c.ResCounty) + ",";
            //if (m.ResTown != c.ResTown) Col += GetPropertyName(() => c.ResTown) + ",";
            //if (m.PregWeek != c.PregWeek) Col += GetPropertyName(() => c.PregWeek) + ",";
            //if (m.BirthNum != c.BirthNum) Col += GetPropertyName(() => c.BirthNum) + ",";
            //if (m.BirthSeq != c.BirthSeq) Col += GetPropertyName(() => c.BirthSeq) + ",";
            //if (m.BirthWeight != c.BirthWeight) Col += GetPropertyName(() => c.BirthWeight) + ",";
            //if (m.BirthPlace != c.BirthPlace) Col += GetPropertyName(() => c.BirthPlace) + ",";
            //if (m.Deliver != c.Deliver) Col += GetPropertyName(() => c.Deliver) + ",";
            //if (m.DeliverOrg != c.DeliverOrg) Col += GetPropertyName(() => c.DeliverOrg) + ",";
            //if (m.MarryStatus != c.MarryStatus) Col += GetPropertyName(() => c.MarryStatus) + ",";
            //if (m.ResVillage != c.ResVillage) Col += GetPropertyName(() => c.ResVillage) + ",";
            //if (m.ResAddr != c.ResAddr) Col += GetPropertyName(() => c.ResAddr) + ",";
            //if (m.ResNei != (c.ResNei == "0" ? "" : c.ResNei)) Col += GetPropertyName(() => c.ResNei) + ",";
            //if (m.EnName != c.EnName) Col += GetPropertyName(() => c.EnName) + ",";
            //if (m.OtherNo != c.OtherNo) Col += GetPropertyName(() => c.OtherNo) + ",";
            #endregion
            //這些欄位需要上傳file  


            //再去看哪些需要 uploadfile



            if (Col != "")
            {
                #region 再去看哪些需要 uploadfile
                DataTable dt = (DataTable)DBUtil.DBOp("ConnDB"
                          , " SELECT NiisFieldName ,  ID, FieldDiscription, FileToCheck,SystemCodeKey  FROM   C_CaseCheck_FieldCheck where NiisFieldName in (select data from dbo.fn_slip_str({0},',') ) "
                          , new string[] { Col }
                          , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string ViewBefore = Convert.ToString(GetProperty(c, r["NiisFieldName"].ToString()));
                        string ViewAfter = Convert.ToString(GetProperty(m, r["NiisFieldName"].ToString()));
                        string ValBefore = Convert.ToString(GetProperty(c, r["NiisFieldName"].ToString()));
                        string ValAfter = Convert.ToString(GetProperty(m, r["NiisFieldName"].ToString()));
                        string SystemCodeKey = r["SystemCodeKey"].ToString();

                        if (SystemCodeKey != "")
                        {
                            if (SystemCodeKey.Contains("County") || SystemCodeKey.Contains("Town") || SystemCodeKey.Contains("Village"))
                            {
                                ViewBefore = SystemAreaCode.GetName(Convert.ToInt32(Convert.ToString(GetProperty(c, r["NiisFieldName"].ToString()))));
                                ViewAfter = SystemAreaCode.GetName(Convert.ToInt32(Convert.ToString(GetProperty(m, r["NiisFieldName"].ToString()))));

                            }
                            else
                            {
                                int iViewBefore = 0;
                                int.TryParse(Convert.ToString(GetProperty(c, r["NiisFieldName"].ToString())) ,out iViewBefore);
                                int iViewAfter = 0;
                                int.TryParse(Convert.ToString(GetProperty(m, r["NiisFieldName"].ToString())), out iViewAfter);

                                ViewBefore = SystemCode.GetName(r["SystemCodeKey"].ToString(), iViewBefore);
                                ViewAfter = SystemCode.GetName(r["SystemCodeKey"].ToString(), iViewAfter);
                            }
                        }

                        CheckFieldVM cf = new CheckFieldVM();
                        cf.ID = Convert.ToInt32(r["ID"]);
                        cf.FieldName = r["NiisFieldName"].ToString();
                        cf.FieldDiscription = r["FieldDiscription"].ToString();
                        cf.ValBefore = ValBefore;
                        cf.ValAfter = ValAfter;
                        cf.ViewBefore = ViewBefore;
                        cf.ViewAfter = ViewAfter;
                        cf.FileCheck = r["FileToCheck"].ToString();
                        CheckFieldVMList.Add(cf);
                        //變成一個list 之後好操作

                    }


                }
                #endregion
            } 
        
        }


        if (Col == "" || CheckFieldVMList.Count ==0) Response.Redirect("UserProfileList.aspx");
    
    }



    public static object GetProperty(object myObject, string member)
    { 
        return  myObject.GetType().GetProperty(member).GetValue(myObject, null);
    }


    public static string GetPropertyName<t>(Expression<Func<t>> PropertyExp)
    {
        return (PropertyExp.Body as MemberExpression).Member.Name;
    }
 

    protected void BindCaseData(int caseid)
    {
        CaseUserProfile c = new CaseUserProfile(caseid);
        if (c.CaseID > 0)
        {
            ltName.Text = c.ChName;
            ltIdNo.Text = c.IdNo;
            ltResAddr.Text = c.ResFullAddress;
        }

    } 

    protected void HandelFiles()
    {
        UserVM user = AuthServer.GetLoginUser();
        NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient();

        foreach (CheckFieldVM cf in CheckFieldVMList)
        {
            try
            {   //fu_ID 
                HttpPostedFile uploadedFile = Request.Files["fu_" + cf.ID];



                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    BinaryReader b = new BinaryReader(uploadedFile.InputStream);
                    int OutFileInfoID = 0;

                    OutFileInfoID = WS.UploadFile(11, uploadedFile.ContentType, uploadedFile.FileName.Split('.').Last(), uploadedFile.FileName, user.ID, user.OrgID, b.ReadBytes(uploadedFile.ContentLength));

                    if (OutFileInfoID > 0) cf.FileID = OutFileInfoID ;

                }

            }
            catch (Exception ex)
            {

               // Response.Write(ex.Message + ex.StackTrace);
                Response.End();
            }



        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        HandelFiles();

        //把上傳檔案的再insert db

        List<CheckFieldVM> WithFileList = new List<CheckFieldVM>();
        WithFileList = CheckFieldVMList.Where(vm => vm.FileID != 0).ToList();

        UserVM u = AuthServer.GetLoginUser();

        if (WithFileList.Count > 0)
        {

            int ApplyID = Convert.ToInt32(
               DBUtil.DBOp("ConnDB"
                   , @" INSERT INTO [dbo].[C_CaseCheck_ModifyApply] ([CreateUserID] ,[CreateOrgID],[ModifyCase],[CaseIdNo],[CaseName],NowLevel) VALUES ( {0},{1},{2},{3},{4},1  ) ;select @@identity "
                   , new string[] { u.ID.ToString(), u.OrgID.ToString(), c.CaseID.ToString(), c.IdNo, c.ChName }
                   , NSDBUtil.CmdOpType.ExecuteScalar));



            if (ApplyID > 0)
            {
                foreach (CheckFieldVM cf in WithFileList)
                {
                    DBUtil.DBOp("ConnDB"
                      , @" INSERT INTO [dbo].[C_CaseCheck_ModifyApplyDetail] ([ApplyID],[FieldName] ,[FieldDiscription] ,[FileCheck],[CheckLevel],[ViewBefore] ,[ViewAfter] ,[ValBefore] ,[ValAfter] ,[FileID],NowLevel)
                                    SELECT {1}, [NiisFieldName] ,[FieldDiscription] ,[FileToCheck] ,[CheckLevel]   ,{2},{3},{4},{5},{6} ,1  FROM [dbo].[C_CaseCheck_FieldCheck] where ID={0} "
                      , new string[] { cf.ID.ToString(), ApplyID.ToString(), cf.ViewBefore, cf.ViewAfter, cf.ValBefore, cf.ValAfter, cf.FileID.ToString() }
                      , NSDBUtil.CmdOpType.ExecuteNonQuery);
                }
            }



            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "location.href='UserProfile.aspx?i=" + iCaseID.ToString () + "'", true);
        }
        Session["ModifiedCaseToCheck"] = null; //清除
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "location.href='UserProfileList.aspx'", true);
    }

    }
 