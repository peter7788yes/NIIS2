using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ICalculatorProvider 的摘要描述
/// </summary>
public class BaseCalculator
{
    public string RunTask(string MD5, int ReportCateID,int ReportType,int ApplyType, Dictionary<string,object> dict)
    {
        var data = DoData(ReportType, ApplyType, dict);
        var file = DoExport(ReportType, ApplyType, data);

        //upload data & xls
        var result = new ReportResultVM();
        result.ResultData = data;
        result.ResultFile = file;

        return JsonConvert.SerializeObject(result);//, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
    }

    protected virtual List<BaseReportRecordVM> DoData(int ReportType,int ApplyType, Dictionary<string, object> dict)
    {
        return new List<BaseReportRecordVM>();
    }

    protected virtual byte[] DoExport(int ReportType,int ApplyType, List<BaseReportRecordVM> list)
    {
        return new byte[] { };
    }
}