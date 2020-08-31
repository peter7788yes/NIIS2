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

public partial class System_CodeM_CodeSetting_Add : BasePage
{

    

    int ID = 0;

    public System_CodeM_CodeSetting_Add()
    {
        base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["i"], out ID);

        if (ID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
        }

        if (this.IsPostBack == false)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetEnabledSystemCodeCateByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            SystemCodeVM VM = new SystemCodeVM();
            EntityS.FillModel(VM, dt);
            lblCate.Text = VM.CodeDescription;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        
        int EnumValue = 0;
        string EnumName = tbName.Text.Trim();
        int OrderNumber = 0;

        int.TryParse(tbValue.Text, out EnumValue);
        int.TryParse(tbSort.Text, out OrderNumber);
        
        UserVM user =AuthServer.GetLoginUser();
        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xAddSystemCode", sc))
            {
               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemCodeCateID", ID);
                cmd.Parameters.AddWithValue("@EnumValue", EnumValue);
                cmd.Parameters.AddWithValue("@EnumName", EnumName);
                cmd.Parameters.AddWithValue("@CanEdit", 0);
                cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/System/CodeM/CodeSetting_Detail.aspx?i="+ID+"';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
}