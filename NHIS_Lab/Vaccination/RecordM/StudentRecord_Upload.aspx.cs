using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.Web.Configuration;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_StudentRecord_Upload : BasePage
{

    public Vaccination_RecordM_StudentRecord_Upload()
    {
        base.AddPower("/Vaccination/RecordM/StudentRecord.aspx", MyPowerEnum.上傳);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";


        //Response.Write("<div id='mydiv' >");
        //Response.Write("_");
        //Response.Write("</div>");
        //Response.Write("<script>mydiv.innerText = '';</script>");

        //Response.Write("<script language=javascript>;");
        //Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
        //Response.Write("{var output; output = 'Loading';dots++;if(dots>=dotmax)dots=1;");
        //Response.Write("for(var x = 0;x < dots;x++){output += '.';}mydiv.innerText =  output;}");
        //Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; window.setInterval('ShowWait()', 1000);}");
        //Response.Write("function HideWait(){mydiv.style.visibility = 'hidden';window.clearInterval();}");
        //Response.Write("StartShowWait();</script>");
        //Response.Flush();
        //Response.Write("<script>");
        //Response.Write("HideWait();");
        //Response.Write("</script>");
        //Thread.Sleep(10000);
        //Response.End();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
            string script = "";
            if (this.tbFile.HasFile==false)
            {
                script = "<script>alert('檔案無效');</style>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            }


            HSSFWorkbook workbook = new HSSFWorkbook(this.tbFile.FileContent);
            var sheet = workbook.GetSheetAt(0);

            DataTable dt = new DataTable();
            DataTable outDt = new DataTable();
            outDt.Columns.Add("失敗原因");
            outDt.Columns.Add("學校代碼");
            outDt.Columns.Add("學校名稱");
            outDt.Columns.Add("入學年度");
            outDt.Columns.Add("學生人數");
            outDt.Columns.Add("持卡人數");
            outDt.Columns.Add("BCG");
            outDt.Columns.Add("rHepB1");
            outDt.Columns.Add("rHepB2");
            outDt.Columns.Add("rHepB3");
            outDt.Columns.Add("5in1-1");
            outDt.Columns.Add("5in1-2");
            outDt.Columns.Add("5in1-3");
            outDt.Columns.Add("5in1-4");
            outDt.Columns.Add("Var");
            outDt.Columns.Add("MMR1");
            outDt.Columns.Add("MMR2");
            outDt.Columns.Add("JE1");
            outDt.Columns.Add("JE2");
            outDt.Columns.Add("JE3");
            outDt.Columns.Add("JE4");
            outDt.Columns.Add("Tdap-IPV");



            var headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn dc = new DataColumn(headerRow.GetCell(i).ToString());
                dt.Columns.Add(dc);
            }

            int rowCount = sheet.LastRowNum;

            for (int i2 = (sheet.FirstRowNum + 1); i2 <= sheet.LastRowNum; i2++)
            {
                var row = sheet.GetRow(i2);
                DataRow dr = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dr[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dr);
            }

            workbook = null;
            sheet = null;


                ElementaryRecordVM VM = new ElementaryRecordVM();

                foreach (DataRow item in dt.Rows)
                {
                        int Chk = 0;

                        try
                        {
                            VM.SchoolCode = item[0].ToString();
                            VM.SchoolName = item[1].ToString();
                            VM.AdmissionYear = int.Parse(item[2].ToString());
                            VM.StudentNumber = int.Parse(item[3].ToString());
                            VM.HasYellowCardNumber = int.Parse(item[4].ToString());

                            List<ElementaryRecordDataVM> list = new List<ElementaryRecordDataVM>();
                            for (int i = 5; i <= dt.Columns.Count - 1; i++)
                            {
                                ElementaryRecordDataVM child = new ElementaryRecordDataVM();
                                switch (i)
                                {
                                    case 5:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 1;
                                        break;
                                    case 6:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 2;
                                        break;
                                    case 7:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 3;
                                        break;
                                    case 8:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 4;
                                        break;
                                    case 9:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 5;
                                        break;
                                    case 10:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 6;
                                        break;
                                    case 11:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 7;
                                        break;
                                    case 12:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 8;
                                        break;
                                    case 13:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 9;
                                        break;
                                    case 14:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 10;
                                        break;
                                    case 15:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 11;
                                        break;
                                    case 16:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 12;
                                        break;
                                    case 17:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 13;
                                        break;
                                    case 18:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 14;
                                        break;
                                    case 19:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 15;
                                        break;
                                    case 20:
                                        child.VaccineDataID = 1;
                                        child.VaccineType = 16;
                                        break;
                                }

                                child.InoculationNumber = int.Parse(item[i].ToString());
                                child.ShouldInoculationNumber = child.InoculationNumber;

                                list.Add(child);


                            }

                            var user = AuthServer.GetLoginUser();



                            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddElementaryRecord", sc))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ElementarySchoolID", 1);
                                    cmd.Parameters.AddWithValue("@AdmissionYear", VM.AdmissionYear);
                                    cmd.Parameters.AddWithValue("@StudentNumber", VM.StudentNumber);
                                    cmd.Parameters.AddWithValue("@HasYellowCardNumber", VM.HasYellowCardNumber);
                                    cmd.Parameters.AddWithValue("@SignUserID", user.ID);
                                    cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                                    cmd.Parameters.AddWithValue("@StudentYear", 1);
                                    cmd.Parameters.AddWithValue("@InoculationType", 1);
                                    cmd.Parameters.AddWithValue("@VaccineTypeString", string.Join(",", list.Select(x => x.VaccineType.ToString())));
                                    cmd.Parameters.AddWithValue("@InoculationNumberString", string.Join(",", list.Select(x => x.InoculationNumber.ToString())));
                                    cmd.Parameters.AddWithValue("@ShouldInoculationNumberString", string.Join(",", list.Select(x => x.ShouldInoculationNumber.ToString())));
                                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                                    SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                                    sp.Direction = ParameterDirection.Output;

                                    sc.Open();
                                    cmd.ExecuteNonQuery();

                                    Chk = (int)sp.Value;
                                }
                            }
                        }
                        catch
                        {

                        }


                        if (Chk < 1)
                        {
                            List<object> list = new List<object>();
                            DataRow drNew = outDt.NewRow(); 
                            foreach (object  obj in item.ItemArray)
                            {
                                list.Add(obj);
                            }
                            list.Insert(0,"失敗");
                            drNew.ItemArray = list.ToArray();
                            outDt.Rows.Add(drNew);
                        }

                }

                script = "";
                if (outDt.Rows.Count > 0)
                {

                    //script = @"<script>
                    //     var popUpWindow = function (url, title, w, h) {
                    //         var left = (screen.width / 2) - (w / 2);
                    //         var top = (screen.height / 2) - (h / 2);
                    //         return window.open(url, title, 'toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,left=10000, top=10000, width=10, height=10, visible=none');
                    //     };
                    //    var openWindowWithPost  = function(url, title, w, h, keys, values) {
                    //    var newWindow = popUpWindow(url, title, w, h);
                    //    if (!newWindow) return false;
                    //    var html = '';
                    //    html += ""<html><head></head><body><form id='formid' method='post' target='_blank' action='"" + url + ""'>"";
                    //    keys = keys || [];
                    //    values = values || [];



                    //       if (keys && values && (keys.length == values.length))
                    //        for (var i = 0; i < keys.length; i++)
                    //            html += ""<input type='hidden' name='"" + keys[i] + ""' value='"" + values[i] + ""'/>"";
                    //        html += ""</form></body></html>"";
                    //       newWindow.document.write(html);
                    //       newWindow.document.getElementById('formid').submit();
                    //       setTimeout(function(){
                    //            newWindow.close();
                    //       },1000); 
                    //       return newWindow;
                    //    };";
                    //script += @"alert('上傳失敗');  
                    //        var keys = [];
                    //        var values = [];
                    //        keys[0] = 'json';
                    //        values[0] = encodeURI('" + JsonConvert.SerializeObject(outDt) + "');" +
                    //        "  openWindowWithPost('/Ashx/JsonToExcel.ashx','_blank',100,100, keys, values);</script>";

                     script += @"<script>alert('上傳失敗');  document.getElementById('json').value='" + JsonConvert.SerializeObject(outDt) + "';document.getElementById('formid').submit();</script>";
                }
                else
                {
                     script = "<script>alert('上傳成功');location.href = '/Vaccination/RecordM/StudentRecord.aspx';</script><style>body{display:none;}</style>";
                }

                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}