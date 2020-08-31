using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToExcel;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using System.IO;

namespace LConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            int work = 0;
            int count = 0;

            bool startWork = false;
            label1.Text = string.Format("匯入  {0}/{1} .。", "","");
            string Conn = File.ReadAllText("conn.conf");

            Task.Factory.StartNew(() =>
            {
                    var excel = new ExcelQueryFactory("excel.xlsx");
                    excel.AddMapping("RocID", "ID");
                    excel.AddMapping("Age", "適齡");
                    excel.AddMapping("vID", "劑別代號");
                    excel.AddMapping("aDate", "預定日期");
                    excel.AddMapping("iDate", "接種日");
                    excel.AddMapping("Org", "接種單位");
                    excel.AddMapping("bID", "批號");
                    excel.AddMapping("CreateType", "建檔方式");
                    excel.AddMapping("CreatedDate", "建檔日期");

                    var RowList = excel.Worksheet<xVM>("預種資料");

                    count = RowList.Count();
                    foreach (var item in RowList)
                    {
                        work++;
                        startWork = true;
                        try
                        {
                            MSDB.ExecuteNonQuery(Conn, "dbo.usp_LConvert"
                                                    , new Dictionary<string, object>()
                                                    {
                                                        { "@RocID", item.RocID ?? ""},
                                                        { "@Age", item.Age ?? ""},
                                                        { "@vID", item.vID ?? ""},
                                                        { "@db_aDate", item.db_aDate },
                                                        { "@db_iDate", item.db_iDate },
                                                        { "@Org", item.Org ?? "" },
                                                        { "@bID", item.bID ?? ""},
                                                        { "@CreateType", item.CreateType ?? ""},
                                                        { "@db_CreatedDate", item.db_CreatedDate }
                                                    });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    startWork = false;
            });

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                while (true)
                {
                    Thread.Sleep(200);

                    this.BeginInvoke(new Action(() =>
                    {
                        if (label1.Text.Contains(" .。"))
                        {
                            label1.Text = string.Format("匯入  {0}/{1} ..。", work == 0 ? "" : work.ToString(), count == 0 ? "" : count.ToString());
                        }
                        else if (label1.Text.Contains(" ..。"))
                        {
                            label1.Text = string.Format("匯入  {0}/{1} ...。", work == 0 ? "" : work.ToString(), count == 0 ? "" : count.ToString());
                        }
                        else if (label1.Text.Contains(" ...。"))
                        {
                            label1.Text = string.Format("匯入  {0}/{1} .。", work == 0 ? "" : work.ToString(), count == 0 ? "" : count.ToString());
                        }
                    }));

                    if (work>0 && work == count && startWork == false)
                    {
                        Thread.Sleep(10);
                        this.BeginInvoke(new Action(() =>
                        {
                            label1.Text = string.Format("完成  {0}/{1} .。", work == 0 ? "" : work.ToString(), count == 0 ? "" : count.ToString());
                        }));
                        break;
                    }
                }
            });
        }
    }

    public class xVM
    {
        //ID 
        public string RocID { get; set; }
        //適齡
        public string Age { get; set; }
        //劑別代號
        public string vID { get; set; }
        //預定日期
        public string aDate { get; set; }
        //接種日
        public string iDate { get; set; }
        //接種單位
        public string Org { get; set; }
        //批號
        public string bID { get; set; }
        //建檔方式
        public string CreateType { get; set; }
        //建檔日期
        public string CreatedDate { get; set; }

        public DateTime db_aDate {
            get
            {
                DateTime rtn = new DateTime(2099, 1, 1, 1, 1, 1, 0);
                bool success = DateTime.TryParseExact( aDate.RepublicToAD(),
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out rtn);

                if(success)
                   return rtn;
            
                return new DateTime(2099, 1, 1, 1, 1, 1, 0);
            }
        }

        public DateTime db_iDate
        {
            get
            {
                DateTime rtn = new DateTime(2099, 1, 1, 1, 1, 1, 0);
                bool success = DateTime.TryParseExact( iDate.RepublicToAD(),
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out rtn);

                if (success)
                    return rtn;
           
                return new DateTime(2099, 1, 1, 1, 1, 1, 0);
            }
        }

        public DateTime db_CreatedDate
        {
            get
            {
                DateTime rtn = new DateTime(2099, 1, 1, 1, 1, 1, 0);
                bool success = DateTime.TryParseExact(CreatedDate.RepublicToAD(),
                        "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out rtn);

                if (success)
                    return rtn;
                else
                    return new DateTime(2099, 1, 1, 1, 1, 1, 0);
            }
        }

    }
}
