using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_Return_VaccineOut : BasePage
{
    public Vaccine_StockManagementM_VaccineOut_Return_VaccineOut()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.瀏覽);
    }
    public string VaccReturn = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (SystemCode.dict.ContainsKey("StockManagementM_VaccReturn"))
            VaccReturn = JsonConvert.SerializeObject(SystemCode.dict["StockManagementM_VaccReturn"].Where(item => item.EnumName != null));
    }
}