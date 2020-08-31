using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_CaseUserRemark_Add : BasePage
{
    public int ID = 0;
    //public UserVM user { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";


        if (Request.HttpMethod.Equals("POST"))
        {
            int.TryParse(Request.Form["c"], out ID);
            if (ID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            if (this.IsPostBack == false)
            {
                ddCate.Items.Add(new ListItem("個案姓名、生日、戶籍地址等備註：", "1"));
                ddCate.Items.Add(new ListItem("聯絡人資料備註：", "2"));
                ddCate.Items.Add(new ListItem("身分備註：(上傳附件)", "3"));
                ddCate.Items.Add(new ListItem("父母新資料備註：", "4"));
                ddCate.Items.Add(new ListItem("死亡資料備註：", "5"));
                ddCate.Items.Add(new ListItem("其他：", "6"));
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
      
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ID == 0)
        {
            string sc = "<script>alert('資料取得失敗');window.close();</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        int Chk = 0;
        string script = "";

        var user = AuthServer.GetLoginUser();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddCaseUserRemark", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", ID);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                cmd.Parameters.AddWithValue("@RemarkType", ddCate.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@CaseRemark", tbRemark.Text.Trim());
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        if (Chk > 0)
        {
            string message = string.Format("{0} {1} {2} (登錄者: {3} - {4})", ddCate.SelectedItem.Text, DateTime.Now.ToShortTaiwanDate("/"), tbRemark.Text.Trim(), user.UserName, user.RoleName);
            script = "<script>alert('儲存成功');window.opener.changeCaseUserRemark('" + message + "');window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}