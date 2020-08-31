using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_BCGRecord : BasePage
{

    public MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));

    public List<MyPowerVM> PowerList = new List<MyPowerVM>();
    public Vaccination_RecordM_BCGRecord()
    {
        PowerList = base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        base.AllowHttpMethod("GET");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}