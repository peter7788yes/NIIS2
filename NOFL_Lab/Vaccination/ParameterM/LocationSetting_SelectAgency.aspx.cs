using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_ParameterM_LocationSetting_SelectAgency : BasePage
{

    public Vaccination_ParameterM_LocationSetting_SelectAgency()
    {
        base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }
}