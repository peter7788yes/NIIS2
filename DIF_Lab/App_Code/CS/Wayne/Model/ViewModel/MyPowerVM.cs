using Newtonsoft.Json;
using System;

public class MyPowerVM
{
    public string PageUrl { get; set; }

    public MyPowerEnum myPowerEnum { get; set; }

    public bool HasPower { get; set; }

    public MyPowerVM(string PageUrl, MyPowerEnum myPowerEnum)
    {
        this.PageUrl = PageUrl;
        this.myPowerEnum = myPowerEnum;
    }
  
}