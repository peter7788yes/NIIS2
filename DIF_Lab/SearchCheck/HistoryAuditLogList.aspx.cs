using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SearchCheck_HistoryAuditLogList : System.Web.UI.Page
{
    //protected string  CountyData = "[]";
     protected string  OrgData = "[]";
     protected string StartDate = "";
     protected string EndDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.DisableTop(true);  
          // CountyData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
         // TownData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());

         
       // List<SystemOrgVM> list = SystemOrg.list;
        //SystemOrg.Update(); 
        OrgData = SystemOrg.JsonList ;

      EndDate   = (DateTime.Today.Year - 1911).ToString() + "-" + (DateTime.Today.Month < 10 ? "0" : "") + (DateTime.Today.Month).ToString();
      StartDate = (DateTime.Today.AddMonths(-5).Year - 1911).ToString() + "-" + (DateTime.Today.AddMonths(-5).Month < 10 ? "0" : "") + (DateTime.Today.AddMonths(-5).Month).ToString();


    }
}