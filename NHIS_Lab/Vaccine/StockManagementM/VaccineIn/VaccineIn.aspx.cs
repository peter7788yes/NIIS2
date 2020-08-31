using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_VaccineIn : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineIn_VaccineIn()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }
    public string OrgName { get; set; }
    public bool Check { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);

        int SelectOrgID = 0;

        if (user.OrgID == SelectOrgID)
        {
            Check = true;
        }
        else
        {
            Check = false;
        }
    }
}