using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class RecordDetailVM
{
    public string RocID { get; set; }

    public string CaseUserName { get; set; }

    public string Gender { get; set; }

    public DateTime BrithDate { get; set; }

    public string VaccineData { get; set; }
}