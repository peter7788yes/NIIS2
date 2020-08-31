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

public partial class Vaccine_StockManagementM_VaccineUse_Modify_VaccineUseBatch : BasePage
{
    public MyPowerVM ModifyPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineUse_Modify_VaccineUseBatch()
    {
        ModifyPower = base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModifyPower = base.AddPower(ModifyPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        UserVM user = AuthServer.GetLoginUser();

        if (this.IsPostBack == false)
        {
            DealDate.Attributes.Add("onclick", "WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            DealDateImg.Attributes.Add("onclick", "WdatePicker({ el:'" + DealDate.ClientID + "',dateFmt: 'yyyMMdd', lang: 'zh-tw' })");
            if (SystemCode.dict.ContainsKey("StockManagementM_UseType"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_UseType"];
                foreach (SystemCodeVM sc in SystemCodeList) UseType.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }

            if (SystemCode.dict.ContainsKey("StockManagementM_UseUpdateReson"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_UseUpdateReson"];
                foreach (SystemCodeVM sc in SystemCodeList) UpdateReson.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
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
            if (dt1.Rows.Count > 0)
            {
                DealDate.Text = dt1.Rows[0]["DealDate"].ToString();
                Num.Text = dt1.Rows[0]["Num"].ToString();
                UseType.SelectedValue = dt1.Rows[0]["UseType"].ToString();
                UpdateReson.SelectedValue = dt1.Rows[0]["UpdateReson"].ToString();
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

        string dealDate = (Convert.ToInt32(DealDate.Text.Substring(0, 3)) + 1911).ToString() + "/" + DealDate.Text.Substring(3, 2) + "/" + DealDate.Text.Substring(5, 2); ;
        int updateReson = int.Parse(UpdateReson.SelectedValue);
        string num = Num.Text.Trim();
        int useType = int.Parse(UseType.SelectedValue);
        int CheckStorage = 0;
        int Success = 0; 

        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xUpdateSearchVaccineUseBatchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", VaccUseBatchDataID);
                cmd.Parameters.AddWithValue("@Num", num);
                cmd.Parameters.AddWithValue("@UseType", useType);
                cmd.Parameters.AddWithValue("@DealDate", dealDate);
                cmd.Parameters.AddWithValue("@UpdateReson", updateReson);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
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
            script = "<script>alert('儲存成功!');location.href = '/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx';</script>";
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
        string dealDate = DealDate.Text.Trim();
        string num = Num.Text.Trim();
        string storge = Storage.Text.Trim();
        string useType = UseType.SelectedValue.ToString();
        string updateReson = UpdateReson.SelectedValue.ToString();

        if (dealDate.Length == 0)
        {
            rtn += "領用日期:必填!\\n";
        }
        else if (RegularExpressions.IsDate((Convert.ToInt32(DealDate.Text.Substring(0, 3)) + 1911).ToString() + "-" + DealDate.Text.Substring(3, 2) + "-" + DealDate.Text.Substring(5, 2)) == false)
        {
            rtn += "領用日期:日期有誤!\\n";
        }

        if (num.Length == 0)
        {
            rtn += "領用數量:必填!\\n";
        }
        else if (RegularExpressions.IsPlusInt(num) == false)
        {
            rtn += "領用數量:請輸入正整數!\\n";
        }

        if (useType == "0")
        {
            rtn += "領用用途:必填!\\n";
        }

        if (updateReson == "0")
        {
            rtn += "修改原因:必填!\\n";
        }        

        return rtn;

    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VaccineUse.aspx");
    }
}