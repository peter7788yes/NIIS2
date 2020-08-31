using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// ExportFile 的摘要描述
/// </summary>
public class ExportToFile
{
    
	 public void ExporttoExcel(string htmlstring, string fileName)
    {
     
		HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write("<style> .t { mso-number-format:\\@; } .text { mso-number-format:\\@; } </style>");
        HttpContext.Current.Response.Write("<meta http-equiv='Content-Type'; content='text/html';charset='utf-8'>");
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xls" );
        HttpContext.Current.Response.ContentType =  "application/vnd.xls"; 
        HttpContext.Current.Response.Write(htmlstring);
        HttpContext.Current.Response.End();
    }
	
	
    public void ExporttoExcel(WebControl webControl, string fileName)
    {
        ExportFileParse(webControl, fileName, "xls", "vnd.xls");
    }

    public void ExporttoExcel(DataTable dt, string fileName)
    {
        Table myTB = new Table();
        TableHeaderRow thr = new TableHeaderRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TableHeaderCell thc = new TableHeaderCell();
            thc.Attributes.Add("class", "t");
            thc.Text = dt.Columns[i].ColumnName;
            //thc.BorderWidth = Unit.Pixel(1);
            thr.Cells.Add(thc);
        }
        myTB.Rows.Add(thr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow tr = new TableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    TableCell tc = new TableCell();
                   // tc.Attributes.Add("class", "t");
                    if (dt.Rows[i][j] != DBNull.Value)
                    { 
                        tc.Text = HttpUtility.HtmlEncode( dt.Rows[i][j].ToString());
                        if (tc.Text.StartsWith("0") && tc.Text != "0" && (!tc.Text.StartsWith("0.")))
                            tc.Attributes.Add("class", "t");


                    }
                   // tc.BorderWidth = Unit.Pixel(1);
                    tr.Cells.Add(tc);
                }
                myTB.Rows.Add(tr);
            }
        }
        else
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.ColumnSpan = dt.Columns.Count;
            tc.BorderWidth = Unit.Pixel(1);
            tc.Text = "目前尚無資料！";
            tr.Cells.Add(tc);
            myTB.Rows.Add(tr);
        }
        ExportFileParse(myTB, fileName, "xls", "vnd.xls");
    }

    public void ExporttoWord(WebControl webControl, string fileName)
    {
        ExportFileParse(webControl, fileName, "doc", "vnd.word");
    }
	
	    public void ExporttoHtml(DataTable dt, string fileName)
    {
	        Table myTB = new Table();
        TableHeaderRow thr = new TableHeaderRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TableHeaderCell thc = new TableHeaderCell();
            thc.Attributes.Add("class", "t");
            thc.Text = dt.Columns[i].ColumnName;
            //thc.BorderWidth = Unit.Pixel(1);
            thr.Cells.Add(thc);
        }
        myTB.Rows.Add(thr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow tr = new TableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    TableCell tc = new TableCell();
                    tc.Attributes.Add("class", "t");
                    if (dt.Rows[i][j] != DBNull.Value)
                        tc.Text = dt.Rows[i][j].ToString();
                   // tc.BorderWidth = Unit.Pixel(1);
                    tr.Cells.Add(tc);
                }
                myTB.Rows.Add(tr);
            }
        }
        else
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.ColumnSpan = dt.Columns.Count;
            tc.BorderWidth = Unit.Pixel(1);
            tc.Text = "目前尚無資料！";
            tr.Cells.Add(tc);
            myTB.Rows.Add(tr);
        }
         
		HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write("<html>");
        HttpContext.Current.Response.Write("<meta http-equiv='Content-Type'; content='text/html';charset='utf-8'>");
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "." + "html");
        HttpContext.Current.Response.ContentType =  "text/html";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        myTB.RenderControl(htmlWrite);
        HttpContext.Current.Response.Write(stringWrite.ToString());
		HttpContext.Current.Response.Write("</html>");
        HttpContext.Current.Response.End();
		
		
 
    }
	
	


    public void ExporttoWord(DataTable dt, string fileName)
    {
        Table myTB = new Table();
        TableHeaderRow thr = new TableHeaderRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = dt.Columns[i].ColumnName;
            thc.BorderWidth = Unit.Pixel(1);
            thr.Cells.Add(thc);
        }
        myTB.Rows.Add(thr);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow tr = new TableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    TableCell tc = new TableCell();
                    tc.Attributes.Add("class", "text");
                    if (dt.Rows[i][j] != DBNull.Value)
                        tc.Text = dt.Rows[i][j].ToString();
                    tc.BorderWidth = Unit.Pixel(1);
                    tr.Cells.Add(tc);
                }
                myTB.Rows.Add(tr);
            }
        }
        else
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            tc.ColumnSpan = dt.Columns.Count;
            tc.BorderWidth = Unit.Pixel(1);
            tc.Text = "目前尚無資料！";
            tr.Cells.Add(tc);
            myTB.Rows.Add(tr);
        }
        ExportFileParse(myTB, fileName, "doc", "vnd.word");
    }

    private void ExportFileParse(WebControl webControl, string fileName, string Extension, string contentType)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write("<style> .t { mso-number-format:\\@; } </style>");
        HttpContext.Current.Response.Write("<meta http-equiv='Content-Type'; content='text/html';charset='utf-8'>");
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "." + Extension);
        HttpContext.Current.Response.ContentType =  "application/" + contentType;
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        webControl.RenderControl(htmlWrite);
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }
}
