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

public partial class System_FrequentlyAskedQuestionM_QAView_QAView : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_FrequentlyAskedQuestionM_QAView_QAView()
    {
        SearchPower = base.AddPower("/System/FrequentlyAskedQuestionM/QAView/QAView.aspx", MyPowerEnum.查詢);
    }
    public string QAViewDateStatus = "[]";
    public string QnAType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("FrequentlyAskedQuestionM_QAViewDate"))
            QAViewDateStatus = JsonConvert.SerializeObject(SystemCode.dict["FrequentlyAskedQuestionM_QAViewDate"].Where(item => item.EnumName != null));

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAType_xGetQnATypeAllData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        QnAType = JsonConvert.SerializeObject(ds.Tables[0]);
    }
}