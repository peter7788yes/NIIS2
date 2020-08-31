using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SearchCheck_HistoryAuditLogListByUser : BasePage
{
    protected int UserID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);
        int.TryParse(Request.Form["i"], out UserID);


    }
}