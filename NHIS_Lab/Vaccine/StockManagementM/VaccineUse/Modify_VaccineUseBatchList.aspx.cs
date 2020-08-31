using AlanTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineUse_Modify_VaccineUseBatchList : BasePage
{
   public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

   public Vaccine_StockManagementM_VaccineUse_Modify_VaccineUseBatchList()
    {
        NewPower = base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        UserVM user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            if (SystemCode.dict.ContainsKey("StockManagementM_UseType"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_UseType"];
                foreach (SystemCodeVM sc in SystemCodeList) UseType.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }

            int VaccUseBatchDataID;
            HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["VaccUseBatchDataID"], out VaccUseBatchDataID));

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xGetVaccineUseBatchData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", VaccUseBatchDataID);
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }

                }
            }
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            if (dt.Rows.Count > 0)
            {
                VaccineID.Text = dt.Rows[0]["VaccineID"].ToString();
                BatchType.Text = dt.Rows[0]["BatchType"].ToString();
                BatchID.Text = dt.Rows[0]["BatchID"].ToString();
                FormDrug.Text = dt.Rows[0]["FormDrug"].ToString();
                Storage.Text = dt.Rows[0]["Storage"].ToString();
            }
            if(dt1.Rows.Count > 0)
            {
                Num.Text = dt1.Rows[0]["Num"].ToString();
                UseType.SelectedValue = dt1.Rows[0]["UseType"].ToString();
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
        int VaccUseBatchDataID;
        HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["VaccUseBatchDataID"], out VaccUseBatchDataID));

        string num = Num.Text.Trim();
        int useType = int.Parse(UseType.SelectedValue);

        int CheckStorage = 0;
        int Success = 0;       

        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xUpdateTempVaccineUseBatchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", VaccUseBatchDataID);
                cmd.Parameters.AddWithValue("@Num", num);
                cmd.Parameters.AddWithValue("@UseType", useType);
                cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@CheckStorage", CheckStorage);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                    CheckStorage = (int)sp.Value;
                    Success = (int)sp1.Value;
                }
            }
        }


        if (Success > 0)
        {
            int VaccineUseID;
            HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["ID"], out VaccineUseID));
            script = "<script>alert('儲存成功!');location.href = '/Vaccine/StockManagementM/VaccineUse/New_VaccineUseDataList.aspx?ID=" + VaccineUseID + "';</script>";
        }
        else
        {
            if (CheckStorage > 0)
            {
                script = "<script>alert('庫存量不足!');</script>";
            }
            else
            {
                script = "<script>alert('儲存失敗!');</script>";
            }
        }

        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    }

    private string CheckNeeded()
    {
        string rtn = "";
        string num = Num.Text.Trim();
        string storge = Storage.Text.Trim();
        string useType = UseType.SelectedValue.ToString();

        if (num.Length == 0)
        {
            rtn += "領用數量:必填!\\n";
        }
        else if (RegularExpressions.IsPlusInt(num) == false)
        {
            rtn += "領用數量:請輸入正整數!\\n";
        }
        else if (int.Parse(storge) - int.Parse(num) < 0)
        {
            rtn += "領用數量:請小於或等於庫目前庫存量!\\n";
        }

        if (useType == "0")
        {
            rtn += "領用用途:必填!\\n";
        }

        return rtn;

    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        int VaccineUseID;
        HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["ID"], out VaccineUseID));
        Response.Redirect("New_VaccineUseDataList.aspx?ID=" + VaccineUseID);
    }
}