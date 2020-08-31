using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_Login_VaccineIn : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineIn_Login_VaccineIn()
    {
        NewPower = base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.新增);
    }
    public string DealStatus = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("StockManagementM_DealStatus"))
            DealStatus = JsonConvert.SerializeObject(SystemCode.dict["StockManagementM_DealStatus"].Where(item => item.EnumName != null));
    }
}