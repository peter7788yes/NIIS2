using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_FrequentlyAskedQuestionM_QnAData_Modify_QnAType : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_FrequentlyAskedQuestionM_QnAData_Modify_QnAType()
    {
        list = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.修改, MyPowerEnum.刪除);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(list[0]);
        DeletePower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        if (this.IsPostBack == false)
        {
            int ID;

            int.TryParse(Request.QueryString["I"], out ID);

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_QnAType_xGetQnATypeData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
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
                TypeName.Text = dt.Rows[0]["Name"].ToString();
                string status = dt.Rows[0]["TypeStatus"].ToString();
                if (status == "1")
                {
                    Status1.Checked = true;
                }
                else if (status == "2")
                {
                    Status2.Checked = true;
                }
                QaNum.Text = dt.Rows[0]["QaNum"].ToString();
            }

        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();
        string script = "";
        string message = CheckNeeded();
        if (message.Length > 0)
        {
            script = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
            return;
        }
        
        string typeName = TypeName.Text.Trim();
        int status = 0;

        if (Status1.Checked == true)
        {
            status = 1;
        }
        if (Status2.Checked == true)
        {
            status = 2;
        }

        int ID = 0;
        int Success = 0;
        int CheckNum = 0;

        int.TryParse(Request.QueryString["I"], out ID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAType_xModifyQnATypeData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@TypeName", typeName);
                cmd.Parameters.AddWithValue("@TypeStatus", status);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@CheckNum", CheckNum);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                    CheckNum = (int)sp.Value;
                    Success = (int)sp1.Value;
                }
            }
        }

        if (CheckNum == 1)
        {
            script = "<script>alert('問題類別名稱已經存在!');</script>";
        }
        else
        {
            if (Success > 0)
            {
                script = "<script>alert('儲存成功!');location.href = '/System/FrequentlyAskedQuestionM/QnAData/QnAType.aspx';</script>";
            }
            else
            {
                script = "<script>alert('儲存失敗!');</script>";
            }
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        string script = "";
        string message = "";

        if (int.Parse(QaNum.Text) > 0)
        {
            message += "問答集數量不等於0!";
        }
        if (message.Length > 0)
        {
            script = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
            return;
        }

        int ID;
        int Success = 0;

        int.TryParse(Request.QueryString["I"], out ID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAType_xDeleteQnATypeData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                    Success = (int)sp.Value;
                }
            }
        }
        if (Success > 0)
        {
            script = "<script>alert('刪除成功!');location.href = '/System/FrequentlyAskedQuestionM/QnAData/QnAType.aspx';</script>";
        }
        else
        {
            script = "<script>alert('刪除失敗!');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    private string CheckNeeded()
    {
        string rtn = "";
        string typeName = TypeName.Text.Trim();
        int status = 0;

        if (typeName.Length == 0)
        {
            rtn += "問題類別:必填!\\n";
        }

        if (Status1.Checked == true)
        {
            status = 1;
        }
        if (Status2.Checked == true)
        {
            status = 2;
        }

        if (status == 0)
        {
            rtn += "狀態:必填!\\n";
        }

        return rtn;

    }
    protected void Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("QnAType.aspx");
    }
    
}