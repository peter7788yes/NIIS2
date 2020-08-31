using System;
using System.Collections.Generic;

public partial class System_CodeM_CodeSettingOP : BasePage
{

    public System_CodeM_CodeSettingOP()
    {
        base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");


        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<SystemCodeCateVM>(), "ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeCateList",
                                         new Dictionary<string, object>()
                                         {
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow},
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize}
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

    }
}