using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_FrequentlyAskedQuestionM_QnAData_New_QnAType : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

    public System_FrequentlyAskedQuestionM_QnAData_New_QnAType()
    {
        NewPower = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();
        string scriptmessage = "";
        string message = CheckNeeded();
        if (message.Length > 0)
        {
            scriptmessage = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", scriptmessage, false);
            return;
        }

        string typeName = TypeName.Text.Trim();
        
        int CheckNum = 0;
        int Success = 0;

        DataSet ds = new DataSet();
        string script = "";
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAType_xAddQnATypeData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TypeName", typeName);
                cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
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
            Response.Write("<script>alert('問題類別名稱已經存在!');</script>");
        }
        else
        {
            if (Success == 1)
            {
                string PageView = "";
                int ID = 0;

                PageView = Request.QueryString["PageView"];
                int.TryParse(Request.QueryString["I"], out ID);

                if (PageView == "add")
                {
                    script = "<script>alert('儲存成功');location.href = '/System/FrequentlyAskedQuestionM/QnAData/QnAType.aspx';</script>";
                }
                else if (PageView == "add1")
                {
                    script = "<script>alert('儲存成功');location.href = '/System/FrequentlyAskedQuestionM/QnAData/New_QnAData.aspx';</script>";
                }
                else if (PageView == "modify")
                {
                    script = "<script>alert('儲存成功');location.href = '/System/FrequentlyAskedQuestionM/QnAData/Modify_QnAData.aspx?I=" + ID + "';</script>";
                }
            }
            else
            {
                script = "<script>alert('儲存失敗');</script>";
            }
        }
        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    private string CheckNeeded()
    {
        string rtn = "";
        string typeName = TypeName.Text.Trim();

        if (typeName.Length == 0)
        {
            rtn += "問題類別:必填!\\n";
        }

        return rtn;

    }
    protected void Return_Click(object sender, EventArgs e)
    {
        string script = "";
        string PageView = "";
        int ID = 0;

        PageView = Request.QueryString["PageView"];
        int.TryParse(Request.QueryString["I"], out ID);

        if (PageView == "add")
        {
            script = "<script>location.href = '/System/FrequentlyAskedQuestionM/QnAData/QnAType.aspx';</script>";
        }
        else if (PageView == "add1")
        {
            script = "<script>location.href = '/System/FrequentlyAskedQuestionM/QnAData/New_QnAData.aspx';</script>";
        }
        else if (PageView == "modify")
        {
            script = "<script>location.href = '/System/FrequentlyAskedQuestionM/QnAData/Modify_QnAData.aspx?I=" + ID + "';</script>";
        }
        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "location.href", script, false);
    }
}