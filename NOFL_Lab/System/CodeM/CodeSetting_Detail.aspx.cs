using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_CodeM_CodeSetting_Detail : BasePage
{
    public int ID { get; set; }

    public MyPowerVM AddPower { get; set; }

    public System_CodeM_CodeSetting_Detail()
    {
        var dict = base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.新增);
        AddPower = dict[MyPowerEnum.新增];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int ID = 0;
        int.TryParse(Request["i"], out ID);
        this.ID = ID;
    }

    
}