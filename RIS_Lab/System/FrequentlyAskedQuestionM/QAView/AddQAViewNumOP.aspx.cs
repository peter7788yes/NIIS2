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

public partial class System_FrequentlyAskedQuestionM_QAView_AddQAViewNumOP : BasePage
{
    public System_FrequentlyAskedQuestionM_QAView_AddQAViewNumOP()
    {
        base.AddPower("/System/FrequentlyAskedQuestionM/QAView/QAView.aspx", MyPowerEnum.瀏覽);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int CheckID;
        int ViewNum;

        int.TryParse(Request.Form["CheckID"], out CheckID);
        int.TryParse(Request.Form["ViewNum"], out ViewNum);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QAView_xAddQAViewNum", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", CheckID);
                cmd.Parameters.AddWithValue("@ViewNum", ViewNum);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
    }
}