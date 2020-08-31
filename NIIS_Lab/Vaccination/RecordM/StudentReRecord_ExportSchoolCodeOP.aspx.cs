using System;
using System.Collections.Generic;
using System.Web;

public partial class Vaccination_RecordM_StudentReRecord_ExportSchoolCodeOP : BasePage
{
    public Vaccination_RecordM_StudentReRecord_ExportSchoolCodeOP()
    {
        base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.下載);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("POST");

        var user = AuthServer.GetLoginUser();

        var dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetSchoolCodeByOrgID"
                                        , new Dictionary<string, object>()
                                        {
                                            { "@OrgID", user.OrgID}
                                        });
        Response.ContentType = "application/download";
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + HttpUtility.UrlEncode("範本-學校代碼.xls")));
        ExcelToolT tool = new ExcelToolT();
        tool.RenderDataTableToExcel(dt).CopyTo(Response.OutputStream);
        Response.End();
    }
}