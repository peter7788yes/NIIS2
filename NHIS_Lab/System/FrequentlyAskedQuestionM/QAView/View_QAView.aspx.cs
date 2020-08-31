using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_FrequentlyAskedQuestionM_QAView_View_QAView : BasePage
{
    public System_FrequentlyAskedQuestionM_QAView_View_QAView()
    {
        base.AddPower("/System/FrequentlyAskedQuestionM/QAView/QAView.aspx", MyPowerEnum.瀏覽);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);
    }
}