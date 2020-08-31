using System.Collections.Generic;

public class WorkloadStatisticsCal : BaseCalculator
{
    protected override List<BaseReportRecordVM> DoData(int ReportType,int ApplyType, Dictionary<string, object> dict)
    {
        return new List<BaseReportRecordVM>() { new WorkloadElementaryRRVM() { ID=1,Name="A"} };
    }

    protected override byte[] DoExport(int ReportType,int ApplyType, List<BaseReportRecordVM> list)
    {
        return new byte[] { };
    }
}