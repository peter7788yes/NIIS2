using Newtonsoft.Json;
using System;
using ASP;

public partial class VaccinationM_RegisterData : BasePage
{
    protected string CountyData = "[]";
    protected string TownData = "[]";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);

        if (SystemAreaCode.dict.ContainsKey("County"))
            CountyData = JsonConvert.SerializeObject(SystemAreaCode.dict["County"]);

        if (SystemAreaCode.dict.ContainsKey("Town"))
            TownData = JsonConvert.SerializeObject(SystemAreaCode.dict["Town"]);

        ASP.masterpage_custom_decoratedmasterpage_master masterPage = this.Master as ASP.masterpage_custom_decoratedmasterpage_master;
        masterPage.AutoIncludeCSS = false;
        masterPage.AutoIncludeJS = false;
    }
}