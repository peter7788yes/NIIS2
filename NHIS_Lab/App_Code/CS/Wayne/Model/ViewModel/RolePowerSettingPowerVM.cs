﻿using Newtonsoft.Json;
using System;

public class RolePowerSettingPowerVM
{

    //public int RID { get; set; }

    //public int RoleID
    //{
    //    get
    //    {
    //        return RID;
    //    }
    //}


    public int mId { get; set; }

    public int ModuleID { get; set; }



    public string  ps { get; set; }

    public int PowerStringOut {
        get
        {
            return Convert.ToInt32(ps, 2);
        }

    }

    public int PowerString { get; set; }

    [JsonProperty(PropertyName = "PSS")]
    public string PowerStringString {

        get
        {
           return Convert.ToString(PowerString, 2);
        }
    }

}