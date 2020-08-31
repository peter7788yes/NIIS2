using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls; 

public partial class CaseMaintain_MergeCheckDownloadOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
         
        string BirthDateS;
        string BirthDateE;
        int SearchKind=1; 

        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? ""; 
        if (BirthDateS!="") BirthDateS = (Convert.ToInt32(BirthDateS.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateS.Substring(3, 2) + "/" + BirthDateS.Substring(5, 2);
        if (BirthDateE != "")  BirthDateE = (Convert.ToInt32(BirthDateE.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateE.Substring(3, 2) + "/" + BirthDateE.Substring(5, 2);
         
        int.TryParse(Request["SearchKind"], out SearchKind);
         


        SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetMergeUserListToExport");
        cmd.CommandType = CommandType.StoredProcedure; 
        cmd.Parameters.AddWithValue("@BirthDateS", BirthDateS);
        cmd.Parameters.AddWithValue("@BirthDateE", BirthDateE);
        cmd.Parameters.AddWithValue("@Area", 0);
        cmd.Parameters.AddWithValue("@SearchKind", SearchKind); 
        DataTable dt = DB.GetDataTable(cmd, "ConnDB"); 


      DataTableToCSV.DownloadCSV(dt, "MyCsv.csv");
       // DataTableToCSV.ExportToCSVFile(dt);
        //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt));

    }
}