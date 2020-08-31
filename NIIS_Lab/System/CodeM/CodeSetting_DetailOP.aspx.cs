using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_CodeM_CodeSetting_DetailOP : BasePage
{
    public System_CodeM_CodeSetting_DetailOP()
    {
        base.AddPower("/System/CodeM/CodeSetting_Detail.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int ID = GetNumber<int>("ci");
        int EnState = GetNumber<int>("en");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");


        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<SystemCodeVM>(), "ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeList",
                                         new Dictionary<string, object>()
                                         {
                                               { "@SystemCodeCateID",  ID },
                                               { "@EnState",  EnState },
                                               { "@pgNow", pgNow == 0 ? 1 : pgNow},
                                               { "@pgSize", pgSize == 0 ? 10 : pgSize}
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

        
    }
}