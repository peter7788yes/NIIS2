using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_MessageViewM_MessageView : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_MessageViewM_MessageView()
    {
        SearchPower = base.AddPower("/System/ElectronBulletinM/MessageView/MessageView.aspx", MyPowerEnum.查詢);
    }
    public string SearchDate = "[]";
    public string SearchContentFile = "[]";
    public string OrgData = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (SystemCode.dict.ContainsKey("MessageViewM_MessageDate"))
            SearchDate = JsonConvert.SerializeObject(SystemCode.dict["MessageViewM_MessageDate"].Where(item => item.EnumName != null));

        if (SystemCode.dict.ContainsKey("MessageViewM_ContentFile"))
            SearchContentFile = JsonConvert.SerializeObject(SystemCode.dict["MessageViewM_ContentFile"].Where(item => item.EnumName != null));

        OrgData = JsonConvert.SerializeObject(SystemOrg.list.Where(item => item.OrgName != null));
    }
}