﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public partial class System_PowerM_RolePowerSetting_ChangePowerOP : BasePage
{
    public System_PowerM_RolePowerSetting_ChangePowerOP()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        string jsonString = GetString("v");
        List<RolePowerSettingPowerVM> list = new List<RolePowerSettingPowerVM>();
        list = JsonConvert.DeserializeObject<List<RolePowerSettingPowerVM>>(jsonString);

        OPVM VM = new OPVM();
        VM.chk = 1;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
        Response.End();
    }
}