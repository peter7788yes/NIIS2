using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Text;

public partial class CaseVisit_VisitCaseContent : BasePage
{   public List<MyPowerVM> PowerList = new List<MyPowerVM>(); 
    public bool bAdd = false;
    public bool bEdit = false;
    public bool bDelete= false; 
    public CaseVisit_VisitCaseContent()
    {
        PowerList = base.AddPower("/CaseVisit/VisitProfileList.aspx"
                , MyPowerEnum.修改
                , MyPowerEnum.新增
                , MyPowerEnum.刪除
                , MyPowerEnum.瀏覽
                , MyPowerEnum.查詢
     );
           
    }
    int VisitID = 0; 
    int CaseID = 0;
    public string VisitReasonSelectVal = "";
    public string VisitResultSelectVal = "";
    public string VisitResultAry = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {


        bAdd = base.GetPower(PowerList[1]).HasPower;
        bEdit = base.GetPower(PowerList[0]).HasPower;
        bDelete = base.GetPower(PowerList[2]).HasPower;
        QS();
      
        if (!Page.IsPostBack)
        {
            if (VisitID!=0)
                   BindData();


            SimpleCaseData();

            ViewState["Generated"] = true;
        }

        if (VisitID == 0)
        {
           if (bAdd)
            btnSave.Visible = true;

        }
        else
        {
            //修改
            if (bEdit)
                btnSave.Visible = true;

            if (bDelete)
               btnDel.Visible = true;
        }


         
    }
    protected void SimpleCaseData( )
    {

        CaseUserProfile u = new CaseUserProfile(CaseID);
        if (u.CaseID > 0)
        {
            ltIdNo.Text = u.IdNo;
            ltBirthDate.Text = u.BirthDate;
            ltName.Text = u.ChName;
            ltGender.Text = u.GenderName;

        }
         

    }
    protected void BindData()
    {
        bool CanEdit =false;

 
           DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "  exec dbo.usp_CaseVist_xGetVisit {0}", new string[] { VisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
      
         if (dt.Rows.Count > 0)
         {
             tbVisitDate.Text = dt.Rows[0]["VisitDate"].ToString ();
             ddlVisitMan.SelectedValue = dt.Rows[0]["VisitAccount"].ToString();
             ddlVisitType.SelectedValue = dt.Rows[0]["VisitType"].ToString();
             ddlVisitReason.SelectedValue = dt.Rows[0]["VisitReason"].ToString();
             hdVac.Value = dt.Rows[0]["VacID"].ToString();
             ddlVisitReasonChange();
             ddlVisitResult.SelectedValue = dt.Rows[0]["VisitResult"].ToString();
              DataTable  dtd = (DataTable)DBUtil.DBOp("ConnDB", "SELECT       ID, CONVERT(varchar, VacID) + '-' + CONVERT(varchar, VacIDSeq) AS VacIDnSeq, VacID, VacIDSeq, VacCodeSeq, VisitResultDetailID,  dbo.fnToTaiwanDate([PrejectDate]) PrejectDate , VisitID   FROM  C_CaseVisitDetail where VisitID={0} and LogicDel=0  order by VacID", new string[] { VisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
              ddlVisitResultChange(dtd.Rows.OfType<DataRow>().Select(k => k["VacID"].ToString()).ToArray(), dtd); 
             tbVisitComment.Text = dt.Rows[0]["VisitComment"].ToString();
             tbKeyInMan.Text = dt.Rows[0]["CreateUserName"].ToString();
             tbVisitOrgName.Text = dt.Rows[0]["VisitOrgName"].ToString();
             CaseID = Convert.ToInt32(dt.Rows[0]["CaseID"]);
             tbVisitOrgName.Text = dt.Rows[0]["VisitOrgName"].ToString(); 
             tbVac.Text = dt.Rows[0]["VacCode"].ToString(); 
             ddlCountry.SelectedValue = dt.Rows[0]["AbroadCountry"].ToString();
             tbPreBackDate.Text = dt.Rows[0]["PreBackDate"].ToString();
             CreateModifyInfo.Visible = true;
             ltCreateInfo.Text = Server.HtmlEncode (dt.Rows[0]["CreateInfo"].ToString());
             ltModifyInfo.Text = Server.HtmlEncode (dt.Rows[0]["ModifyInfo"].ToString()); 
             CanEdit =(Convert.ToInt32(dt.Rows[0]["VisitOrg"]) == AuthServer.GetLoginUser().OrgID); 
             ShowFiles(VisitID, CanEdit);
         }

      




    }

    private void ShowFiles(int VisitID,bool bDownload)
    {
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT  isnull(C_CaseVisitFile.Filetype,0) Filetype,C_CaseVisitFile.ID,[FileID]  ,F_FileInfo.DisplayFileName FROM [dbo].[C_CaseVisitFile] inner join F_FileInfo on F_FileInfo.ID = [C_CaseVisitFile].FileID and [C_CaseVisitFile].VisitID={0} and C_CaseVisitFile.LogicDel=0", new string[] { VisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        foreach (DataRow r in dt.Rows)
        {
            string filetype = SystemCode.GetName("CaseVisit_VisitFileType", Convert.ToInt32(r["Filetype"]));

            if (bDownload)
                ltFiles.Text += string.Format("<div id=\"FileDIV_{2}\"><a href=\"DownloadFileOP.aspx?i={1}\">{3} : {0}</a> <img src=\"/images/icon_del01.gif\"  class=\"delFile\" id=\"img_del_{2}\" />;</div>", Server.HtmlEncode(r["DisplayFileName"].ToString()), Server.HtmlEncode(r["FileID"].ToString()), Server.HtmlEncode(r["ID"].ToString()),filetype);
            else
                ltFiles.Text += filetype +  ":" + Server.HtmlEncode(r["DisplayFileName"].ToString())+";";
        }

    }



    protected void QS()
    {
        int.TryParse(Request.QueryString["i"], out CaseID);
        int.TryParse(Request.QueryString["v"], out VisitID); 
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            tbVisitDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            VisitDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + tbVisitDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            tbVisitDate.Text = (DateTime.Now.Year - 1911).ToString() + DateTime.Now.ToString ("MMdd");

           
            tbPreBackDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            PreBackDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + tbPreBackDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
        
           
            ddlVisitType.Items.Clear();
            ddlVisitType.Items.Add(new ListItem("請選擇", ""));
            SystemCodeControl.ServerSelect(ref ddlVisitType, "CaseVisit_VisitType");
            ddlVisitReason.Items.Clear();
            ddlVisitReason.Items.Add(new ListItem("請選擇", ""));
            SystemCodeControl.ServerSelect(ref ddlVisitReason, "CaseVisit_VisitReason");
            ddlVisitResult.Items.Clear();
            ddlVisitResult.Items.Add(new ListItem("請選擇", "")); 
       
            ddlVisitFileType.Items.Clear();
            SystemCodeControl.ServerSelect(ref ddlVisitFileType, "CaseVisit_VisitFileType");
          
            ddlCountry.Items.Clear();
            ddlCountry.Items.Add(new ListItem("請選擇", ""));
            SystemCodeControl.ServerSelect(ref ddlCountry, "CaseUser_ONationality");


             UserVM u  = AuthServer.GetLoginUser(); 
            tbKeyInMan.Text = u.UserName;
            tbVisitOrgName.Text = u.OrgName;
            tbKeyInMan.Enabled = false;
            tbVisitOrgName.Enabled = false;

            
             DataTable dt = (DataTable)DBUtil.DBOp("ConnUser"
                , "SELECT [ID] , [UserName]   FROM [U_User] where  [OrgID] = {0} "
                , new string[] { u .OrgID.ToString ()}
                , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
            ddlVisitMan.Items.Clear();
            ddlVisitMan.Items.Add(new ListItem("請選擇", ""));
            foreach (DataRow r in dt.Rows) ddlVisitMan.Items.Add(new ListItem(r["UserName"].ToString(), r["ID"].ToString()));

            ddlVisitMan.SelectedValue = u.ID.ToString ();

        }
    }

  
     
    public static Control GetControlFromTag(string controlTag)
      {
          Page p = new Page();
          p.AppRelativeVirtualPath = "~/";
          Control control = p.ParseControl(controlTag);
          return control;
      }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        List<string> RequestKeys = Request.Form.AllKeys.Where(key => key.Contains("ddlVaccineCode_") || key.Contains("ddlNotNeedOrRejectReason_")  || key.Contains("tbPrDate_")  ).ToList();
  
        UserVM user = AuthServer.GetLoginUser(); 
        string VacIDs = hdVac.Value  ?? ""; 
        string[] Vac = VacIDs.Split (',') ; 
        string VisitResult = ddlVisitResult.SelectedValue;
        string spName = (VisitID != 0 ? "usp_CaseVisit_xEdit" : "usp_CaseVisit_xAdd");
        
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB",
            "exec  [dbo].[" + spName + "]  {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}   "
            , new string[] {
                TaiwanYear.ToDateString(tbVisitDate.Text)
                ,(VisitID != 0 ? VisitID.ToString() : CaseID.ToString())  
                ,ddlVisitMan.SelectedValue
                ,ddlVisitType.SelectedValue
                ,ddlVisitReason.SelectedValue
                ,VisitResult
                ,tbVisitComment.Text
                ,user.ID.ToString ()
                ,user.OrgID.ToString() 
                ,TaiwanYear.ToDateString(tbPreBackDate.Text)
                , ddlCountry.SelectedValue  
            }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
       
         

              int NewVisitID = 0;
              if (dt.Rows.Count > 0)
              {
                  NewVisitID = Convert.ToInt32(dt.Rows[0][0]);
                  CaseID = Convert.ToInt32(dt.Rows[0][1]);
                  handlefile(NewVisitID);
              }


              DBUtil.DBOp("ConnDB", "  UPDATE [dbo].[C_CaseVisitDetail] SET  [LogicDel] = 1 where VisitID={0} "
                    , new string[] { NewVisitID.ToString()  }, NSDBUtil.CmdOpType.ExecuteNonQuery);

              //Response.Write(VacIDs);
              //Response.End();
              foreach (string v in Vac)
              {
              

                  string VacIDSeq = "", VisitResultDetailID = "0", PrejectDate = "", VacID = "", Seq = "";
                  
                VacIDSeq =Request.Form [RequestKeys.Find ( r =>r.Contains("ddlVaccineCode_" + v))] ?? "" ;
                 VacID = (VacIDSeq =="" ?v: VacIDSeq.Split('-')[0]);
                Seq = (VacIDSeq.Split ('-').Length >1 ? VacIDSeq.Split('-')[1] :"");
                VisitResultDetailID =Request.Form [RequestKeys.Find ( r =>r.Contains("ddlNotNeedOrRejectReason_" + v))] ?? "" ;
                PrejectDate = TaiwanYear.ToDateString(Request.Form[RequestKeys.Find(r => r.Contains("tbPrDate_" + v))] ?? "");
                   
               DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseVisit_xAddDetail]   {0},{1},{2} ,{3},{4}   "
                   , new string[] { NewVisitID.ToString(), VacID, Seq, VisitResultDetailID, PrejectDate }, NSDBUtil.CmdOpType.ExecuteNonQuery);
              
              }

             //更新個人紀錄
              DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseVisit_xUpdateRecordData]  {0} "
                   , new string[] { NewVisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);
             
        

    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='VisitCaseList.aspx?i=" + CaseID.ToString ()+"';", true);
        
    }



    public void handlefile(int NewVisitID)
    {
        UserVM user = AuthServer.GetLoginUser();

        NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient(); 

        try
        {


            if (fu_Visit.HasFile)
            { 
                int OutFileInfoID = 0;

                OutFileInfoID = WS.UploadFile(11, fu_Visit.PostedFile.ContentType, fu_Visit.FileName.Split('.').Last(), fu_Visit.FileName, user.ID, user.OrgID, fu_Visit.FileBytes);

                DBUtil.DBOp("ConnDB", "INSERT INTO [C_CaseVisitFile] ([VisitID] ,[FileID],FileType)  VALUES  ({0} , {1},{2})", new string[] { NewVisitID.ToString(), OutFileInfoID.ToString(), ddlVisitFileType.SelectedValue }, NSDBUtil.CmdOpType.ExecuteNonQuery);
            }

        }
        catch (Exception ex)
        {

            //Response.Write(ex.Message + ex.StackTrace);
            //Response.End();
        }

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            CaseID = Convert.ToInt32(DBUtil.DBOp("ConnDB", "update C_CaseVisit set  LogicDel=1 where VisitID={0};select CaseID from C_CaseVisit where  VisitID={0};", new string[] { VisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
        }
        catch { 
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='VisitCaseList.aspx?i=" + CaseID.ToString() + "';", true);
        
    }
    protected void ddlVisitReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVisitReason.SelectedValue != "")
        {
            ddlVisitReasonChange();
        }
    }

    protected void ddlVisitReasonChange()
    {
        ddlVisitResult.Items.Clear();
        ddlVisitResult.Items.Add(new ListItem("請選擇", ""));
        SystemCodeControl.ServerSelect(ref ddlVisitResult, "CaseVisit_VisitResult_Reason_" + ddlVisitReason.SelectedValue);

        tr_NotNeedOrReject.Visible = false;
        tr_PreBackDate.Visible = false;
    }
    protected void ddlVisitResultChange(string[] Vac,DataTable dt)
    {
        

        
        

      string VisitResult = ddlVisitResult.SelectedValue;

      string VisitReason = ddlVisitReason.SelectedValue;
        
         phNotNeedOrReject.Controls.Clear();
         tr_NotNeedOrReject.Visible = false;
         tr_PreBackDate.Visible = false;
         tr_Country.Visible = false;
          if (VisitReason == "1")
         {
             if (VisitResult == "5")
             { 
                 tr_PreBackDate.Visible = true;

             }
             else if (VisitResult == "6")
             {
                 tr_Country.Visible = true;

             }
             
             else  if (VisitResult == "1" || VisitResult == "2" || VisitResult == "3")
            {
                tr_NotNeedOrReject.Visible = true;
                if (VisitResult == "1" )
                    ltThTitle.Text = "不需接種";
                else  if (VisitResult == "2")
                   ltThTitle.Text ="拒絕接種";
                else if (VisitResult == "3")
                    ltThTitle.Text = "延後接種";

             

               

                for (int r =0;r< Vac.Length ;r++)
                //foreach (string v in Vac)
                {
                    string v = Vac[r];
                    DataTable dts = (DataTable)DBUtil.DBOp("ConnDB", "SELECT '' as id,'請選擇' as VaccineCode union all SELECT  VaccinIDnSeq ,VaccineCode FROM [dbo].[fn_GetVaccineCode] ({0})", new string[] { v }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
                   DropDownList ddlVaccineCode = new DropDownList();
                   ddlVaccineCode.ID = "ddlVaccineCode_"+v;
                   ddlVaccineCode.DataSource = dts;
                   
                   ddlVaccineCode.DataTextField = "VaccineCode";
                   ddlVaccineCode.DataValueField = "id";
                   ddlVaccineCode.DataBind();
                   RequiredFieldValidator rfv = new RequiredFieldValidator();
                   rfv.ID = "rfv_" + v;
                   rfv.ControlToValidate = "ddlVaccineCode_" + v;
                   rfv.InitialValue = "";
                   rfv.ErrorMessage = "*必填";
                   rfv.Display = ValidatorDisplay.Dynamic;
                   rfv.ForeColor = System.Drawing.Color.Red;

                   DropDownList ddlNotNeedOrRejectReason = new DropDownList();
                   ddlNotNeedOrRejectReason.ID = "ddlNotNeedOrRejectReason_" + v;
                   ddlNotNeedOrRejectReason.Items.Add(new ListItem("請選擇", ""));
                   SystemCodeControl.ServerSelect(ref ddlNotNeedOrRejectReason, "CaseVisit_VisitResult_NotNeedOrReject_" + ddlVisitResult.SelectedValue);
                  
                    RequiredFieldValidator rfvr = new RequiredFieldValidator();
                   rfvr.ID = "rfvr_" + v;
                   rfvr.ControlToValidate = "ddlNotNeedOrRejectReason_" + v;
                   rfvr.InitialValue = "";
                   rfvr.ErrorMessage = "*必填";
                   rfvr.Display = ValidatorDisplay.Dynamic;
                   rfvr.ForeColor = System.Drawing.Color.Red;

                   TextBox tbPrDate = new TextBox();
                   tbPrDate.ID = "tbPrDate_" + v;
                   tbPrDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
                   Image ImgPreDate = new Image();
                   ImgPreDate.ID = "ImgPreDate_" + v;
                   ImgPreDate.ImageUrl = "/images/icon_calendar.png";
                   ImgPreDate.Attributes.Add("onclick", "WdatePicker({ el:'" + tbPrDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");

                   RequiredFieldValidator rfvt = new RequiredFieldValidator();
                   rfvt.ID = "rfvt_" + v;
                   rfvt.ControlToValidate = "tbPrDate_" + v;
                   rfvt.InitialValue = "";
                   rfvt.ErrorMessage = "*必填";
                   rfvt.Display = ValidatorDisplay.Dynamic;
                   rfvt.ForeColor = System.Drawing.Color.Red;

                   if (dt.Rows.Count >0)
                   {
                       ddlVaccineCode.SelectedValue = dt.Rows[r]["VacIDnSeq"].ToString();
                       ddlNotNeedOrRejectReason.SelectedValue = dt.Rows[r]["VisitResultDetailID"].ToString();
                       tbPrDate.Text = dt.Rows[r]["PrejectDate"].ToString();
                   }





                   phNotNeedOrReject.Controls.Add(ddlVaccineCode);
                   phNotNeedOrReject.Controls.Add(rfv);
                   if (VisitResult != "3" )
                   {
                       phNotNeedOrReject.Controls.Add(ddlNotNeedOrRejectReason);
                       phNotNeedOrReject.Controls.Add(rfvr);
                   }
                   else
                   {

                       phNotNeedOrReject.Controls.Add(tbPrDate);
                       phNotNeedOrReject.Controls.Add(ImgPreDate);
                       phNotNeedOrReject.Controls.Add(rfvt);
                   }
                   phNotNeedOrReject.Controls.Add(GetControlFromTag("<br/>"));
                }
              

            }
         }

         
    }
    protected void ddlVisitResult_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlVisitResult.SelectedValue != "" )
        {
            ddlVisitResultChange(hdVac.Value.Split (','),new DataTable ());
        }
    }

    protected void tbVac_TextChanged(object sender, EventArgs e)
    { 
        ddlVisitResultChange(hdVac.Value.Split(','), new DataTable()); 
    }
}