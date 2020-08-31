using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_SelectAgency : BasePage
{
    public Vaccine_StockManagementM_VaccineIn_SelectAgency()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }
}