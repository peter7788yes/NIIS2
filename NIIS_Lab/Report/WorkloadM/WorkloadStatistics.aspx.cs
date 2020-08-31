using System;
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Report_WorkloadM_WorkloadStatistics : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UC_OpenSelectSingleOrg.PageUrl = "/Report/WorkloadM/WorkloadStatistics.aspx";
        UC_OpenSelectSingleOrg.IsRequired = true;

        var dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetVaccineList"
                                       , new Dictionary<string, object>()
                                       {

                                       });

        List<AddVaccineVM> list = new List<AddVaccineVM>();
        EntityS.FillModel(list, dt);

        if(list.Count>0)
        {
            foreach (var item in list)
            {
                var li = new ListItem();
                li.Text = string.Format("{0} ({1})",item.VaccineID,item.VaccineCName);
                li.Value = item.VaccineID;
                ddlVaccSelect.Items.Add(li);
            }
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.匯出xls, Path.GetFileNameWithoutExtension(Request.PhysicalPath).Split('_')[0]
            , "1111", 1, 1, new Dictionary<string, object>(), (data) => { });
    }
}