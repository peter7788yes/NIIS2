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

public partial class CaseMantain_UserProfile : System.Web.UI.Page
{

    protected int CaseID = 0;
    //Conn
    protected string CountyAry = "[]";
    protected string TownAry = "[]";
    protected string VillageAry = "[]"; 
    protected string CountyInival = "0";
    protected string TownInival = "0";
    protected string VillageInival = "0";

    //Res
    protected string ResCountyAry = "[]";
    protected string ResTownAry = "[]";
    protected string ResVillageAry = "[]";
    protected string ResCountyInival = "0";
    protected string ResTownInival = "0";
    protected string ResVillageInival = "0";


    protected string MainContactInival = "0";
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // base.DisableTop(true);
       
        Session["NewCaseContacts"] = null;
        //清空
      
        //SystemCode.Update();
        //SystemOrgRegion.IsCaseInOrgRegion(AuthServer.GetLoginUser().OrgID, CaseID);

        if (!Page.IsPostBack)
        {
          
            Session["UserProfileCaseID"] = CaseID;
            if (CaseID > 0)
            {
                ucCaseRemark1.CaseID = CaseID;
                BindData();
                //SystemOrgRegion.Update(); 
                //if (SystemOrgRegion.IsCaseInOrgRegion(AuthServer.GetLoginUser().OrgID, CaseID))
                //{
                // Response.Write ("在轄區內")   ;
                //}else
                //    Response.Write("不在轄區內");
            }
        }
        else
        { 
            if (Session["UserProfileCaseID"]!=null)
                int.TryParse(Session["UserProfileCaseID"].ToString (), out CaseID);
        }
        if (CaseID == 0)
        {
            btnAdd.Visible = true;
            TabDIV.Visible = false;
        }
        else
        {
            DivAgeTip.Visible = true;
            TabModifyLog.Visible = true;

            int OrgLevel = SystemOrg.GetVM(AuthServer.GetLoginUser().OrgID).OrgLevel;
        
            if (OrgLevel == 1) 
            {//CDC
                btnSave.Visible = true; 
            }
            else if (OrgLevel == 4) 
            { //衛生所
                btnCheck.Visible = true; 
            }
        }

    }
    protected void BindData()
    {
        CaseUserProfile c = new  CaseUserProfile(CaseID);


        if (c.CaseID > 0)
        {

            BirthDate.Text = c.BirthDate;
            tbIdNo.Text = c.IdNo;
            tbPassportNo.Text = c.PassportNo;
            tbResNo.Text = c.ResNo;
            tbOtherNo.Text = c.OtherNo;
            tbName.Text = c.ChName;
            tbEngName.Text = c.EnName;
            ddlGender.SelectedValue = c.Gender.ToString();
            tbHouseNo.Text = c.HouseNo;
            ddlONationality.SelectedValue = c.ONationality.ToString();

            foreach (string s in c.Language)
                foreach (ListItem i in cblLang.Items) { if (i.Value == s) { i.Selected = true; } };
            foreach (string s in c.Capacity)
            {
                for (int i = 1; i <= 4; i++)
                {
                    CheckBoxList cblist = (CheckBoxList)form1.FindControl("cblCapacity_" + i.ToString());
                    foreach (ListItem li in cblist.Items) { if (li.Value == s) { li.Selected = true; } };
                }
            }

            CountyInival = c.ConCounty.ToString();
            TownAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetTownList(Convert.ToInt32(c.ConCounty)));
            TownInival = c.ConTown.ToString();
            VillageAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Convert.ToInt32(c.ConTown)));
            VillageInival = c.ConVillage.ToString();

            ResCountyInival = c.ResCounty.ToString();
            ResTownAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetTownList(Convert.ToInt32(c.ResCounty)));
            ResTownInival = c.ResTown.ToString();
            ResVillageAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Convert.ToInt32(c.ResTown)));
            ResVillageInival = c.ResVillage.ToString();

           
             tbArea.Text = "";
             tbPregWeek.Text = c.PregWeek.ToString();
             tbBirthNum.Text = c.BirthNum.ToString();
             tbBirthSeq.Text = c.BirthSeq.ToString();
             tbBirthWeight.Text = c.BirthWeight.ToString();
             ddlBirthPlace.SelectedValue = c.BirthPlace.ToString();
             ddlDeliver.SelectedValue = c.Deliver.ToString();
             tbDeliverOrg.Text = c.DeliverOrg;
             ddlMarryStatus.SelectedValue = c.MarryStatus;

             tbEduLevel.Text = c.EduLevel;
             tbElemSchool.Text = c.ElemSchool;
             tbOccupation.Text = c.Occupation;
             tbEduLevel.Text = c.EduLevel; 
                

             tbResAddr.Text = c.ResAddr;
             tbConAddr.Text = c.ConAddr;

            tbResNei.Text = (c.ResNei== 0  ? "":c.ResNei.ToString());
            tbConNei.Text = (c.ConNei == 0 ? "" : c.ConNei.ToString());

            ddlBirthMulti.SelectedValue = c.BirthMulti.ToString(); 


             tbTelDayArea.Text = c.TelDayArea;
             tbTelDayNo.Text = c.TelDayNo;
             tbTelDayExt.Text = c.TelDayExt;


             tbTelNightArea.Text = c.TelNightArea;
             tbTelNightNo.Text = c.TelNightNo;
             tbTelNightExt.Text = c.TelNightExt;

             tbImmiType.Text = c.ImmiTypeName;
             tbArea.Text = c.RegionName;

             ltBirthDate .Text = c.BirthDate;
             ltIdNo.Text = c.IdNo;
             ltName.Text = c.ChName;
             ltGender.Text = Server.HtmlEncode (ddlGender.SelectedItem.Text);
             ltAgeTip.Text = c.AgeTip ;
            // MainContactInival =  Convert.ToString(DBUtil.DBOp("ConnDB", " select isnull((SELECT  top 1 [ContactID]  FROM [dbo].[C_CaseUserContact] where [LogicDel]=0 and  [CaseID]={0} and [IsMain]=1),0) ", new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
            // MainContactInival = c.MainContactCaseID.ToString();
             MainContactInival = Convert.ToString(DBUtil.DBOp("ConnDB", " select isnull((SELECT  top 1 [ContactID]  FROM [dbo].[C_CaseUserContact] where [LogicDel]=0 and  [CaseID]={0} and [IsMain]=1),0) ", new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));
             CaseIDdiv.Controls.Add(GetControlFromTag( CaseID.ToString ()));

             string MobileFormat = "<div class=\"MobileDetail\"><input name=\"tbMobileNo_{0}\"  type=\"text\" value=\"{1}\" class=\"text03 tbMobile\" /><a onclick =\"javascript:void(0);\" class=\"DelMobile\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddMobile\"><img src=\"/images/icon_increase.png\" /></a></div>";
               foreach (UserMobile um in  c.Mobiles ) MobileDIV.Controls.Add(GetControlFromTag(string.Format(MobileFormat, um.ID, um.Mobile)));
             
               string  EmailFormat = "<div class=\"EmailDetail\"><input name=\"tbEmail_{0}\"  type=\"text\" value=\"{1}\" class=\"text03 tbEmail\" /><a onclick =\"javascript:void(0);\" class=\"DelEmail\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddEmail\"><img src=\"/images/icon_increase.png\" /></a></div>";

               foreach (UserEmail  ue in c.Emails)  EmailDIV.Controls.Add(GetControlFromTag(string.Format(EmailFormat, ue.ID, ue.Email)));
         


            //info
               ltCreateInfo.Text = c.CreateInfo;
               ltModifyInfo.Text = c.ModifyInfo;
            //李小明 - 衛生福利部疾病管制署 - 104/5/19 14:23:45





        }






    }
   
     

    protected void Page_Init(object sender, EventArgs e)
    {
         
        if (!Page.IsPostBack)
        {
            int.TryParse(Request.Form["i"], out CaseID);


            BirthDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            BirthDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + BirthDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            SystemCodeControl.ServerSelect(ref ddlONationality, "CaseUser_ONationality");
            SystemCodeControl.ServerCheckBox(ref cblLang, "CaseUser_Language");
            cblLang.Attributes.Add("style", "display:inline-block;");
            SystemCodeControl.ServerSelect(ref ddlBirthMulti, "CaseUser_BirthMulti");
            SystemCodeControl.ServerSelect(ref ddlGender, "CaseUser_Gender");
            
         
            //身份別有4種
            DataTable CapDt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT [ID] ,[CapacityCate] ,[TypeName] ,[bCanEdit]  FROM [dbo].[C_CaseUserCapacity_Type] order by [CapacityCate] ,[ID]", new string[] { }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);


            foreach (DataRow r in CapDt.Rows)
            {
                CheckBoxList cblist = (CheckBoxList)form1.FindControl("cblCapacity_" + r["CapacityCate"].ToString());
                ListItem li = new ListItem(r["TypeName"].ToString(), r["ID"].ToString());
                li.Enabled = Convert.ToBoolean(r["bCanEdit"]);

                if (li.Enabled || CaseID > 0)
                  cblist.Items.Add(li);

                
                
                
                    if (cblist.Items.Count > 0 || CaseID>0)
                       form1.FindControl("tr_cblCapacity_" + r["CapacityCate"].ToString()).Visible = true;
                                     
                   
            }
             
             
            #region 聯絡地址

                  CountyAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
              
                  ddlConCounty.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlConCounty.Attributes.Add("ng-change", "SelectConCountyChange()");
                  ddlConCounty.Attributes.Add("ng-model", "VM.SelectCounty");
                 ddlConCounty.Attributes.Add("class", "ConCounty");
                 ddlConCounty.Items[0].Attributes.Add("ng-repeat", "option in VM.CountyAry"); 
             
               
                 ddlConTown.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlConTown.Attributes.Add("class", "ConTown"); 
                 ddlConTown.Attributes.Add("ng-change", "SelectConTownChange()");
                 ddlConTown.Attributes.Add("ng-model", "VM.SelectTown"); 
                  ddlConTown.Items[0].Attributes.Add("ng-repeat", "option in VM.TownAry"); 
             
                  ddlConVillage.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlConVillage.Attributes.Add("class", "ConVillage");  
                 ddlConVillage.Attributes.Add("ng-model", "VM.SelectVillage");
                 ddlConVillage.Items[0].Attributes.Add("ng-repeat", "option in VM.VillageAry");
                  #endregion

            #region 戶籍地址


                 ResCountyAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
                 ddlResCounty.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlResCounty.Attributes.Add("ng-change", "SelectResCountyChange()");
                 ddlResCounty.Attributes.Add("ng-model", "VM.SelectResCounty");
                 ddlResCounty.Attributes.Add("class", "ResCounty");
                 ddlResCounty.Items[0].Attributes.Add("ng-repeat", "option in VM.ResCountyAry");
                

                 ddlResTown.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlResTown.Attributes.Add("class", "ResTown");
                 ddlResTown.Attributes.Add("ng-change", "SelectResTownChange()");
                 ddlResTown.Attributes.Add("ng-model", "VM.SelectResTown");
                 ddlResTown.Items[0].Attributes.Add("ng-repeat", "option in VM.ResTownAry");

                 ddlResVillage.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlResVillage.Attributes.Add("class", "ResVillage");
                 ddlResVillage.Attributes.Add("ng-model", "VM.SelectResVillage");
                 ddlResVillage.Items[0].Attributes.Add("ng-repeat", "option in VM.ResVillageAry");
            #endregion



                 ddlMainContact.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 //ddlMainContact.Attributes.Add("ng-change", "SelectConCountyChange()");
                 ddlMainContact.Attributes.Add("ng-model", "VM.SelectMainContact");
                 ddlMainContact.Attributes.Add("class", "SelectMainContact");
                 ddlMainContact.Items[0].Attributes.Add("ng-repeat", "option in VM.MainContactAry");
                // ddlMainContact.Items[0].Attributes.Add("selected", "{{ option.S == '1' ? 'selected' : '' }}");
     
              //ltDDLCommentKind.Text = HtmlSelect("","ddlCommentKind", "CaseUser_RemarkType", "");

         

        }
        
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         
        //if (BirthDate.Text.Length == 6)
        //    BirthDate.Text = "0" + BirthDate.Text;
        //string BrithDate =  (Convert.ToInt32 (BirthDate.Text.Substring(0, 3))+1911).ToString ()  + "/" + BirthDate.Text.Substring(3, 2) + "/" + BirthDate.Text.Substring(5, 2) ;

        //int ApplyID = 0;


        //DataSet ds = new DataSet();

        //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        //{
        //    string sqlpoName = "dbo.usp_CaseUser_xAddCaseUser";
        //    if (CaseID != 0) sqlpoName = "dbo.usp_CaseUser_xModifyCaseUser";


        //    using (SqlCommand cmd = new SqlCommand(sqlpoName, sc))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        if (CaseID != 0)
        //        cmd.Parameters.AddWithValue("@CaseID", CaseID);

        //    cmd.Parameters.AddWithValue("@BirthDate", BrithDate);
        //    cmd.Parameters.AddWithValue("@IdNo", tbIdNo .Text); 
        //    cmd.Parameters.AddWithValue("@PassportNo", tbPassportNo .Text); 
        //    cmd.Parameters.AddWithValue("@ResNo", tbResNo .Text); 
        //    cmd.Parameters.AddWithValue("@OtherNo", tbOtherNo .Text);
        //    cmd.Parameters.AddWithValue("@ChName", tbName .Text);
        //    cmd.Parameters.AddWithValue("@EnName", tbEngName .Text);
        //    cmd.Parameters.AddWithValue("@Gender", ddlGender .SelectedValue);
        //    cmd.Parameters.AddWithValue("@HouseNo", tbHouseNo .Text);
        //    cmd.Parameters.AddWithValue("@ONationality",  ddlONationality .SelectedValue);
        //    cmd.Parameters.AddWithValue("@Language", String.Join(",", cblLang.Items.OfType<ListItem>().Where(r => r.Selected) .Select(r => r.Value )));

        //   // cmd.Parameters.AddWithValue("@Capacity", String.Join(",", cblCapacity.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value)));
        //        //全部加起來  到DB再處理 因為 有些身份別是不能改的
        //    cmd.Parameters.AddWithValue("@Capacity", 
        //         String.Join(",",
        //      String.Join(",", cblCapacity_1.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        //    , String.Join(",", cblCapacity_2.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        //    , String.Join(",", cblCapacity_3.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        //    , String.Join(",", cblCapacity_4.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))                       
        //      )  );
                 

        //    //cmd.Parameters.AddWithValue("@ResCounty", ddlResCounty.SelectedValue );
        //    //cmd.Parameters.AddWithValue("@ResTown", ddlResTown.SelectedValue);
        //    //cmd.Parameters.AddWithValue("@ResVillage", ddlResVillage.SelectedValue);
        //    cmd.Parameters.AddWithValue("@ResCounty", Request[ddlResCounty.ClientID.Replace("_", "$")].ToString());
        //    cmd.Parameters.AddWithValue("@ResTown", Request[ddlResTown.ClientID.Replace("_", "$")].ToString());
        //    cmd.Parameters.AddWithValue("@ResVillage", Request[ddlResVillage.ClientID.Replace("_", "$")].ToString());


        //    cmd.Parameters.AddWithValue("@ConCounty",Request[ddlConCounty.ClientID.Replace("_", "$")].ToString());
        //    cmd.Parameters.AddWithValue("@ConTown", Request[ddlConTown.ClientID.Replace("_", "$")].ToString());
        //    cmd.Parameters.AddWithValue("@ConVillage", Request[ddlConVillage.ClientID.Replace("_", "$")].ToString());

        //    cmd.Parameters.AddWithValue("@PregWeek",tbPregWeek .Text);
        //    cmd.Parameters.AddWithValue("@BirthNum",tbBirthNum.Text );
        //    cmd.Parameters.AddWithValue("@BirthSeq",tbBirthSeq.Text  );
        //    cmd.Parameters.AddWithValue("@BirthWeight",tbBirthWeight.Text );
        //    cmd.Parameters.AddWithValue("@BirthPlace",ddlBirthPlace.SelectedValue );
        //    cmd.Parameters.AddWithValue("@Deliver",ddlDeliver.SelectedValue );
        //    cmd.Parameters.AddWithValue("@DeliverOrg",tbDeliverOrg .Text  );
        //    cmd.Parameters.AddWithValue("@MarryStatus",ddlMarryStatus.SelectedValue );
        //    cmd.Parameters.AddWithValue("@EduLevel", tbEduLevel .Text  );
        //    cmd.Parameters.AddWithValue("@ElemSchool",  tbElemSchool .Text );
        //    cmd.Parameters.AddWithValue("@Occupation", tbOccupation .Text  ); 
        //    cmd.Parameters.AddWithValue("@ModifyUserID", AuthServer.GetLoginUser().ID); 
        //    cmd.Parameters.AddWithValue("@TelDayArea", tbTelDayArea.Text);
        //    cmd.Parameters.AddWithValue("@TelDayNo", tbTelDayNo.Text);
        //    cmd.Parameters.AddWithValue("@TelDayExt", tbTelDayExt.Text);
        //    cmd.Parameters.AddWithValue("@TelNightArea", tbTelNightArea.Text);
        //    cmd.Parameters.AddWithValue("@TelNightNo", tbTelNightNo.Text);
        //    cmd.Parameters.AddWithValue("@TelNightExt", tbTelNightExt.Text); 
        //    cmd.Parameters.AddWithValue("@ResAddr", tbResAddr .Text  );
        //    cmd.Parameters.AddWithValue("@ConAddr",tbConAddr .Text   );
        //    cmd.Parameters.AddWithValue("@BirthMulti", tbBirthMulti.Text  );

        //    cmd.Parameters.AddWithValue("@ResNei", tbResNei.Text);
        //    cmd.Parameters.AddWithValue("@ConNei", tbConNei.Text);


        //    try
        //    {
        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            sc.Open();
        //            da.Fill(ds);
        //        }

              

        //      CaseID= Convert.ToInt32(ds.Tables[0].Rows[0][0]);


        //    }
        //    catch(Exception ex) {
        //        Response.Write(ex.StackTrace +ex.Message );
        //    }

        //    }
        //}

        CaseUserProfile c = GetModifyCase();
        c.Update();

        //電話email 不用審 直接更新
        SaveEmailMobiles(c.CaseID);



      
      
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='UserProfileList.aspx'", true);
      
       
    }


    protected void SaveContactIDs(string ContactIDs, int iCaseID)
    {
       
              DBUtil.DBOp("ConnDB"
               , @" UPDATE [dbo]. [C_CaseUserContact]   SET  [CaseID] ={0} WHERE [CaseID] =0 and ContactID in  (SELECT data FROM [dbo].[fn_slip_str] ( {1} , ',' ) where data !='') 
                    ; exec [dbo].[usp_CaseUser_xUpdateCaseUserMother] {0}  "
               , new string[] { iCaseID.ToString(), ContactIDs }
               , NSDBUtil.CmdOpType.ExecuteNonQuery);
        
    }
    protected void SaveRemarkIDs(string RemarkIDs, int iCaseID)
    {
       
              DBUtil.DBOp("ConnDB"
               , " UPDATE [dbo].[C_CaseUserRemark]   SET  [CaseUserID] ={0} WHERE [CaseUserID] =0 and ID in  (SELECT data FROM [dbo].[fn_slip_str] ( {1} , ',' ) where data !='')  "
               , new string[] { iCaseID.ToString(), RemarkIDs }
               , NSDBUtil.CmdOpType.ExecuteNonQuery);
        
    }
      
    

     

    //protected string HtmlSelect(string inputid, string inputname,  string SystemCodeCate, string SelectVal)
    //{
    //    string select = "<select   {2}  {0}  >{1}</select>";
    //    string options = "";
    //    List<SystemCodeVM> SystemCodeList = SystemCode.GetDict(SystemCodeCate);
    //    foreach (SystemCodeVM sc in SystemCodeList)
    //        options += string.Format("<option value=\"{1}\" {2} >{0}</option>", sc.EnumName, sc.EnumValue.ToString(), (sc.EnumValue.ToString() == SelectVal ? "selected" : ""));

    //    return string.Format(select, (inputid != "" ? " id=\"" + inputid + "\"" : ""), options, (inputname != "" ? " name=\"" + inputname + "\"" : ""));

    //}
    
    //  protected void BindComment( )
    //{

    //    string CommentAreaFormat = "<div id=\"CommentDetail\">{3}<input name=\"tbCommentOld_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelPS\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddPS\"><img src=\"/images/icon_increase.png\" /></a></div>";

    //    SqlCommand cmd = new SqlCommand("select ID as RemarkID, [RemarkType] ,[CaseRemark] from [C_CaseUserRemark] where CaseUserID=@CaseID  and LogicDel=0 order by ID ");
    //      cmd.Parameters.AddWithValue("@CaseID", CaseID);
    //      DataTable dt =  DB.GetDataTable(cmd, "ConnDB");

    //    foreach (DataRow r in dt.Rows)
    //    {
    //        CommentAreaDIV.Controls.Add(GetControlFromTag(string.Format(CommentAreaFormat, r["RemarkID"], r["CaseRemark"], r["RemarkType"], SystemCodeControl.HtmlSelect("ddlCommentKindOld_" + r["RemarkID"].ToString(), "ddlCommentKindOld_" + r["RemarkID"].ToString(), "CaseUser_RemarkType", r["RemarkType"].ToString()))));
        
    //    }
     
    // }

       
    
    public static Control GetControlFromTag(string controlTag)
      {
          Page p = new Page();
          p.AppRelativeVirtualPath = "~/";
          Control control = p.ParseControl(controlTag);
          return control;
      }


    protected CaseUserProfile GetModifyCase()
    {


   string ResCounty = Request[ddlResCounty.ClientID.Replace("_", "$")] ?? "";
        string ResTown = Request[ddlResTown.ClientID.Replace("_", "$")] ?? "";
        string ResVillage = Request[ddlResVillage.ClientID.Replace("_", "$")] ?? "";


        string ConCounty = Request[ddlConCounty.ClientID.Replace("_", "$")] ?? "";
        string ConTown = Request[ddlConTown.ClientID.Replace("_", "$")] ?? "";
        string ConVillage = Request[ddlConVillage.ClientID.Replace("_", "$")] ?? "";

        int iGender,iResCounty, iResTown, iResVillage, iConCounty, iConTown
            , iConVillage, iPregWeek, iBirthNum, iBirthSeq, iBirthWeight
            , iBirthPlace, iDeliver, iBirthMulti, iResNei, iConNei
            , iONationality;


        int.TryParse(ddlGender.SelectedValue, out iGender);
        int.TryParse(ddlONationality.SelectedValue, out iONationality);
   

        int.TryParse((ResCounty != "" ? ResCounty : "0"), out iResCounty);
        int.TryParse((ResTown != "" ? ResTown : "0"), out iResTown);
        int.TryParse((ResVillage != "" ? ResVillage : "0"), out iResVillage);
        int.TryParse((tbResNei.Text.Trim() == "" ? "0" : tbResNei.Text.Trim()), out iResNei);

        int.TryParse((ConCounty != "" ? ConCounty : "0"), out iConCounty);
        int.TryParse((ConTown != "" ? ConTown : "0"), out iConTown);
        int.TryParse((ConVillage != "" ? ConVillage : "0"), out iConVillage);
        int.TryParse((tbConNei.Text.Trim() == "" ? "0" : tbConNei.Text.Trim()), out iConNei); 

        int.TryParse((tbPregWeek.Text.Trim() == "" ? "0" : tbPregWeek.Text.Trim()), out iPregWeek);
        int.TryParse((tbBirthNum.Text.Trim() == "" ? "0" : tbBirthNum.Text.Trim()), out iBirthNum);
        int.TryParse((tbBirthSeq.Text.Trim() == "" ? "0" : tbBirthSeq.Text.Trim()), out iBirthSeq);
        int.TryParse((tbBirthWeight.Text.Trim() == "" ? "0" : tbBirthWeight.Text.Trim()), out iBirthWeight);
        int.TryParse(ddlBirthPlace.SelectedValue, out iBirthPlace); 
        int.TryParse(ddlBirthMulti.SelectedValue, out iBirthMulti);
        int.TryParse(ddlDeliver.SelectedValue, out iDeliver);






        CaseUserProfile c = new CaseUserProfile();
        c.CaseID = CaseID;
        c.BirthDate = BirthDate.Text;
        c.IdNo = tbIdNo.Text;
        c.PassportNo = tbPassportNo.Text;
        c.ResNo = tbResNo.Text;
        c.OtherNo = tbOtherNo.Text;
        c.ChName = tbName.Text;

        c.EnName = tbEngName.Text;
        c.Gender = iGender;
        c.HouseNo = tbHouseNo.Text;
        c.ONationality = iONationality; 
        c.Language = String.Join(",", cblLang.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value)).Split(',');
        c.Capacity = String.Join(",",
        String.Join(",", cblCapacity_1.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        , String.Join(",", cblCapacity_2.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        , String.Join(",", cblCapacity_3.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        , String.Join(",", cblCapacity_4.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value))
        ).Split(',');

     


        c.ResCounty = iResCounty;
        c.ResTown = iResTown;
        c.ResVillage = iResVillage; 
        c.ResNei = iResNei;
        c.ConNei = iConNei;
        c.ConCounty = iConCounty;
        c.ConTown = iConTown;
        c.ConVillage = iConVillage;

        c.PregWeek = iPregWeek;
        c.BirthNum = iBirthNum;
        c.BirthSeq = iBirthSeq;
        c.BirthWeight = iBirthWeight;
        c.BirthPlace = iBirthPlace;
        c.BirthMulti = iBirthMulti; 

        c.Deliver = iDeliver;
        c.DeliverOrg = tbDeliverOrg.Text;
        c.MarryStatus = ddlMarryStatus.SelectedValue;
        c.EduLevel = tbEduLevel.Text;
        c.ElemSchool = tbElemSchool.Text;
        c.Occupation = tbOccupation.Text;

        c.TelDayArea = tbTelDayArea.Text;
        c.TelDayNo = tbTelDayNo.Text;
        c.TelDayExt = tbTelDayExt.Text;
        c.TelNightArea = tbTelNightArea.Text;
        c.TelNightNo = tbTelNightNo.Text;
        c.TelNightExt = tbTelNightExt.Text;
        c.ResAddr = tbResAddr.Text;
        c.ConAddr = tbConAddr.Text;
     

        
        //電話 email






        return c;
    }


    protected void SaveEmailMobiles(int caseid)
    {
        CaseUserProfile u = new CaseUserProfile(caseid); 
        UserEmail ueop = new UserEmail();
        foreach (UserEmail ue in u.Emails)
        {

            if (Request.Form["tbEmail_" + ue.ID.ToString()] != null)
            {
                string Email = Request.Form["tbEmail_" + ue.ID.ToString()].ToString();
                if (ue.Email != Email) ueop.Update(ue.ID, Email);
            }
            else
                ueop.Delete(ue.ID);


        }
        UserMobile umop = new UserMobile();
        foreach (UserMobile um in u.Mobiles)
        {

            if (Request.Form["tbMobileNo_" + um.ID.ToString()] != null)
            {
                string MobileNo = Request.Form["tbMobileNo_" + um.ID.ToString()].ToString();
                if (um.Mobile != MobileNo) umop.Update(um.ID, MobileNo);
            }
            else
                umop.Delete(um.ID);


        }

        //新增
        string tbMobileNo = Request.Form["tbMobileNo"] ?? "";
        string tbEmail = Request.Form["tbEmail"] ?? "";
        tbMobileNo = tbMobileNo.TrimEnd(',');
        tbEmail = tbEmail.TrimEnd(',');

        if (tbEmail != "") ueop.Add(caseid, tbEmail);
        if (tbMobileNo != "") umop.Add(caseid, tbMobileNo);

        u.UpdateMainMobileCol(caseid);


    }
   


     protected void btnCheck_Click(object sender, EventArgs e)
    {

        CaseUserProfile c = GetModifyCase(); 
        c.SimpleUpdate();

        SaveEmailMobiles(c.CaseID);








        Session["ModifiedCaseToCheck"] = c;
        Response.Redirect("UpdateReview.aspx");
       
    }


     protected void btnAdd_Click(object sender, EventArgs e)
     {
         CaseUserProfile c = GetModifyCase();
         CaseID = c.Add();
      
             if (CaseID > 0)
             { 
                 string RemarkIDs = Request.Form["NewCaseUserRemarkIDs"] ?? ""; 
                 if (RemarkIDs.Length > 0) SaveRemarkIDs(RemarkIDs, CaseID);

                 string ContactIDs = Request.Form["NewCaseUserContactIDs"] ?? "";
                 if (ContactIDs.Length > 0) SaveContactIDs(ContactIDs, CaseID);


                 SaveEmailMobiles(CaseID);
             }
 



         Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='UserProfileList.aspx'", true);
      
     }
}