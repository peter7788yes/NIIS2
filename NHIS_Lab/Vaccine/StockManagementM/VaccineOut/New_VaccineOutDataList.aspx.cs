using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_New_VaccineOutDataList : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineOut_New_VaccineOutDataList()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.新增, MyPowerEnum.刪除);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        DeletePower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
    }
}