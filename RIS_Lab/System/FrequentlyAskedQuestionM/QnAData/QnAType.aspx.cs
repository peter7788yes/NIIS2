using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_FrequentlyAskedQuestionM_QnAData_QnAType : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_FrequentlyAskedQuestionM_QnAData_QnAType()
    {
        list = base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }
    public string TypeStatus = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        
        if (SystemCode.dict.ContainsKey("FrequentlyAskedQuestionM_QAMaintenanceQATypeStatus"))
            TypeStatus = JsonConvert.SerializeObject(SystemCode.dict["FrequentlyAskedQuestionM_QAMaintenanceQATypeStatus"].Where(item => item.EnumName != null));
    }
    protected void Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("QnAData.aspx");
    }
}