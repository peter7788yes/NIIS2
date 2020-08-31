using Newtonsoft.Json;
using System;
using System.Linq;

public partial class System_CodeM_MedicalCode : BasePage
{
    public string BgStateJson = "[]";
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_CodeM_MedicalCode()
    {
        SearchPower = base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("ParameterM_LocationSetting_BusinessState"))
        {
            var codes = SystemCode.dict["ParameterM_LocationSetting_BusinessState"];
            if(codes.Count()>0)
                BgStateJson = JsonConvert.SerializeObject(codes);

        }
    }
}