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

public partial class System_FrequentlyAskedQuestionM_QnAData_QnAData : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_FrequentlyAskedQuestionM_QnAData_QnAData()
    {
        list = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }
    public string PublishedStatus = "[]";
    public string QnAType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("FrequentlyAskedQuestionM_QAMaintenanceStatus"))
            PublishedStatus = JsonConvert.SerializeObject(SystemCode.dict["FrequentlyAskedQuestionM_QAMaintenanceStatus"].Where(item => item.EnumName != null));

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