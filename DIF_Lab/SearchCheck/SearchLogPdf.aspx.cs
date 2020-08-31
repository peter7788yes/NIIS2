using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections;
using System.Data;
public partial class SearchCheck_SearchLogPdf : System.Web.UI.Page
{
    int AuditID = 0;
    string HeaderText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request["a"], out AuditID);

        DataTable dta = (DataTable)DBUtil.DBOp("ConnDB",
            @"SELECT [id]
      ,[AuditYearMonth]
      , (SELECT [OrgAgencyName]  FROM [NIIS_User].[dbo].[O_Org] where O_Org.[ID]=[OrgID]) as OrgName
      , (SELECT [UserName]  FROM [NIIS_User].[dbo].[U_User] where [U_User].[ID]=[UserID]  ) as UserName
      ,[SearchKind] 
,year ([AuditYearMonth])-1911 as Y
,month ([AuditYearMonth]) as M
,year(AuditDate)-1911 as AY
,month ([AuditDate]) as AM
,day ([AuditDate]) as AD
  FROM [dbo].[C_CaseSearchAudit] where [id]={0} 

",
              new string[] { AuditID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        if (dta.Rows.Count > 0)
        {
            HeaderText = string.Format("戶役政子系統使用紀錄查核表({0}年{1}月)"
                                       , dta.Rows[0]["Y"].ToString()
                                     , dta.Rows[0]["M"].ToString());
            ltCaption.Text = string.Format("受稽核單位：{0}   受稽核人員：{1}   稽核日期：{2}年{3}月{4}日"
                                        , dta.Rows[0]["OrgName"].ToString()
                                         , dta.Rows[0]["UserName"].ToString()
                                       , dta.Rows[0]["AY"].ToString()
                                       , dta.Rows[0]["AM"].ToString()
                                       , dta.Rows[0]["AD"].ToString());

        }


        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "exec  [dbo].[usp_SearchCheck_xExportSearchLogDetailList] {0}  ",
              new string[] { AuditID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

        GridView1.DataSource = dt;
        GridView1.DataBind();
       // GridView1.HeaderRow.Cells[0].Width = Unit.Percentage(1);
        ExportToPDF();
    }
    protected void ExportToPDF()
    {
        try
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.Page.RenderControl(htw);
            StringReader sr = new StringReader(sw.ToString());

            //建立Document,HTMLWorker及PdfWriter物件,並指定寫出PDF檔的路徑
            //文件格式為橫式A4,若建構式不加引數,預設為直式A4
            Document doc = new Document(PageSize.A4 );
             
            HTMLWorker hw = new HTMLWorker(doc);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            Font fontChinese = FontFactory.GetFont("標楷體", BaseFont.IDENTITY_H, 16f);

            //取得作業系統中有安裝的字型,通常在C:\WINDOWS\Fonts目錄下
            //取"windir"這一個環境變數,設定為C:\WINDOWS
            //若要觀看字型檔案的檔名,在C:\WINDOWS\Fonts目錄中以滑鼠右鍵點選檔案並按下內容
            FontFactory.Register(System.Environment.GetEnvironmentVariable("windir") +
            @"\Fonts\simhei.ttf");//SimHei字體(中易黑體)
            FontFactory.Register(System.Environment.GetEnvironmentVariable("windir") +
            @"\Fonts\MINGLIU.TTC");//細明體 & 新細明體
            FontFactory.Register(System.Environment.GetEnvironmentVariable("windir") +
            @"\Fonts\KAIU.TTF");//標楷體

            //建立樣式格式
            StyleSheet style = new StyleSheet();
            //設定html tag應用什麼樣式來解析
            //根據測試,LoadTagStyle(...)的參數意思如下:
            //第一個參數: html標籤名稱
            //第二個參數: 屬性名稱,目前解讀如下:
            //            "face" 使用的字型(但要先取得系統字型)
            //            "encoding" 字型編碼,Identity-H代表
            //            The Unicode encoding with horizontal writing.
            //            "leading" 列的高度

            //目前測試,標楷體尚無法寫出
            /*
            style.LoadTagStyle("body", "face", "KAIU");
            style.LoadTagStyle("body", "encoding", "Identity-H");
            style.LoadTagStyle("body", "leading", "50,0");
            */

            //body tag設定
            style.LoadTagStyle("body", "face", "新細明體");
            style.LoadTagStyle("body", "encoding", "Identity-H");
            style.LoadTagStyle("body", "leading", "11,0");

            //td tag設定,會蓋過body tag的設定
            style.LoadTagStyle("td", "face", "新細明體");
            style.LoadTagStyle("td", "encoding", "Identity-H");
            style.LoadTagStyle("td", "leading", "14,0");

            //開啟Document文件,並使用HTMLWorker解析html檔案的輸入串流後,匯出PDF檔
            writer.PageEvent = new PDFFooter(HeaderText);
            doc.Open();

            //根據iTextSharp的解析,html的tag會解析成不同的iTextSharp物件,
            //ParseToList(...)第一個引數為html文件的輸入串流,第二個引數為樣式設定
            //List<IElement> htmlElement = HTMLWorker.ParseToList(sr, style);
            List<IElement> htmlElement = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(sr, style);

            for (int i = 0; i < htmlElement.Count; i++)
            {
                //以這一份html檔,iTextSharp將其解讀為iTextSharp.text.Paragraph與
                //iTextSharp.text.pdf.PdfPTable物件
                //System.Console.WriteLine("第" + (i + 1) + "個物件為: " + htmlElement[i]);
                //將每個被HTMLWorker解析的物件加到Document物件中
                doc.Add(htmlElement[i]);
            }
       
            
            doc.Close();
            Response.Buffer = true;
            Response.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/download";
            Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(string.Format("戶役政子系統使用紀錄查核表.pdf", DateTime.Now.ToString("yyyyMMdd"))));
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            //sr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
public class PDFFooter : PdfPageEventHelper
{
    string HeaderText;
    public PDFFooter(string headertext)
    {
        HeaderText = headertext;
    }



    Font fontChineseTitle = FontFactory.GetFont("新細明體", BaseFont.IDENTITY_H, 16f);
    Font fontChineseContent = FontFactory.GetFont("新細明體", BaseFont.IDENTITY_H, 11f);
    // write on top of document
    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        base.OnOpenDocument(writer, document);
        
        PdfPTable tabFot = new PdfPTable(new float[] { 1F });
        //tabFot.SpacingAfter = 10F;
        PdfPCell cell;

        tabFot.TotalWidth = 300F; 
        cell = new PdfPCell(new Phrase(HeaderText, fontChineseTitle));
        cell.Border = Rectangle.NO_BORDER;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        tabFot.AddCell(cell);
        tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);

    }

    // write on start of each page
    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);
    }

    // write on end of each page
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);
        PdfPTable tabFot = new PdfPTable(new float[] { 1F }); 
      
        PdfPCell cell; 
    
        cell = new PdfPCell(new Phrase("受稽單位資訊安全官簽章　　　　　　　　受稽人員簽章", fontChineseContent));
        cell.Border = Rectangle.NO_BORDER;
        tabFot.AddCell(cell);
     

        PdfPCell cell3;
        tabFot.TotalWidth = 1800F; 
        cell3 = new PdfPCell(new Phrase("", fontChineseContent));
        cell3.Border = Rectangle.NO_BORDER;
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3);
        tabFot.AddCell(cell3); 
            PdfPCell cell2; 
        cell2 = new PdfPCell(new Phrase("　　　　　　　　　　　　　　　　　　　稽核單位主管簽章", fontChineseContent));
        cell2.Border = Rectangle.NO_BORDER;
        tabFot.AddCell(cell2);
        tabFot.WriteSelectedRows(0, -1, 100, 150, writer.DirectContent);
    }

    //write on close of document
    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);
    }
}