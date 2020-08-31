using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class CaseVisit_uc_CaseVisitDataForVaccination : System.Web.UI.UserControl
{ 
    public int  CaseID { get; set; }
    public string DoseID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(CaseID.ToString());
        //Response.Write(DoseID);

        if (CaseID != null && DoseID != null)
        {
            DoseID = DoseID.Split('-')[0];
            //Response.Write(DoseID);
            BindData();
        }
    }
    protected void BindData()
    {//<table>
        //<tbody><tr>
        //<th scope="col">疫苗別</th>
        //<th scope="col">訪查日期</th>
        //<th scope="col">訪查單位（人員）</th>
        //<th scope="col">訪查方式</th>
        //<th scope="col">訪查原因</th>
        //<th scope="col">訪查紀錄</th>
        //<th scope="col">附件</th>
        //<th scope="col">刪除</th>
        //</tr>
        //</tbody>
        //</table>
        int ViewOrgID = AuthServer.GetLoginUser().OrgID;
        CaseVisitTb.Controls.Clear();

        Table tb = new Table();
        TableRow thr = new TableRow();
        TableHeaderCell th1 = new TableHeaderCell();
        th1.Text = "疫苗別"; thr.Cells.Add(th1);
        TableHeaderCell th2 = new TableHeaderCell();
        th2.Text = "訪查日期"; thr.Cells.Add(th2);
        TableHeaderCell th3 = new TableHeaderCell();
        th3.Text = "訪查單位（人員）"; thr.Cells.Add(th3);
        TableHeaderCell th4 = new TableHeaderCell();
        th4.Text = "訪查方式"; thr.Cells.Add(th4);
        TableHeaderCell th5 = new TableHeaderCell();
        th5.Text = "訪查原因"; thr.Cells.Add(th5);
        TableHeaderCell th6 = new TableHeaderCell();
        th6.Text = "訪查結果"; thr.Cells.Add(th6);
         TableHeaderCell th7 = new TableHeaderCell();
         th7.Text = "附件"; thr.Cells.Add(th7);
         tb.Controls.Add(thr);
         

        DataTable dt = (DataTable)
                     DBUtil.DBOp("ConnDB",
                     @"exec [dbo].[usp_CaseVist_xCaseVisitListByDoseID] {0},{1}  "
                         , new string[] { 
                                     CaseID .ToString ()
                                    ,DoseID  
                                }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);


        foreach (DataRow r in dt.Rows)
        {
            TableRow thd = new TableRow();
              TableCell td1 = new TableCell();
             td1.Text = r["疫苗別"].ToString (); thd.Cells.Add(td1);
             TableCell td2 = new TableCell();
             td2.Text = r["訪查日期"].ToString(); thd.Cells.Add(td2);
             TableCell td3 = new TableCell();
             td3.Text = r["訪查單位（人員）"].ToString(); thd.Cells.Add(td3);
             TableCell td4 = new TableCell();
             td4.Text =  SystemCode.GetName("CaseVisit_VisitType",Convert.ToInt32 (r["訪查方式"])) ; thd.Cells.Add(td4);
           
             TableCell td5 = new TableCell();
             td5.Text = SystemCode.GetName("CaseVisit_VisitReason", Convert.ToInt32(r["訪查原因"])); thd.Cells.Add(td5);
             TableCell td6 = new TableCell();
             td6.Text = SystemCode.GetName("CaseVisit_VisitResult_Reason_" + r["訪查原因"], Convert.ToInt32(r["訪查結果"])); thd.Cells.Add(td6);
             TableCell td7 = new TableCell();

             int VisitOrg;
             int.TryParse(r["VisitOrg"].ToString(), out VisitOrg); 
             td7.Text = ShowFiles(Convert.ToInt32(r["VisitID"]), (VisitOrg == ViewOrgID )); thd.Cells.Add(td7);
            
            tb.Controls.Add(thd);

        }

        CaseVisitTb.Controls.Add(tb);

    }


    private string ShowFiles(int VisitID, bool bDownload)
    {
        StringBuilder sb = new StringBuilder(""); 

        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT  isnull(C_CaseVisitFile.Filetype,0) Filetype,C_CaseVisitFile.ID,[FileID]  ,F_FileInfo.DisplayFileName FROM [dbo].[C_CaseVisitFile] inner join F_FileInfo on F_FileInfo.ID = [C_CaseVisitFile].FileID and [C_CaseVisitFile].VisitID={0} and C_CaseVisitFile.LogicDel=0", new string[] { VisitID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        foreach (DataRow r in dt.Rows)
        {
            string filetype = SystemCode.GetName("CaseVisit_VisitFileType", Convert.ToInt32(r["Filetype"]));

            if (bDownload)
                sb.AppendFormat("<div id=\"FileDIV_{2}\"><a href=\"DownloadFileOP.aspx?i={1}\">{3} : {0}</a> ;</div>", Server.HtmlEncode(r["DisplayFileName"].ToString()), Server.HtmlEncode(r["FileID"].ToString()), Server.HtmlEncode(r["ID"].ToString()), filetype);
            else
               sb.Append( filetype + ":" + Server.HtmlEncode(r["DisplayFileName"].ToString()) + ";");
        }
        return sb.ToString();
    }
}