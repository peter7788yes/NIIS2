using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_New_VaccineOutData : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineOut_New_VaccineOutData()
    {
        NewPower = base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.新增);
    }
    public string OrgName { get; set; }
    public string DealType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(NewPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);

        if (SystemCode.dict.ContainsKey("StockManagementM_DealType"))
            DealType = JsonConvert.SerializeObject(SystemCode.dict["StockManagementM_DealType"].Where(item => item.EnumName != null));
    }
}