using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectSingleOrg : System.Web.UI.Page
{
    public string MyTreeData = "[]";

    protected void Page_Load(object sender, EventArgs e)
    {

        MyTreeData = SystemOrg.JsonList;
        
    }
}