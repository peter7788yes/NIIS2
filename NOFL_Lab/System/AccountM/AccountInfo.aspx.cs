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

public partial class System_AccountM_AccountInfo : BasePage
{
    public System_AccountM_AccountInfo()
    {
        base.AddPower("/System/AccountM/AccountInfo.aspx", MyPowerEnum.修改);
    }

    UserVM user;
    UserVM user2;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        user = AuthServer.GetLoginUser();
        user2 = new UserVM();

        if (this.IsPostBack == false)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_AccountM_xGetUserInfoByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", user.ID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }


            EntityS.FillModel(user2, dt);

            lblAccount.Text = user.LoginName;
            tbDept.Text = user.RoleName;

            tbIDF.Text = user2.RocID;
            tbName.Text = user2.UserName;
            tbEmail.Text = user2.Email;
            tbTitle.Text = user2.Title;
            tbTel.Text = user2.PhoneNumber;
        }
    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
       string IDF = tbIDF.Text.Trim();
       string UserName = tbName.Text.Trim();
       string Email = tbEmail.Text.Trim();
       string Title = tbTitle.Text.Trim();
       string Tel = tbTel.Text.Trim();

       int Chk = 0;
       using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
       {
           using (SqlCommand cmd = new SqlCommand("dbo.usp_AccountM_xUpdateUserInfo", sc))
           {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@UserID", user.ID);
               cmd.Parameters.AddWithValue("@RocID", IDF);
               cmd.Parameters.AddWithValue("@UserName", UserName);
               cmd.Parameters.AddWithValue("@Email", Email);
               cmd.Parameters.AddWithValue("@Title", Title);
               cmd.Parameters.AddWithValue("@PhoneNumber", Tel);

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
           script = "<script>alert('儲存成功');</script>";
       }
       else
       {
           script = "<script>alert('儲存失敗');</script>";
       }

       Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        tbPWD.Enabled = true;
        tbPWD2.Enabled = true;
    }
}