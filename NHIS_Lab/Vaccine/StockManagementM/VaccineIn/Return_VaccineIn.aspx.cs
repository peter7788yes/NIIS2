using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_Return_VaccineIn : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineIn_Return_VaccineIn()
    {
        NewPower = base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        if (this.IsPostBack == false)
        {
            if (SystemCode.dict.ContainsKey("StockManagementM_VaccReturn"))
            {
                VaccReturn.Items.Add(new ListItem("請選擇", "0"));
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_VaccReturn"];
                foreach (SystemCodeVM sc in SystemCodeList) VaccReturn.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
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

        int vaccReturn = int.Parse(VaccReturn.SelectedValue);
        string returnOther = ReturnOther.Text.Trim();
        int Success = 0;

        int VaccineOutBatchID;
        int VaccineOutID;

        int.TryParse(Request.QueryString["VaccineOutBatchID"], out VaccineOutBatchID);
        int.TryParse(Request.QueryString["VaccineOutID"], out VaccineOutID);

        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineIn_xAddReturnBatch", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VaccineOutBatchID", VaccineOutBatchID);
                cmd.Parameters.AddWithValue("@VaccReturn", vaccReturn);
                cmd.Parameters.AddWithValue("@ReturnOther", returnOther);
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
            script = "<script>alert('儲存成功');location.href = '/Vaccine/StockManagementM/VaccineIn/Login_VaccineIn.aspx?VaccineOutID=" + VaccineOutID + "';</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }
    protected void VaccReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (VaccReturn.SelectedValue == "4")
        {
            ReturnOther.Visible = true;
        }
        else
        {
            ReturnOther.Visible = false;
        }
    }
    private string CheckNeeded()
    {
        string rtn = "";
        string vaccReturn = VaccReturn.SelectedValue.ToString();
        

        if (vaccReturn == "0")
        {
            rtn += "退回原因:必填!\\n";
        }

        return rtn;

    }
    protected void CanCel_Click(object sender, EventArgs e)
    {
        int VaccineOutID;

        int.TryParse(Request.QueryString["VaccineOutID"], out VaccineOutID);

        Response.Redirect("Login_VaccineIn.aspx?VaccineOutID=" + VaccineOutID);
    }
    
}