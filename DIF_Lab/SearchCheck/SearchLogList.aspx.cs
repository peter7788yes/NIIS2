using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SearchCheck_SearchLogList : BasePage
{
    //protected string  CountyData = "[]";
     protected string  OrgData = "[]";
     protected string StartDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);  
          // CountyData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
         // TownData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());

         
       // List<SystemOrgVM> list = SystemOrg.list;
        //SystemOrg.Update(); 
        OrgData = SystemOrg.JsonList ;

        StartDate=(DateTime.Today.Year - 1911).ToString() + "-" + (DateTime.Today.Month < 10 ? "0" : "") + (DateTime.Today.Month).ToString();

    }
}