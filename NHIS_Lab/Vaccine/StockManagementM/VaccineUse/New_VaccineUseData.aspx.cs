using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineUse_New_VaccineUseData : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineUse_New_VaccineUseData()
    {
        NewPower = base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.新增);
    }
    public string OrgName { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
    }
}