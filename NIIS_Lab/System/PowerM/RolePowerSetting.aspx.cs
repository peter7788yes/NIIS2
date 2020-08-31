using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Configuration;

public partial class RolePowerSetting : BasePage
{
    public int RoleCateID = 0;
    public string OtherAttr = "";
    public string MyPowerData = "[]";
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";

    public RolePowerSetting()
    {
        AddPower = base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        AddPower = base.GetPower(AddPower);

        if (SystemPowerCate.list.Count > 0)
        {
            RoleCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["RoleCateID"]);
            var list = SystemPowerCate.list;
            if (RoleCateID == 1)
            {
                MyPowerData = JsonConvert.SerializeObject(list);
            }
            else
            {
                OtherAttr = "ng-init=\"VM.selectSpc='4'\" ng-disabled='true'";
                MyPowerData = JsonConvert.SerializeObject(list.Where(item => item.ID == RoleCateID));
            }
        }

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<RolePowerSettingVM>(), "ConnUser", "dbo.usp_PowerM_xGetRoleListByRoleCateID",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                            { "@RoleCateID", 0 },
        //                                            { "@pgNow", 1},
        //                                            { "@pgSize", 10 }
        //                                });

    }
}