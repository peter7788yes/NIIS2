using System; 
using System.Web; 
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

public partial class CaseVist_VistCaseContentOP : System.Web.UI.Page
{

    protected string VisitResultData = "[]"; 
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.DisableTop(true); 
        int r = 0;
        int.TryParse(Request["r"], out r);
        //SystemCode.Update();
        if (r >0)
        {
            string ResultCatKey = "CaseVisit_VisitResult_Reason_" + r.ToString();

            List<SystemCodeVM> SystemCodeList = SystemCode.GetDict(ResultCatKey);
             VisitResultData = JsonConvert.SerializeObject(SystemCodeList);
             
              
         }
             
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(VisitResultData);
        Response.End();
    }
}