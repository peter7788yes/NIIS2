using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineUse_VaccineUse : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineUse_VaccineUse()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.新增, MyPowerEnum.刪除, MyPowerEnum.查詢, MyPowerEnum.列印);
    }
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    public string UseType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        DeletePower = base.AddPower(list[1]);
        SearchPower = base.AddPower(list[2]);
        PrintPower = base.AddPower(list[3]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
        OrgID = string.Format("{0}", user.OrgID);

        if (SystemCode.dict.ContainsKey("StockManagementM_UseType"))
            UseType = JsonConvert.SerializeObject(SystemCode.dict["StockManagementM_UseType"].Where(item => item.EnumName != null));

    }
}