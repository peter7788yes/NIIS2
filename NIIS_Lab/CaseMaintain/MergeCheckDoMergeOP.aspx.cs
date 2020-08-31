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
using Newtonsoft.Json;
using System.Text;

public partial class CaseMaintain_MergeCheckDoMergeOP : BasePage
{ 
     
    protected void Page_Load(object sender, EventArgs e)
    {
         
        
       // ConnDB
         JsonReply r = new JsonReply();
         DB.ExecuteNonQuery(new SqlCommand("exec dbo.usp_CaseUser_xUpdateMergeCheckList"), "ConnDB");
       r.RetCode = 1;
       r.Content = "ok"  ;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        Response.End();
    }

    

}

 

