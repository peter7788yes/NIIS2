﻿using System;

public partial class Vaccination_ParameterM_LocationSetting_SelectAgency : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";

    public Vaccination_ParameterM_LocationSetting_SelectAgency()
    {
        SearchPower = base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}