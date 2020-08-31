using Newtonsoft.Json;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_PowerM_RolePowerSetting_UpdatePower : BasePage
{
    public string MyTreeData = "[]";
    public string tbAry = "[]";
    int ID = 0;


    public int ModuleCateID = 0;
    public int OutRoleID = 0;
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public string RoleLevelName { get; set; }

    public MyPowerVM ViewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public System_PowerM_RolePowerSetting_UpdatePower()
    {
        PowerList = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        ViewPower = base.GetPower(PowerList[0]);
        UpdatePower = base.GetPower(PowerList[1]);

        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";
      



        if (Request.HttpMethod.Equals("POST"))
        {
                int.TryParse(Request.Form["i"], out ID);
                int.TryParse(Request.Form["mc"], out ModuleCateID);

                MyTreeData = GetMenu();

                if (this.IsPostBack == false )
                {
                        hfR.Value = ID.ToString();
                        if (ID == 0 || ModuleCateID == 0)
                        {
                            string script = "<script>alert('資料取得失敗');location.href='/System/PowerM/RolePowerSetting.aspx';</script><style>body{display:none;}</style>";
                            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                            return;
                        }
                }



                DataSet ds = new DataSet();

                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetRoleAndModuleByRoleID", sc))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleID", ID);
                        cmd.Parameters.AddWithValue("@RoleCateID", ModuleCateID);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            sc.Open();
                            da.Fill(ds);
                        }
                    }
                }

                RolePowerSettingVM VM = new RolePowerSettingVM();

                List<RolePowerSettingPowerVM> list = new List<RolePowerSettingPowerVM>();
                EntityS.FillModel(VM, ds.Tables[0]);
                EntityS.FillModel(list, ds.Tables[1]);

                tbAry = JsonConvert.SerializeObject(list);
                RoleName = VM.RoleName;
                RoleDescription = VM.RoleDescription;


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
            Response.Write("");
            Response.End();
        }
       
    }


    private string GetMenu()
    {
        List<ModuleMenuVM> list = new List<ModuleMenuVM>();
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xGetModuleMenuByModuleCateID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", 1);
                cmd.Parameters.AddWithValue("@ModuleCateID", ModuleCateID);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        EntityS.FillModel(list, dt);
        return JsonConvert.SerializeObject(list);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int RoleID = 0;
        int.TryParse(hfR.Value, out RoleID);

        if (RoleID == 0)
        {
            string SC = "<script>alert('資料取得失敗');location.href = '/System/PowerM/RolePowerSetting.aspx';</script><style>body{display:none;}</style>";
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
                dt.Rows.Add(0,RoleID, item.mId, item.PowerStringOut);
            }

            System.Transactions.TransactionOptions option = new System.Transactions.TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            option.Timeout = new TimeSpan(0, 10, 0);
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
                {
                    sc.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xDeleteRoleModuleByRoleID", sc))
                    {
                      
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RoleID", RoleID);
                            cmd.ExecuteNonQuery();

                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sc))
                            {
                                bulkCopy.BulkCopyTimeout = 60;
                                bulkCopy.DestinationTableName = "dbo.R_RoleModule";
                                bulkCopy.WriteToServer(dt);
                            }

                            ts.Complete();
                       
                    }
                }
            }

            script = "<script>alert('儲存成功');location.href='/System/PowerM/RolePowerSetting.aspx';</script><style>body{display:none;}</style>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

        }


        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }

}