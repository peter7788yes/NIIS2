using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SearchCheck_AuditSearchLogList : BasePage
{
      protected string  OrgData = "[]";
     protected string StartDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);  
         
        OrgData = SystemOrg.JsonList ;
        StartDate = (DateTime.Today.Year - 1911).ToString() + "-" + (DateTime.Today.Month < 10 ? "0" : "") + (DateTime.Today.Month).ToString();



    }
}