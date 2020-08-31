using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_VaccineParameterM_OrgVaccSet_OrgVaccSet : BasePage
{
    public string VaccineStatus = "[]";

    public MyPowerVM ModeifyPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();    

    public Vaccine_VaccineParameterM_OrgVaccSet_OrgVaccSet()
    {
        list = base.AddPower("/Vaccine/VaccineParameterM/OrgVaccSet/OrgVaccSet.aspx",MyPowerEnum.修改,MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ModeifyPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        if (SystemCode.dict.ContainsKey("VaccineM_VaccineStatus"))
            VaccineStatus = JsonConvert.SerializeObject(SystemCode.dict["VaccineM_VaccineStatus"].Where(item => item.EnumName != null));
    }
}