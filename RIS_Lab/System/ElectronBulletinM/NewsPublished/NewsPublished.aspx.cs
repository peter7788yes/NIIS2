using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_NewsPublishedM_NewsPublished : BasePage
{
    public MyPowerVM NewPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public System_NewsPublishedM_NewsPublished()
    {
        list = base.AddPower("/System/ElectronBulletinM/NewsPublished/NewsPublished.aspx", MyPowerEnum.新增, MyPowerEnum.查詢);
    }
    public string PublishedStatus = "[]";
    public string OrgName { get; set; }
    public string OrgID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        NewPower = base.AddPower(list[0]);
        SearchPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);

        if (SystemCode.dict.ContainsKey("NewsPublishedM_PublishedStatus"))
            PublishedStatus = JsonConvert.SerializeObject(SystemCode.dict["NewsPublishedM_PublishedStatus"].Where(item => item.EnumName != null));

    }
}