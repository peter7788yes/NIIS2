using System;
using System.Collections.Generic;

public partial class Vaccination_RecordM_BCGRecordOP : BasePage
{
    public Vaccination_RecordM_BCGRecordOP()
    {
        base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

       
        int OrgID = GetNumber<int>("lid");
        int StatisticalYearStart = GetNumber<int>("s");
        int StatisticalYearEnd = GetNumber<int>("e");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<BCGRecordVM>(), "ConnDB", "dbo.usp_RecordM_xGetBCGRecordListByMany", 
                                        new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID },
                                              { "@StatisticalYearStart", StatisticalYearStart },
                                              { "@StatisticalYearEnd", StatisticalYearEnd == 0 ? 200 : StatisticalYearEnd },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}