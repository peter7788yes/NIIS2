using System;

public partial class Import_EmiM_ImportLogDetail : BasePage
{
    public int SuccessCnt { get; set; }
    public int RepeatCnt { get; set; }
    public int ErrorCnt { get; set; }
    public int TotalCnt { get; set; }
    public int MasterID { get; set; }
    public string UserName { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterID = GetNumber<int>("id", MyHttpMethod.GET);
        SuccessCnt = GetNumber<int>("s", MyHttpMethod.GET);
        RepeatCnt = GetNumber<int>("r", MyHttpMethod.GET);
        ErrorCnt = GetNumber<int>("e", MyHttpMethod.GET);
        TotalCnt = SuccessCnt + RepeatCnt + ErrorCnt;
        UserName = GetString("u", MyHttpMethod.GET);
    }
}