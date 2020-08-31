using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class System_CodeM_SchoolCode : BasePage
{
    public string EnStateJson = "[]";
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    List<MyPowerVM> list = new List<MyPowerVM>();
    public System_CodeM_SchoolCode()
    {
        list = AddPower("/System/CodeM/SchoolCode.aspx", MyPowerEnum.查詢, MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = list[0];
        AddPower = list[1];
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("CodeM_SchoolCode_EnableState"))
        {
            var codes = SystemCode.dict["CodeM_SchoolCode_EnableState"];
            if (codes.Count > 0)
                EnStateJson = JsonConvert.SerializeObject(codes);

        }
    }
}