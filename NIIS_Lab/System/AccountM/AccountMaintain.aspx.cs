using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;

public partial class System_AccountM_AccountMaintain : BasePage
{
    public int SystemPowerCateID = 0;
    public string OtherAttr = "";
    public string MyPowerData = "[]";
    public string MyRoleData = "[]";
    public string MyEnableData = "[]";
    public string MyCheckData = "[]";
    public string MyLogoutData = "[]";
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public System_AccountM_AccountMaintain()
    {
        PowerList = base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        var user = AuthServer.GetLoginUser();
        UC_OpenSelectSingleOrg.PageUrl = "/System/AccountM/AccountMaintain.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;

        MyRoleData = JsonConvert.SerializeObject(SystemRole.list.Where(item => item.RoleName != null));

        if (SystemPowerCate.list.Count > 0)
        {
            SystemPowerCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["SystemPowerCateID"]);
            var list = SystemPowerCate.list;
            if (SystemPowerCateID == 1)
            {
                MyPowerData = JsonConvert.SerializeObject(list);
            }
            else
            {
                OtherAttr = "ng-init=\"VM.selectSpc='4'\" ng-disabled='true'";
                MyPowerData = JsonConvert.SerializeObject(list.Where(item => item.ID == 4));
            }
        }

        if (SystemCode.dict.ContainsKey("AccountM_EnableState"))
            MyEnableData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_EnableState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_CheckState"))
            MyCheckData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_CheckState"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("AccountM_LogoutPeriod"))
            MyLogoutData = JsonConvert.SerializeObject(SystemCode.dict["AccountM_LogoutPeriod"].Where(item => item.EnumName != null));


    }
}