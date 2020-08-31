using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using WayneEntity;

public partial class System_PowerM_RolePowerSetting_AddPower : BasePage
{
    public string MyTreeData = "";
    public int OutRoleID = 0;
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public string RoleLevelName { get; set; }
    public int RoleCateID { get; set; }
    public string RoleCateName { get; set; }

    public System_PowerM_RolePowerSetting_AddPower()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        if (Page.PreviousPage != null)
        {
            if (PreviousPage.IsCrossPagePostBack == true)
            {
                System_PowerM_RolePowerSetting_Add page = (System_PowerM_RolePowerSetting_Add)PreviousPage;
                RoleName = PureString(page.RoleName);
                RoleDescription = PureString(page.RoleDescription);
                int RoleLevel = page.RoleLevel;
                RoleLevelName = PureString(page.RoleLevelName);

                int tmRoleCateID = GetNumber<int>("hfCateID");
                if (tmRoleCateID == 0)
                    tmRoleCateID = 1;

                RoleCateID = tmRoleCateID;
                RoleCateName = GetString("hfCateName");

                if (RoleName.Length==0)
                {
                    Response.Redirect("~/System/PowerM/RolePowerSetting_Add.aspx");
                }
                else
                {
                    MyTreeData = GetMenu();
                }

                var user = AuthServer.GetLoginUser();

                Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@OutRoleID", OutRoleID } };

                MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_PowerM_xAddRole"
                                                 , ref OutDict
                                                 , new Dictionary<string, object>()
                                                 {
                                                    { "@RoleName", RoleName },
                                                    { "@RoleLevel", RoleLevel },
                                                    { "@RoleDescription", RoleDescription },
                                                    { "@RoleCateID", RoleCateID },
                                                    { "@CreatedUserID", user.ID }
                                                });

                OutRoleID = (int)OutDict["@OutRoleID"];

                string script = "";

                if (OutRoleID <= 0)
                {
                    script = "<script>alert('儲存失敗');location.href = '/System/PowerM/RolePowerSetting.aspx';</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                }
                else
                {
                    hfR.Value = OutRoleID.ToString();
                }
            }
        }
        else if(this.IsPostBack ==true)
        {
            MyTreeData = GetMenu();

            DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_PowerM_xGetRoleByID"
                                                 , new Dictionary<string, object>()
                                                 {
                                                    { "@ID",  int.Parse(hfR.Value) }
                                                });
            RolePowerSettingVM VM = new RolePowerSettingVM();
            EntityS.FillModel(VM, dt);

            RoleName = VM.RoleName;
            RoleDescription = VM.RoleDescription;
            int RoleLevel = VM.RoleLevel;
            switch (VM.RoleLevel)
            {
                case 1:
                    RoleLevelName = "中央"; 
                    break;
                case 2:
                    RoleLevelName = "區管中心";
                    break;
                case 3:
                    RoleLevelName = "局";
                    break;
                case 4:
                    RoleLevelName = "所";
                    break;
            }
        }
        else
        {
            Response.Redirect("~/System/PowerM/RolePowerSetting_Add.aspx");
        }
           
    }

    private HttpCookie SetCookie(string name, string value, DateTime Expires)
    {
        //產生一個Cookie
        HttpCookie cookie = new HttpCookie("RoleName");
        //設定單值
        cookie.Value = HttpUtility.UrlEncode(RoleName);
        //設定過期日
        cookie.Expires = DateTime.Now.AddDays(1);

        return cookie;
    }

    private string GetMenu()
    {
        List<ModuleMenuVM> list = new List<ModuleMenuVM>();
        DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_SystemM_xGetModuleMenu"
                                          , new Dictionary<string, object>()
                                          {
                                                    { "@UserID", 1 },
                                                    { "@ModuleCateID", RoleCateID }
                                          });
        EntityS.FillModel(list, dt);
        return JsonConvert.SerializeObject(list);
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        int RoleID = 0;
        int.TryParse(hfR.Value, out RoleID);

        if (RoleID == 0)
        {
            string SC = "<script>alert('資料取得失敗');location.href = '/System/PowerM/RolePowerSetting_Add.aspx';</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", SC, false);
            return;
        }

        string script = "<script>alert('儲存失敗');</script>";

        if (hfV != null)
        {
            string jsonString = hfV.Value;

            List<RolePowerSettingPowerVM> list = new List<RolePowerSettingPowerVM>();
            list = JsonConvert.DeserializeObject<List<RolePowerSettingPowerVM>>(jsonString);


            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("RoleID");
            dt.Columns.Add("ModuleID");
            dt.Columns.Add("PowerString");


            foreach (var item in list.Where(item => item.PowerStringOut > 1))
            {
                dt.Rows.Add(0, RoleID, item.mId, item.PowerStringOut);
            }


            System.Transactions.TransactionOptions option = new System.Transactions.TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            option.Timeout = new TimeSpan(0, 10, 0);
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
                {
                            sc.Open();
                       
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sc))
                            {
                                bulkCopy.BulkCopyTimeout = 60;
                                bulkCopy.DestinationTableName = "dbo.R_RoleModule";
                                bulkCopy.WriteToServer(dt);
                            }

                            ts.Complete();
                       
                 
                }
            }
            //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            //{
            //    sc.Open();
            //    SqlTransaction st = sc.BeginTransaction();
            //    //SqlBulkCopy批次處理新增 沒有檢驗比對處理
            //    using (SqlBulkCopy sb = new SqlBulkCopy(sc, SqlBulkCopyOptions.Default, st))
            //    {
            //        //foreach (string columnName in GetMapping(stringSource, stringTarget, 'dbo.R_RoleModule'))
            //        //{
            //        //    sb.ColumnMappings.Add(new SqlBulkCopyColumnMapping(columnName, columnName));
            //        //}
            //        sb.DestinationTableName = "dbo.R_RoleModule";
            //        sb.WriteToServer(dt);
            //    }

            //    st.Commit();

            //}
            SystemRole.Update();
            script = "<style>body{display:none;}</style><script>alert('儲存成功');location.href='/System/PowerM/RolePowerSetting.aspx';</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

        }
        

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
       
    }
}