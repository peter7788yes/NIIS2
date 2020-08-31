using System;
using System.Collections.Generic;

public partial class CodeM_MedicalCode_DetailOP : BasePage
{
    public CodeM_MedicalCode_DetailOP()
    {
        base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        int OrgID = GetNumber<int>("i");


        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<MedicalCodeVM>(), "ConnUser", "dbo.usp_CodeM_xGetOrgChangeLogListByID",
                                         new Dictionary<string, object>()
                                         {
                                               { "@OrgID", OrgID }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

    }
}