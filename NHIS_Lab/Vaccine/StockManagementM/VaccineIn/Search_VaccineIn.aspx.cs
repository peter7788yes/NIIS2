using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_Search_VaccineIn : BasePage
{
    public MyPowerVM DeletePower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM DownloadPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_VaccineIn_Search_VaccineIn()
    {
        list = base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx",MyPowerEnum.刪除,MyPowerEnum.查詢,MyPowerEnum.列印,MyPowerEnum.下載);
    }
    public string OrgName { get; set; }
    public string BatchType = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        DeletePower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        PrintPower = base.AddPower(list[2]);
        DownloadPower = base.AddPower(list[3]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);

        if (SystemCode.dict.ContainsKey("VaccineM_BatchType"))
            BatchType = JsonConvert.SerializeObject(SystemCode.dict["VaccineM_BatchType"].Where(item => item.EnumName != null));

    }
}