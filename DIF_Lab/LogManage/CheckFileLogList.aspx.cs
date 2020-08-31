using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class LogManage_CheckFileLogList : BasePage
{
    //protected string  CountyData = "[]";
    //protected string  TownData = "[]";
    protected string EndDate = "";
    protected string StartDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);  
          // CountyData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
         // TownData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());

        StartDate = DateTime.Today.AddDays(-6).ToString("yyyy/MM/dd");
        EndDate = DateTime.Today.ToString("yyyy/MM/dd");
    }
}