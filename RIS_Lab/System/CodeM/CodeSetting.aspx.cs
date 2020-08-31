using System;

public partial class System_CodeM_CodeSetting : BasePage
{
    public string tbData = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<SystemCodeCateVM>(), "ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeCateList",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                      { "@pgNow", 1},
        //                                      { "@pgSize", 10}
        //                                });
    }
}