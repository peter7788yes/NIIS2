using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] bb = new byte[] { 1, 2, 3 };
        ReportResultVM VM = new ReportResultVM();
        VM.ResultState = 1;
        VM.ResultFile = bb;
        var x =JsonConvert.DeserializeObject<ReportResultVM>(JsonConvert.SerializeObject(VM));
        Response.Write("");
        Response.End();
    }
}