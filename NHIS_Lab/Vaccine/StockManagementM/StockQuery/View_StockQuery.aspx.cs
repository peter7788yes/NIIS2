using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_StockQuery_View_StockQuery : BasePage
{
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_StockQuery_View_StockQuery()
    {
        PrintPower = base.AddPower("/Vaccine/StockManagementM/StockQuery/StockQuery.aspx", MyPowerEnum.列印);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintPower = base.AddPower(PrintPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
    }
}