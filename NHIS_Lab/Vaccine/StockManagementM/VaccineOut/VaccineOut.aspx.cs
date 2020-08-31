using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_VaccineOut : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DownloadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineOut_VaccineOut()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.新增, MyPowerEnum.刪除, MyPowerEnum.查詢, MyPowerEnum.列印, MyPowerEnum.下載);
    }
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    public string BatchType = "[]";
    public string DealStatus = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        DeletePower = base.AddPower(list[1]);
        SearchPower = base.AddPower(list[2]);
        PrintPower = base.AddPower(list[3]);
        DownloadPower = base.AddPower(list[4]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);
        OrgID = string.Format("{0}", user.OrgID);

        if (SystemCode.dict.ContainsKey("VaccineM_BatchType"))
            BatchType = JsonConvert.SerializeObject(SystemCode.dict["VaccineM_BatchType"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("StockManagementM_DealStatus"))
            DealStatus = JsonConvert.SerializeObject(SystemCode.dict["StockManagementM_DealStatus"].Where(item => item.EnumName != null));

    }
}