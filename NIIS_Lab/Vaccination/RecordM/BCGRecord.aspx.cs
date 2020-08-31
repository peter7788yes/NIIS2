using System;
using System.Collections.Generic;

public partial class Vaccination_RecordM_BCGRecord : BasePage
{
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public int DefaultOrgID = 0;
    public string DefaultOrgName = "";

    public Vaccination_RecordM_BCGRecord()
    {
        PowerList = base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
       
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        var user = AuthServer.GetLoginUser();
        UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/RecordM/BCGRecord.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;
        DefaultOrgID = user.OrgID;
        DefaultOrgName = user.OrgName;
     
    }

   
}