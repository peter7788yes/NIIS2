<Query Kind="Program">
  <Reference Relative="LinqToExcel.dll">C:\Users\A1037\Desktop\LC\LinqToExcel.dll</Reference>
  <Reference Relative="log4net.dll">C:\Users\A1037\Desktop\LC\log4net.dll</Reference>
  <Reference Relative="Remotion.Data.Linq.dll">C:\Users\A1037\Desktop\LC\Remotion.Data.Linq.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Globalization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Linq.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.Parallel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Timer.dll</Reference>
  <Namespace>System.Threading</Namespace>
</Query>



void Main()
{
 			int enterCount = 0;
            int work = 0;
            int count = 0;
            
            //label1.Text = string.Format("匯入  {0}/{1} .。", "","");

            string Conn = "";

            Conn = File.ReadAllText("conn.conf");
			
			
	 		System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                var excel = new LinqToExcel.ExcelQueryFactory("excel.xlsx");
                excel.AddMapping("RocID", "ID");
                excel.AddMapping("Age", "適齡");
                excel.AddMapping("vID", "劑別代號");
                excel.AddMapping("aDate", "預定日期");
                excel.AddMapping("iDate", "接種日");
                excel.AddMapping("Org", "接種單位");
                excel.AddMapping("bID", "批號");
                excel.AddMapping("CreateType", "建檔方式");
                excel.AddMapping("CreatedDate", "建檔日期");


                var RowList = excel.Worksheet<xVM>(1);
                count = RowList.Count();
                foreach (var item in RowList)
                {
					item.Dump();
					/*
                    try
                    {
                        work++;
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
					*/
                 }

                //MessageBox.Show("OK");
            });
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
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
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
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
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
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out rtn);

                if (success)
                    return rtn;
                else
                    return new DateTime(2099, 1, 1, 1, 1, 1, 0);
					
            }
        }

    }

 public static class DateTimeExtensions
    {
        public static string ToLongTaiwanDate(this DateTime dateTime,bool HasBlank=false)
        {
            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";

            System.Globalization.TaiwanCalendar TC = new System.Globalization.TaiwanCalendar();

            if (HasBlank)
            {
                return string.Format("民國 {0} 年 {1} 月 {2} 日"
                                    , TC.GetYear(dateTime)
                                    , dateTime.Month
                                    , dateTime.Day);
            }

            return string.Format("民國{0}年{1}月{2}日"
                                , TC.GetYear(dateTime)
                                , dateTime.Month
                                , dateTime.Day);
        }

        public static string ToShortTaiwanDate(this DateTime dateTime,string Split="")
        {

            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";
            System.Globalization.TaiwanCalendar TC = new System.Globalization.TaiwanCalendar();

            return string.Format("{0}" + Split + "{1}" + Split + "{2}"
                                 , TC.GetYear(dateTime)
                                 , dateTime.Month.ToString("00")
                                 , dateTime.Day.ToString("00"));
        }


        public static string ToShortTaiwanDateTime(this DateTime dateTime)
        {
            if (DateTime.Compare(dateTime, new DateTime(1912, 1, 1)) < 0)
                return "";
            System.Globalization.TaiwanCalendar TC = new System.Globalization.TaiwanCalendar();

            return string.Format("{0}/{1}/{2} {3}"
                                 , TC.GetYear(dateTime)
                                 , dateTime.Month
                                 , dateTime.Day
                                 , dateTime.ToString("HH:mm:ss"));
        }
    }


public static class StringExtensions
{
            public static string RepublicToAD(this string RepublicDate)
            {
                try
                {
                    if (RepublicDate != null && RepublicDate.Length > 0)
                    {
                        List<string> list = new List<string>();
                        RepublicDate = RepublicDate.Trim();
                        list.Add(RepublicDate.Substring(RepublicDate.Length - 2, 2));
                        list.Add(RepublicDate.Substring(RepublicDate.Length - 4, 2));
                        list.Add(RepublicDate.Substring(0, RepublicDate.Length - 4));
                        list.Reverse();

                        RepublicDate = string.Format("{0}{1}{2}",(Convert.ToInt32(list[0]) + 1911).ToString("00"), Convert.ToInt32(list[1]).ToString("00"), Convert.ToInt32(list[2]).ToString("00"));
                    }
                }
                catch
                {
                }

                return RepublicDate;
            }
}