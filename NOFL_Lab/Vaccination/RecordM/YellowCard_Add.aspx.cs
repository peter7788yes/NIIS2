using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Vaccination_RecordM_YellowCard_Add :BasePage
{
    public int ID = 0;
    public UserVM user { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
        user = AuthServer.GetLoginUser();

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
                tbUser.Text = user.UserName;
                tbDate.Text = DateTime.Now.ToShortTaiwanDate();
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

        RadioButton selected = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        int ApplyType=0;

        if (selected != null)
        {
            ApplyType=selected.Text == "黃卡" ? 1 : 2;
         }

        //string TaiwanDate =tbDate.Text.Trim();
        //string[] ary = TaiwanDate.Split('/');
        //DateTime ApplyDate = new DateTime(int.Parse(ary[0])+1911,int.Parse(ary[1]),int.Parse(ary[2]));
        DateTime ApplyDate = default(DateTime);
        DateTime.TryParseExact(tbDate.Text.Trim().RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out ApplyDate);

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddApplyYellowCardRecord", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", ID);
                cmd.Parameters.AddWithValue("@ApplyDate", ApplyDate);
                cmd.Parameters.AddWithValue("@ApplyLocation", tbLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                cmd.Parameters.AddWithValue("@ApplyType", ApplyType);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

      
        if (Chk > 0)
        {
            string message = string.Format("{0} {1} (登錄者: {2} - {3})",tbDate.Text.Trim() , tbLocation.Text.Trim(), user.UserName, user.OrgName);
            script = "<script>alert('儲存成功');window.opener.changeYellowCardUpdateRecord('" + message + "');window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}