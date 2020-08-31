using System;
using System.Collections.Generic;

public partial class Vaccination_RecordM_StudentRecordOP : BasePage
{
    public Vaccination_RecordM_StudentRecordOP()
    {
        base.AddPower("/Vaccination/RecordM/StudentRecord.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");
        int OrgID = GetNumber<int>("lid");
        int AdmissionYearStart = GetNumber<int>("s");
        int AdmissionYearEnd = GetNumber<int>("e");
        int ElementarySchoolID = GetNumber<int>("ei");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<ElementaryRecordVM>(), "ConnDB", "dbo.usp_RecordM_xGetElementRecordListByMany",
                                         new Dictionary<string, object>()
                                         {
                                                    { "@OrgID", OrgID },
                                                    { "@AdmissionYearStart", AdmissionYearStart },
                                                    { "@AdmissionYearEnd", AdmissionYearEnd == 0 ? 200 : AdmissionYearEnd },
                                                    { "@InoculationType", 1 },
                                                    { "@ElementarySchoolID", ElementarySchoolID },
                                                    { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                                    { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();
    }
}