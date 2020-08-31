using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

public class ReportMachine
{
    private volatile bool success = false;

    public ReportMachine()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public void Run(ApplyType applyType,string ClassName,string MD5, int ReportCateID,int ReportType, Dictionary<string,object> dict, Action<string> action=null)
    {
        var request = HttpContext.Current.Request;
        var response = HttpContext.Current.Response;
        ReportResultVM result = new ReportResultVM();
        Task.Factory.StartNew(() =>
        {
            try
            {
                int MyID = 0;
                bool CanRun = false;
                int Chk = 0;

                //check seed
                Dictionary<string, object> OutDict = new Dictionary<string, object>() {
                                                                                        { "@MyID", MyID },
                                                                                        { "@CanRun", CanRun },
                                                                                        { "@Chk", Chk }
                                                                                      };

                //var dt = MSDB.GetDataTable("ConnDB", "dbo.usp_ParameterM_xAddOrUpdateDefaultVaccine"
                //                                 , ref OutDict
                //                                 , new Dictionary<string, object>()
                //                                 {
                //                                    { "@MD5", MD5 },
                //                                    { "@OrgID", OrgID },
                //                                    { "@ReportCateID", ReportCateID },
                //                                    //{ "@SeedStorageFileID",0  },
                //                                    //{ "@FruitStorageFileID" ,0 },
                //                                    //{ "@ExcelStorageFileID",0  },
                //                                    { "@ApplyType",applyType },
                //                                    { "@ReportType",1 },
                //                                });

                Chk = (int)OutDict["@Chk"];

                //if(CanRun==true)
                if (true)
                {
                    //upload seed
                    //call WebService
                    //var json = "";
                    //
                    NIIS_Report.WebServiceSoapClient WS = new NIIS_Report.WebServiceSoapClient();
                    var json = WS.SendTask(ClassName, MD5, ReportCateID, ReportType,(int)applyType, JsonConvert.SerializeObject(dict));
                    result = JsonConvert.DeserializeObject<ReportResultVM>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
                    //result.ResultFile = new byte[] { 1,2,3};
                }
                else
                {
                    //ReportDataVM VM = new ReportDataVM();
                    //EntityS.FillModel(VM,dt);
                    //get ReportResultVM

                }

                switch (applyType)
                {
                    case ApplyType.預覽列印:
                        if(action!=null)
                        {
                            success = true;
                            //JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                            //string Serialized = JsonConvert.SerializeObject(inheritanceList, settings);
                            action(JsonConvert.SerializeObject(result.ResultData));
                        }
                        break;
                    case ApplyType.匯出xls:
                        success = true;
                        //var filePath = Path.Combine(Path.GetDirectoryName(request.PhysicalPath), Path.GetFileNameWithoutExtension(request.PhysicalPath) + ".xlsx");
                        //response.ContentType = "application/download";
                        //response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("預防接種紀錄表") + Path.GetExtension(filePath));
                        //response.TransmitFile(filePath);
                        //response.End();
                        response.Clear();
                        MemoryStream ms = new MemoryStream(result.ResultFile);
                        response.ContentType = "application/pdf";
                        response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(result.DisplayFileName));
                        response.Buffer = true;
                        ms.WriteTo(response.OutputStream);
                        response.End();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                #if(Debug)
                    Console.WriteLine(ex.Message);
                    response.Write(ex.Message);
                    response.End();
                #endif
            }
        });


        response.BufferOutput = false;
        response.Write("<html><body><div id='fakeLoader'></div>");
        response.Write("<link rel='stylesheet' href='/css/fakeLoader.css'/>");
        response.Write("<script src='/js/jq/jquery-2.1.4.min.js'></script>");
        response.Write("<script src='/js/jq/fakeLoader.min.js'></script>");
        response.Write("<script>$('#fakeLoader').fakeLoader({timeToHide:60000,bgColor:'#f7cecd'});</script>");
        for (int i = 10; i > 0; i--)
        {
            if (success)
                break;
            response.Write("<script>document.getElementById('spnDisp').innerHTML='" + string.Format("倒數 {0} 秒", i) + "';</script>");

            response.Flush();
            Thread.Sleep(1000);
        }
        response.Write("<script>$('#fakeLoader').fadeOut();document.getElementById('spnDisp').innerText='';</script>");
        if (success == false)
        {
            response.Write("<script>alert('運算進行中，請等候通知');</script>");
        }
        //else
        //{
        //    response.Write("<a id='btnGetFile' href='http://www.google.com' target='_blank'></a>");
        //    response.Write("<script>document.getElementById('btnGetFile').click();</script>");
        //}
        response.Write("</body></html>");

    }
}

