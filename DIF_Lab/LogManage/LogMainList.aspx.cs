using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class LogManage_LogMainList : System.Web.UI.Page
{ 
    protected int LogCheckMainID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        int.TryParse(Request.Form["f"], out LogCheckMainID);
        //Response.Write(LogCheckMainID.ToString ());
    }
}