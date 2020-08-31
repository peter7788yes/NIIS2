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

public partial class CaseMantain_UserProfile : BasePage
{

    int CaseID = 0;
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
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        QS();
      
        if (!Page.IsPostBack)
        {
            
            BindData();
        
        }
        if (CaseID == 0)
        {
            btnAddContract.Visible = false;
            ContractTR.Visible = false;
        }
        else
        {
            btnAddContract.Visible = true;
            ContractTR.Visible = true;
        }

    }
    protected void BindData()
    {

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetCaseUser", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseID", CaseID);  
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        DataTable dt = ds.Tables[0];


        if (dt.Rows.Count > 0)
        {

            BirthDate.Text = dt.Rows[0]["BirthDateSimple"].ToString();
            tbIdNo.Text = dt.Rows[0]["IdNo"].ToString();
            tbPassportNo.Text = dt.Rows[0]["PassportNo"].ToString();
            tbResNo.Text = dt.Rows[0]["ResNo"].ToString();
            tbOtherNo.Text = dt.Rows[0]["OtherNo"].ToString();
            tbName.Text = dt.Rows[0]["ChName"].ToString();
            tbEngName.Text = dt.Rows[0]["EnName"].ToString();
            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            tbHouseNo.Text = dt.Rows[0]["HouseNo"].ToString();
            ddlONationality.SelectedValue = dt.Rows[0]["ONationality"].ToString();

            foreach (string s in dt.Rows[0]["Language"].ToString().Split(','))
                foreach (ListItem i in cblLang.Items) { if (i.Value == s) { i.Selected = true; } };
            foreach (string s in dt.Rows[0]["Capacity"].ToString().Split(','))
                foreach (ListItem i in cblCapacity.Items) { if (i.Value == s) { i.Selected = true; } }; 
            
            
             
            CountyInival = dt.Rows[0]["ConCounty"].ToString();
            TownAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetTownList(Convert.ToInt32(dt.Rows[0]["ConCounty"])));
            TownInival = dt.Rows[0]["ConTown"].ToString();
            VillageAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Convert.ToInt32(dt.Rows[0]["ConTown"])));
            VillageInival = dt.Rows[0]["ConVillage"].ToString();

            ResCountyInival = dt.Rows[0]["ResCounty"].ToString();
            ResTownAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetTownList(Convert.ToInt32(dt.Rows[0]["ConCounty"])));
            ResTownInival = dt.Rows[0]["ResTown"].ToString();
            ResVillageAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(Convert.ToInt32(dt.Rows[0]["ConTown"])));
            ResVillageInival = dt.Rows[0]["ResVillage"].ToString();

            //if (SystemAreaCode.dict.ContainsKey("County"))
            //{
            //    List<SystemAreaCodeVM> SystemAreaCodeList = SystemAreaCode.dict["County"];
            //    ddlResCounty.Items.Clear();
            //    ddlResCounty.Items.Add(new ListItem("縣市", ""));
            //    foreach (SystemAreaCodeVM sc in SystemAreaCodeList)
            //     ddlResCounty.Items.Add(new ListItem(sc.AreaName, sc.ID.ToString())); 

            //    ddlResCounty.SelectedValue = dt.Rows[0]["ResCounty"].ToString(); 

            //}
            //if (SystemAreaCode.dict.ContainsKey("Town"))
            //{
            //    List<SystemAreaCodeVM> SystemAreaCodeList = SystemAreaCode.dict["Town"];
            //    ddlResTown.Items.Clear();
            //    ddlResTown.Items.Add(new ListItem("鄉鎮市區", ""));
            //    foreach (SystemAreaCodeVM sc in SystemAreaCodeList)
            //        ddlResTown.Items.Add(new ListItem(sc.AreaName, sc.ID.ToString()));

            //    ddlResTown.SelectedValue = dt.Rows[0]["ResTown"].ToString();

            //}
             tbArea.Text = "";
             tbPregWeek.Text = dt.Rows[0]["PregWeek"].ToString();
             tbBirthNum.Text = dt.Rows[0]["BirthNum"].ToString();
             tbBirthSeq.Text = dt.Rows[0]["BirthSeq"].ToString();
             tbBirthWeight.Text = dt.Rows[0]["BirthWeight"].ToString();
             ddlBirthPlace.SelectedValue = dt.Rows[0]["BirthPlace"].ToString();
             ddlDeliver.SelectedValue = dt.Rows[0]["Deliver"].ToString();
             tbDeliverOrg.Text = dt.Rows[0]["DeliverOrg"].ToString();
             ddlMarryStatus.SelectedValue = dt.Rows[0]["MarryStatus"].ToString();

             tbEduLevel.Text = dt.Rows[0]["EduLevel"].ToString();
             tbElemSchool.Text = dt.Rows[0]["ElemSchool"].ToString();
             tbOccupation.Text = dt.Rows[0]["Occupation"].ToString();
             tbEduLevel.Text = dt.Rows[0]["EduLevel"].ToString();
             tbEduLevel.Text = dt.Rows[0]["EduLevel"].ToString();
               
            //cmd.Parameters.AddWithValue("@TelDayArea", "03");
            //cmd.Parameters.AddWithValue("@TelDayNo", "12345678");
            //cmd.Parameters.AddWithValue("@TelDayExt", "90");
            //cmd.Parameters.AddWithValue("@TelNightArea", "03");
            //cmd.Parameters.AddWithValue("@TelNightNo", "12345678");
            //cmd.Parameters.AddWithValue("@TelNightExt", "90");

             tbResAddr.Text = dt.Rows[0]["ResAddr"].ToString();
             tbConAddr.Text = dt.Rows[0]["ConAddr"].ToString(); 
             tbBirthMulti.Text = dt.Rows[0]["BirthMulti"].ToString();



             tbTelDayArea.Text = dt.Rows[0]["TelDayArea"].ToString();
             tbTelDayNo.Text = dt.Rows[0]["TelDayNo"].ToString();
             tbTelDayExt.Text = dt.Rows[0]["TelDayExt"].ToString();


             tbTelNightArea.Text = dt.Rows[0]["TelNightArea"].ToString();
             tbTelNightNo.Text = dt.Rows[0]["TelNightNo"].ToString();
             tbTelNightExt.Text = dt.Rows[0]["TelNightExt"].ToString();

             tbImmiType.Text = SystemCode.GetName("CaseUser_ImmiType", Convert.ToInt32(dt.Rows[0]["ImmiType"]));

             ltBirthDate .Text = dt.Rows[0]["BirthDateSimple"].ToString();
             ltIdNo.Text = dt.Rows[0]["IdNo"].ToString();
             ltName.Text = dt.Rows[0]["ChName"].ToString();
             ltGender.Text = ddlGender.SelectedItem.Text;
             MainContactInival =  Convert.ToString(DBUtil.DBOp("ConnDB", " select isnull((SELECT  top 1 [ContactID]  FROM [dbo].[C_CaseUserContact] where [LogicDel]=0 and  [CaseID]={0} and [IsMain]=1),0) ", new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar));


             CaseIDdiv.Controls.Add(GetControlFromTag( CaseID.ToString ()));
             
                 BindComment();
                BindContact();
                BindMobile();
                BindEmail();
        }






    }
    protected void BindMobile()
    {
        string CommentAreaFormat = "<div class=\"MobileDetail\"><input name=\"tbMobileNo_{0}\"  type=\"text\" value=\"{1}\" class=\"text02 tbMobile\" /><a onclick =\"javascript:void(0);\" class=\"DelPS\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddPS\"><img src=\"/images/icon_increase.png\" /></a></div>";

        SqlCommand cmd = new SqlCommand("SELECT MobileID,[MobileNo]  FROM [C_CaseUserMobile] where [LogicDel]=0 and [CaseID]=@CaseID order by [MobileID] ");
        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            MobileDIV.Controls.Add(GetControlFromTag(string.Format(CommentAreaFormat, r["MobileID"], r["MobileNo"])));

        }


    }
    protected void BindEmail()
    {
        string CommentAreaFormat = "<div class=\"EmailDetail\"><input name=\"tbEmail_{0}\"  type=\"text\" value=\"{1}\" class=\"text02 tbEmail\" /><a onclick =\"javascript:void(0);\" class=\"DelEmail\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddEmail\"><img src=\"/images/icon_increase.png\" /></a></div>";

        SqlCommand cmd = new SqlCommand("SELECT [EmailID],[Email]  FROM [C_CaseUserEmail] where [LogicDel]=0 and [CaseID]=@CaseID order by [EmailID] ");
        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            EmailDIV.Controls.Add(GetControlFromTag(string.Format(CommentAreaFormat, r["EmailID"], r["Email"])));

        }


    }
    protected void QS()
    {
        int.TryParse(Request.QueryString["i"], out CaseID); 

    }
    protected void Page_Init(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BirthDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            BirthDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + BirthDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");

          //  SystemCode.UpdateSystemCode();
            if (SystemCode.dict.ContainsKey("CaseUser_ONationality"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["CaseUser_ONationality"];

                foreach (SystemCodeVM sc in SystemCodeList) ddlONationality.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));

            }

            if (SystemCode.dict.ContainsKey("CaseUser_Language"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["CaseUser_Language"];

                foreach (SystemCodeVM sc in SystemCodeList) cblLang.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));

            }

            if (SystemCode.dict.ContainsKey("CaseUser_Capacity"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["CaseUser_Capacity"];

                foreach (SystemCodeVM sc in SystemCodeList) cblCapacity.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));

            }


            // SystemAreaCode.Update();


            #region 戶籍地址
                  //ddlResCounty.Items.Add(new ListItem("請選擇縣市", "0"));
                  //ddlResCounty.Enabled = false;
                  //ddlResTown.Items.Add(new ListItem("鄉鎮市區", "0"));
                  //ddlResTown.Enabled = false;
                  //ddlResVillage.Items.Add(new ListItem("村里", "0"));
                  //ddlResVillage.Enabled = false;
            #endregion
            #region 聯絡地址

                  CountyAry = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
                //  List<SystemAreaCodeVM> CityList = SystemAreaCode.GetCountyList();
                  ddlConCounty.Items.Add(new ListItem("{{option.N}}", "{{option.I}}"));
                 ddlConCounty.Attributes.Add("ng-change", "SelectConCountyChange()");
                  ddlConCounty.Attributes.Add("ng-model", "VM.SelectCounty");
                 ddlConCounty.Attributes.Add("class", "ConCounty");
                 ddlConCounty.Items[0].Attributes.Add("ng-repeat", "option in VM.CountyAry"); 
             //    foreach (SystemAreaCodeVM sc in CityList) ddlConCounty.Items.Add(new ListItem(sc.AreaName, sc.ID.ToString()));

               
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

                

        }

        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (BirthDate.Text.Length == 6)
            BirthDate.Text = "0" + BirthDate.Text;
        string BrithDate =  (Convert.ToInt32 (BirthDate.Text.Substring(0, 3))+1911).ToString ()  + "/" + BirthDate.Text.Substring(3, 2) + "/" + BirthDate.Text.Substring(5, 2) ;

        int ApplyID = 0;


        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            string sqlpoName = "dbo.usp_CaseUser_xAddCaseUser";
            if (CaseID != 0) sqlpoName = "dbo.usp_CaseUser_xModifyCaseUser";


            using (SqlCommand cmd = new SqlCommand(sqlpoName, sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (CaseID != 0)
                cmd.Parameters.AddWithValue("@CaseID", CaseID);

                cmd.Parameters.AddWithValue("@BirthDate", BrithDate);
            cmd.Parameters.AddWithValue("@IdNo", tbIdNo .Text); 
            cmd.Parameters.AddWithValue("@PassportNo", tbPassportNo .Text); 
            cmd.Parameters.AddWithValue("@ResNo", tbResNo .Text); 
            cmd.Parameters.AddWithValue("@OtherNo", tbOtherNo .Text);
            cmd.Parameters.AddWithValue("@ChName", tbName .Text);
            cmd.Parameters.AddWithValue("@EnName", tbEngName .Text);
            cmd.Parameters.AddWithValue("@Gender", ddlGender .SelectedValue);
            cmd.Parameters.AddWithValue("@HouseNo", tbHouseNo .Text);
            cmd.Parameters.AddWithValue("@ONationality",  ddlONationality .SelectedValue);
            cmd.Parameters.AddWithValue("@Language", String.Join(",", cblLang.Items.OfType<ListItem>().Where(r => r.Selected) .Select(r => r.Value )));

            cmd.Parameters.AddWithValue("@Capacity", String.Join(",", cblCapacity.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value)));

            //cmd.Parameters.AddWithValue("@ResCounty", ddlResCounty.SelectedValue );
            //cmd.Parameters.AddWithValue("@ResTown", ddlResTown.SelectedValue);
            //cmd.Parameters.AddWithValue("@ResVillage", ddlResVillage.SelectedValue);
            cmd.Parameters.AddWithValue("@ResCounty", Request[ddlResCounty.ClientID.Replace("_", "$")].ToString());
            cmd.Parameters.AddWithValue("@ResTown", Request[ddlResTown.ClientID.Replace("_", "$")].ToString());
            cmd.Parameters.AddWithValue("@ResVillage", Request[ddlResVillage.ClientID.Replace("_", "$")].ToString());


            cmd.Parameters.AddWithValue("@ConCounty",Request[ddlConCounty.ClientID.Replace("_", "$")].ToString());
            cmd.Parameters.AddWithValue("@ConTown", Request[ddlConTown.ClientID.Replace("_", "$")].ToString());
            cmd.Parameters.AddWithValue("@ConVillage", Request[ddlConVillage.ClientID.Replace("_", "$")].ToString());

            cmd.Parameters.AddWithValue("@PregWeek",tbPregWeek .Text);
            cmd.Parameters.AddWithValue("@BirthNum",tbBirthNum.Text );
            cmd.Parameters.AddWithValue("@BirthSeq",tbBirthSeq.Text  );
            cmd.Parameters.AddWithValue("@BirthWeight",tbBirthWeight.Text );
            cmd.Parameters.AddWithValue("@BirthPlace",ddlBirthPlace.SelectedValue );
            cmd.Parameters.AddWithValue("@Deliver",ddlDeliver.SelectedValue );
            cmd.Parameters.AddWithValue("@DeliverOrg",tbDeliverOrg .Text  );
            cmd.Parameters.AddWithValue("@MarryStatus",ddlMarryStatus.SelectedValue );
            cmd.Parameters.AddWithValue("@EduLevel", tbEduLevel .Text  );
            cmd.Parameters.AddWithValue("@ElemSchool",  tbElemSchool .Text );
            cmd.Parameters.AddWithValue("@Occupation", tbOccupation .Text  );
          
           
            if (CaseID != 0)
                cmd.Parameters.AddWithValue("@ModifyUserID", 1);
            else
                cmd.Parameters.AddWithValue("@CreatedUserID", 1);
                 

            cmd.Parameters.AddWithValue("@TelDayArea", tbTelDayArea.Text);
            cmd.Parameters.AddWithValue("@TelDayNo", tbTelDayNo.Text);
            cmd.Parameters.AddWithValue("@TelDayExt", tbTelDayExt.Text);
            cmd.Parameters.AddWithValue("@TelNightArea", tbTelNightArea.Text);
            cmd.Parameters.AddWithValue("@TelNightNo", tbTelNightNo.Text);
            cmd.Parameters.AddWithValue("@TelNightExt", tbTelNightExt.Text); 
            cmd.Parameters.AddWithValue("@ResAddr", tbResAddr .Text  );
            cmd.Parameters.AddWithValue("@ConAddr",tbConAddr .Text   );
            cmd.Parameters.AddWithValue("@BirthMulti", tbBirthMulti.Text  );
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

                if (CaseID != 0)//修改會有applyID
                   ApplyID = Convert.ToInt32(ds.Tables[0].Rows[0][1]);

              CaseID= Convert.ToInt32(ds.Tables[0].Rows[0][0]);


            }
            catch(Exception ex) {
                Response.Write(ex.StackTrace +ex.Message );
            }

            }
        }

        if (CaseID > 0)
        {
            QSOldComment();
            QSComment(); 
        }
        if (ApplyID!=0)
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "location.href='UpdateReview.aspx?a=" + ApplyID + "'", true);
      
        else 
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='UserProfile.aspx?i=" + CaseID + "'", true);
      
       
    }




    protected void QSComment()
    {
        string CommentKind ="",Comment="";
       if ( Request.Form ["ddlCommentKindddlCommentKind"]!=null)
           CommentKind=Request.Form ["ddlCommentKindddlCommentKind"].ToString ();
        if (Request.Form ["tbComment"] !=null)
            Comment = Request.Form["tbComment"].ToString();

        string[] CommentKindArray = CommentKind.Split(',');
        string[] CommentArray = Comment.Split(',');


        for (int i = 0; i < CommentKindArray.Length; i++)
        {
            if (CommentKindArray[i].ToString() != "" && CommentArray[i].ToString().Trim () != "")
               SaveComment(CaseID, Convert.ToInt32(CommentKindArray[i]), CommentArray[i]);
        }

      




    }
    protected void QSOldComment()
    {
        

        SqlCommand cmd = new SqlCommand("select RemarkID  from [C_CaseUserRemark] where CaseUserID=@CaseID order by RemarkID ");
        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {   string CommentKind ="",Comment="";
        if (Request.Form["ddlCommentKindddlCommentKind_" + r["RemarkID"]] != null)
        {
            CommentKind = Request.Form["ddlCommentKindddlCommentKind_" + r["RemarkID"]].ToString();
        }
        if (Request.Form["tbComment_" + r["RemarkID"]] != null)
        {
            Comment = Request.Form["tbComment_" + r["RemarkID"]].ToString();

            
                SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserRemark] set  [RemarkType]=@RemarkType  ,[CaseRemark]=@CaseRemark  where RemarkID=@RemarkID  ");
                cmdr.Parameters.AddWithValue("@RemarkID", r["RemarkID"]);
                cmdr.Parameters.AddWithValue("@RemarkType", CommentKind);
                cmdr.Parameters.AddWithValue("@CaseRemark", Comment);

                DB.ExecuteNonQuery(cmdr, "ConnDB");
            
        }
        else
        { 
          SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserRemark] set LogicDel=1 where RemarkID=@RemarkID  ");
          cmdr.Parameters.AddWithValue("@RemarkID", r["RemarkID"]); 
          DB.ExecuteNonQuery(cmdr, "ConnDB");
        
        }
           
        }


    }
    protected void SaveComment(int CaseID, int CommentKind, string Comment)
    {
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            string sqlpoName = "dbo.usp_CaseUser_xAddCaseUserComment";
            //if (CaseID != 0) sqlpoName = "dbo.usp_CaseUser_xModifyCaseUser";


            using (SqlCommand cmd = new SqlCommand(sqlpoName, sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CaseID", CaseID);
                cmd.Parameters.AddWithValue("@CommentKind", CommentKind);
                cmd.Parameters.AddWithValue("@Comment", Comment);


                //if (CaseID != 0)
                //    cmd.Parameters.AddWithValue("@ModifyUserID", 1);
                //else
                cmd.Parameters.AddWithValue("@CreatedUserID", 1);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }


            }
        }
    
    }

    protected void BindContact()
    {

     
         

     }

    
      protected void BindComment( )
    {
        string CommentAreaFormat = "<div id=\"CommentDetail\"><select  name=\"ddlCommentKindddlCommentKind_{0}\"><option value=\"1\" >個案姓名、生日、戶籍地址等備註：</option><option value=\"2\" >聯絡人資料備註：</option><option value=\"3\" >身分備註：(上傳附件)</option><option value=\"4\" >父母新資料備註：</option><option value=\"5\" >死亡資料備註：</option><option value=\"6\" >其他：</option></select><input name=\"tbComment_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelPS\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddPS\"><img src=\"/images/icon_increase.png\" /></a></div>";

        SqlCommand cmd = new SqlCommand("select RemarkID, [RemarkType] ,[CaseRemark] from [C_CaseUserRemark] where CaseUserID=@CaseID  and LogicDel=0 order by RemarkID ");
          cmd.Parameters.AddWithValue("@CaseID", CaseID);
          DataTable dt =  DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            CommentAreaDIV.Controls.Add(GetControlFromTag(string.Format (CommentAreaFormat,r["RemarkID"],r["CaseRemark"],r["RemarkType"]  )));
        
        }
    

     }

       
    
    public static Control GetControlFromTag(string controlTag)
      {
          Page p = new Page();
          p.AppRelativeVirtualPath = "~/";
          Control control = p.ParseControl(controlTag);
          return control;
      }
}