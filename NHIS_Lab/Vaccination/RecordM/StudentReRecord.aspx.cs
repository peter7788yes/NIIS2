using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_StudentReRecord : BasePage
{
    public string sAry = "[]";


    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UploadPower = new MyPowerVM("", default(MyPowerEnum));

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();

    public Vaccination_RecordM_StudentReRecord()
    {
        PowerList = base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.上傳);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UploadPower = base.GetPower(PowerList[0]);

        base.AllowHttpMethod("GET");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        if (SystemElementarySchool.list.Count > 0)
            sAry = JsonConvert.SerializeObject(SystemElementarySchool.list);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}