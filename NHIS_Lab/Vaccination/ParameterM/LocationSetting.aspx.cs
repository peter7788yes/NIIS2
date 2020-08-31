using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_ParameterM_LocationSetting : BasePage
{
    public string AgStateJson = "";

    public MyPowerVM SearchPower =new MyPowerVM("",default(MyPowerEnum));

    public Vaccination_ParameterM_LocationSetting()
    {
        SearchPower = base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx",MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        if (SystemCode.dict.ContainsKey("ParameterM_LocationSetting_AgencyState"))
        {
            var codes = SystemCode.dict["ParameterM_LocationSetting_AgencyState"];
            AgStateJson = JsonConvert.SerializeObject(codes);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}