using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class CaseMaintain_GetIdNoFromUploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string TempUploadFile = "~/CaseMaintain/TempUploadFile";
        string IdNos = "";
        if (fu_Excel.PostedFile.ContentLength > 0)
        {
            try
            {
                if (!Directory.Exists(Server.MapPath(TempUploadFile)))
                    Directory.CreateDirectory(Server.MapPath(TempUploadFile));
                string strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                Session["UploadExcelFileName"] = strDate;
                fu_Excel.SaveAs(Server.MapPath(TempUploadFile + "/") + strDate + ".xls");
                SqlCommand cmd1 = new SqlCommand();

                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
                DataTable dt  = new DataTable();
                cn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(TempUploadFile + "/") + strDate + ".xls" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                cn.Open();
                cmd1.CommandTimeout = 0; 

                try
                {
                    da = new System.Data.OleDb.OleDbDataAdapter("select * from [IdentityNumber$]", cn); 
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(dt);

                    foreach (DataRow r in dt.Rows)
                        IdNos += r["ID"].ToString() + ",";
                }
               catch (Exception ex2)
                {
                    Response.Write(ex2.Message);
                }
                 
                 
                cmd1.Dispose(); 
                cn.Close();
          
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
             }
        }

        IdNos = IdNos.TrimEnd(',');
        //Response.Write(IdNos);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "window.opener.getIdNo('" + IdNos + "'); window.close();", true);
       // Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", "window.opener.getIdNo('" + IdNos + "'); window.close();", true);

    }
}