using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_View_VaccineOutBatch : BasePage
{
    public MyPowerVM DownloadPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineOut_View_VaccineOutBatch()
    {
        DownloadPower = base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.下載);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DownloadPower = base.AddPower(DownloadPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        UserVM user = AuthServer.GetLoginUser();

        DealHospitalName.Text = HttpUtility.HtmlEncode(Request.Form[DealHospitalName.UniqueID]);

        if (this.IsPostBack == false)
        {
            if (SystemCode.dict.ContainsKey("StockManagementM_FroIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_FroIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) FroIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            if (SystemCode.dict.ContainsKey("StockManagementM_MonIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_MonIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) MonIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            if (SystemCode.dict.ContainsKey("StockManagementM_OriFroIdx"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_OriFroIdx"];
                foreach (SystemCodeVM sc in SystemCodeList) OriFroIdx.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }
            if (SystemCode.dict.ContainsKey("StockManagementM_DealType"))
            {
                List<SystemCodeVM> SystemCodeList = SystemCode.dict["StockManagementM_DealType"];
                foreach (SystemCodeVM sc in SystemCodeList) DealType.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
            }

            int VaccOutBatchDataID;

            HttpUtility.HtmlEncode(int.TryParse(Request.QueryString["VaccOutBatchDataID"], out VaccOutBatchDataID));

            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_VaccineOut_xGetVaccineOutBatchData", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", VaccOutBatchDataID);
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
                DealType.SelectedValue = dt1.Rows[0]["DealType"].ToString();
                if (DealType.SelectedValue == "4")
                {
                    DealHospitalName.Visible = true;
                    DealHospitalID.Value = dt1.Rows[0]["DealHospital"].ToString();
                    int dealHospitalID = 0;
                    int.TryParse(DealHospitalID.Value, out dealHospitalID);
                    DealHospitalName.Text = SystemOrg.GetName(dealHospitalID);
                }
                Remark.Text = dt1.Rows[0]["Remark"].ToString();
                Num.Text = dt1.Rows[0]["Num"].ToString();
                TempHigh.Text = Convert.ToDouble(dt1.Rows[0]["TempHigh"]).ToString();
                FroIdx.SelectedValue = dt1.Rows[0]["FroIdx"].ToString();
                TempLow.Text = Convert.ToDouble(dt1.Rows[0]["TempLow"]).ToString();
                OriFroIdx.SelectedValue = dt1.Rows[0]["OriFroIdx"].ToString();
                MonIdx.SelectedValue = dt1.Rows[0]["MonIdx"].ToString();
                DownloadFile.PostBackUrl = "/Vaccine/StockManagementM/VaccineOut/DownloadFileOP.aspx?i=" + dt1.Rows[0]["FileInfoID"].ToString();
                DownloadFile.Text = dt1.Rows[0]["DisplayFileName"].ToString();
            }
        }
        DealType.Enabled = false;
        FroIdx.Enabled = false;
        OriFroIdx.Enabled = false;
        MonIdx.Enabled = false;
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VaccineOut.aspx");
    }
}