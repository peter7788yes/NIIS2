using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SearchCheck_SearchLogDetailList : System.Web.UI.Page
{
    //protected string  CountyData = "[]";
    //protected string  TownData = "[]";
	protected int UserID=0;
	protected int SearchKind=0;
	protected string SearchDate="";
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.DisableTop(true);  
          // CountyData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
         // TownData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
int.TryParse(Request.Form["i"], out UserID);
int.TryParse(Request.Form["k"], out SearchKind);
SearchDate = Request.Form["d"] ?? "";
 
    }
}