using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseMaintain_ucUserProfileList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        


    }

    public UseModule DisplayMode
    { get; set; }
}



public enum UseModule
{
    個案基本資料=0,
    個案訪查維護=1,
    登錄預種資料=2

}

