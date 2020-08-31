using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VaccinationM_RegisterData : BasePage
{
    protected string CountyData = "[]";
    protected string TownData = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";
        if (SystemAreaCode.dict.ContainsKey("County"))
            CountyData = JsonConvert.SerializeObject(SystemAreaCode.dict["County"]);

        if (SystemAreaCode.dict.ContainsKey("Town"))
            TownData = JsonConvert.SerializeObject(SystemAreaCode.dict["Town"]);

    }
}