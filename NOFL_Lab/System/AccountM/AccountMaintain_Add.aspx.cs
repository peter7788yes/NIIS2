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

public partial class System_AccountM_AccountMaintain_Add : BasePage
{

    public System_AccountM_AccountMaintain_Add()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";



        if (this.IsPostBack == false)
        {
            var user = AuthServer.GetLoginUser();

            tbDept.Text = user.RoleName;
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