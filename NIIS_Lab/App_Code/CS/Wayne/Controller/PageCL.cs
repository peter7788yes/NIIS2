using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public class PageCL
{
    public string GetList<T>(List<T> list,string Conn,string SQL, Dictionary<string, object> dict)
    {
        DataSet ds = MSDB.GetDataSet(Conn, SQL
                                        , dict);
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        if (list.Count > 0)
            rtn.message = list;
        else
            rtn.message = new List<string>();

        return JsonConvert.SerializeObject(rtn);
    }
}