using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class CaseMaintain_MergedUserList : BasePage
{
    //protected string  CountyData = "[]";
    //protected string  TownData = "[]";
    protected string OrgID = "";
    protected string OrgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);   
        UserVM u = AuthServer.GetLoginUser(); 
        OrgID = u.OrgID.ToString();
        OrgName = u.OrgName;
    }
}