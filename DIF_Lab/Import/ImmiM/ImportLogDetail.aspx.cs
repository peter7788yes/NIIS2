using System;

public partial class Import_ImmiM_ImportLogDetail : BasePage
{
    public string ImportDate { get; set; }
    public int DataCnt { get; set; }
    public int MasterID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterID = GetNumber<int>("id", MyHttpMethod.GET);
        DataCnt = GetNumber<int>("DataCnt", MyHttpMethod.GET);
        ImportDate = GetString("ImportDate", MyHttpMethod.GET);
    }
}