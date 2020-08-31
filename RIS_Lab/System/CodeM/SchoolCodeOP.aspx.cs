using System;
using System.Collections.Generic;

public partial class CodeM_SchoolCodeOP : BasePage
{
    public CodeM_SchoolCodeOP()
    {
        base.AddPower("/System/CodeM/SchoolCode.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        //int OrgID = GetNumber<int>("oid");
        int SchoolCounty = GetNumber<int>("sc");
        int SchoolTown = GetNumber<int>("st");
        int SchoolVillage = GetNumber<int>("sv");
        int EnableState = GetNumber<int>("es");
        string SchoolName = GetString("sn");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<SchoolCodeVM>(), "ConnDB", "dbo.usp_CodeM_xGetSchoolListByMany",
                                         new Dictionary<string, object>()
                                         {
                                              //{ "@OrgID", OrgID },
                                              { "@SchoolCounty", SchoolCounty  },
                                              { "@SchoolTown", SchoolTown },
                                              { "@SchoolVillage", SchoolVillage },
                                              { "@EnableState", EnableState },
                                              { "@SchoolName", SchoolName },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

    }
}