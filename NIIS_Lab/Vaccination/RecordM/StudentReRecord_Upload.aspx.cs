using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

public partial class Vaccination_RecordM_StudentReRecord_Upload : BasePage
{
    public Vaccination_RecordM_StudentReRecord_Upload()
    {
        base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.上傳);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string script = "";
        if (this.tbFile.HasFile == false)
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
        outDt.Columns.Add("級別\r\n(1 = 新生, 2 = 二年級)");
        outDt.Columns.Add("BCG應補種人數");
        outDt.Columns.Add("BCG實際補種人數");

        outDt.Columns.Add("rHepB1應補種人數");
        outDt.Columns.Add("rHepB1實際補種人數");

        outDt.Columns.Add("rHepB2應補種人數");
        outDt.Columns.Add("rHepB2實際補種人數");

        outDt.Columns.Add("rHepB3應補種人數");
        outDt.Columns.Add("rHepB3實際補種人數");

        outDt.Columns.Add("5in1-1應補種人數");
        outDt.Columns.Add("5in1-1實際補種人數");

        outDt.Columns.Add("5in1-2應補種人數");
        outDt.Columns.Add("5in1-2實際補種人數");

        outDt.Columns.Add("5in1-3應補種人數");
        outDt.Columns.Add("5in1-3實際補種人數");

        outDt.Columns.Add("5in1-4應補種人數");
        outDt.Columns.Add("5in1-4實際補種人數");

        outDt.Columns.Add("Var應補種人數");
        outDt.Columns.Add("Var實際補種人數");

        outDt.Columns.Add("MMR1應補種人數");
        outDt.Columns.Add("MMR1實際補種人數");

        outDt.Columns.Add("MMR2應補種人數");
        outDt.Columns.Add("MMR2實際補種人數");

        outDt.Columns.Add("JE1應補種人數");
        outDt.Columns.Add("JE1實際補種人數");

        outDt.Columns.Add("JE2應補種人數");
        outDt.Columns.Add("JE2實際補種人數");

        outDt.Columns.Add("JE3應補種人數");
        outDt.Columns.Add("JE3實際補種人數");

        outDt.Columns.Add("JE4應補種人數");
        outDt.Columns.Add("JE4實際補種人數");

        outDt.Columns.Add("Tdap-IPV應補種人數");
        outDt.Columns.Add("Tdap-IPV實際補種人數");



        var headerRow = sheet.GetRow(1);
        int cellCount = headerRow.LastCellNum;

        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn dc = new DataColumn(headerRow.GetCell(i).ToString());
            if(i>=4)
            {
                if (i % 2 == 1)
                {
                    dc.ColumnName = sheet.GetRow(0).GetCell(i - 1).ToString() + dc.ColumnName;
                }
                else
                {
                    dc.ColumnName = sheet.GetRow(0).GetCell(i).ToString() + dc.ColumnName;
                }
            }
            else
            {
                dc.ColumnName = sheet.GetRow(0).GetCell(i).ToString();
            }
            dt.Columns.Add(dc);
        }

        int rowCount = sheet.LastRowNum;

        for (int i2 = 2; i2 <= sheet.LastRowNum; i2++)
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
                VM.StudentYear = int.Parse(item[3].ToString());

                List<ElementaryRecordDataVM> list = new List<ElementaryRecordDataVM>();
                for (int i = 4; i <= dt.Columns.Count-1; i++)
                {
                    ElementaryRecordDataVM child = new ElementaryRecordDataVM();
                    switch (i)
                    {
                        case 4:
                        case 5:
                            child.VaccineDataID = 1;
                            child.VaccineType = 1;
                            break;
                        case 6:
                        case 7:
                            child.VaccineDataID = 1;
                            child.VaccineType = 2;
                            break;
                        case 8:
                        case 9:
                            child.VaccineDataID = 1;
                            child.VaccineType = 3;
                            break;
                        case 10:
                        case 11:
                            child.VaccineDataID = 1;
                            child.VaccineType = 4;
                            break;
                        case 12:
                        case 13:
                            child.VaccineDataID = 1;
                            child.VaccineType = 9;
                            break;
                        case 14:
                        case 15:
                            child.VaccineDataID = 1;
                            child.VaccineType = 10;
                            break;
                        case 16:
                        case 17:
                            child.VaccineDataID = 1;
                            child.VaccineType = 11;
                            break;
                        case 18:
                        case 19:
                            child.VaccineDataID = 1;
                            child.VaccineType = 12;
                            break;
                        case 20:
                        case 21:
                            child.VaccineDataID = 1;
                            child.VaccineType = 13;
                            break;
                        case 22:
                        case 23:
                            child.VaccineDataID = 1;
                            child.VaccineType = 14;
                            break;
                        case 24:
                        case 25:
                            child.VaccineDataID = 1;
                            child.VaccineType = 15;
                            break;
                        case 26:
                        case 27:
                            child.VaccineDataID = 1;
                            child.VaccineType = 16;
                            break;
                        case 28:
                        case 29:
                            child.VaccineDataID = 1;
                            child.VaccineType = 17;
                            break;
                        case 30:
                        case 31:
                            child.VaccineDataID = 1;
                            child.VaccineType = 18;
                            break;
                        case 32:
                        case 33:
                            child.VaccineDataID = 1;
                            child.VaccineType = 19;
                            break;
                        case 34:
                        case 35:
                            child.VaccineDataID = 1;
                            child.VaccineType = 20;
                            break;
                        case 36:
                        case 37:
                            child.VaccineDataID = 1;
                            child.VaccineType = 21;
                            break;
                        case 38:
                        case 39:
                            child.VaccineDataID = 1;
                            child.VaccineType = 22;
                            break;
                        case 40:
                        case 41:
                            child.VaccineDataID = 1;
                            child.VaccineType = 23;
                            break;
                        case 42:
                        case 43:
                            child.VaccineDataID = 1;
                            child.VaccineType = 24;
                            break;
                    }

                    child.InoculationNumber = int.Parse(item[i].ToString());
                    child.ShouldInoculationNumber = child.InoculationNumber;

                    list.Add(child);


                }

                var user = AuthServer.GetLoginUser();

                Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

                MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddElementaryRecord"
                                                 , ref OutDict
                                                 , new Dictionary<string, object>()
                                                 {
                                                    { "@ElementarySchoolID", 1 },
                                                    { "@AdmissionYear", VM.AdmissionYear },
                                                    { "@StudentNumber", VM.StudentNumber },
                                                    { "@HasYellowCardNumber", VM.HasYellowCardNumber },
                                                    { "@SignUserID", user.ID},
                                                    { "@CreatedUserID", user.ID },
                                                    { "@StudentYear", VM.StudentYear },
                                                    { "@InoculationType", 2 },
                                                    { "@VaccineTypeString", string.Join(",", list.Select(x => x.VaccineType.ToString())) },
                                                    { "@InoculationNumberString", string.Join(",", list.Select(x => x.InoculationNumber.ToString())) },
                                                    { "@ShouldInoculationNumberString", string.Join(",", list.Select(x => x.ShouldInoculationNumber.ToString())) },
                                                    { "@OrgID", user.OrgID }

                                                });

                Chk = (int)OutDict["@Chk"];

            }
            catch
            {

            }


            if (Chk < 1)
            {
                List<object> list = new List<object>();
                DataRow drNew = outDt.NewRow();
                foreach (object obj in item.ItemArray)
                {
                    list.Add(obj);
                }
                list.Insert(0, "失敗");
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