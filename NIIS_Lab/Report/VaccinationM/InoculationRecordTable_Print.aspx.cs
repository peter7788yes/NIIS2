using System;
using System.Collections.Generic;

public partial class Report_VaccinationM_InoculationRecordTable_Print : BasePage
{
    public string tbData = "[]";

    protected new void Page_Load(object sender, EventArgs e)
    {
        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.匯出xls, "", "1111", 1,1,new Dictionary<string, object>(),(data)=> { tbData = data; });
    }
}