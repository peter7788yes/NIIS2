using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Vaccination_RecordM_StudentReRecord : BasePage
{
    public string sAry = "[]";

    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public int DefaultOrgID = 0;
    public string DefaultOrgName = "";

    public Vaccination_RecordM_StudentReRecord()
    {
        PowerList = base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.上傳);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UploadPower = base.GetPower(PowerList[2]);

        var user = AuthServer.GetLoginUser();
        UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/RecordM/StudentReRecord.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;
        DefaultOrgID = user.OrgID;
        DefaultOrgName = user.OrgName;

        if (SystemElementarySchool.list.Count > 0)
            sAry = JsonConvert.SerializeObject(SystemElementarySchool.list.Where(item => item.OrgID == user.OrgID));

    }

    
}