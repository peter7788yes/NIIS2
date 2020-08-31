using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class CaseVisit_ChooseVacListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int pgNow = 1, pgSize = 10; 

        int.TryParse(Request.Form["pgNow"], out pgNow); 
       pgNow=( pgNow == 0 ? 1 : pgNow);

       
        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
          , "exec dbo.usp_CaseVisit_xGetVacList {0},{1} "
          , new string[]{   pgNow.ToString ()
                      , 50.ToString ()
               }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);

        List<ChooseVacListVM> list = new List<ChooseVacListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}