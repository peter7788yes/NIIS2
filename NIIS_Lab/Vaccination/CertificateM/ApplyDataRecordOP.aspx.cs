using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_CertificateM_ApplyDataRecordOP : BasePage
{
    public Vaccination_CertificateM_ApplyDataRecordOP()
    {
        base.AddPower("/Vaccination/CertificateM/PrintCertificate.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");
        int CaseUserID = GetNumber<int>("i");

        DataSet ds  = MSDB.GetDataSet("ConnDB", "dbo.usp_CertificateM_xGetApplyDataList"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@CaseUserID", CaseUserID },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
       
        List<ApplyDataRecordVM> list = new List<ApplyDataRecordVM>();
        List<ApplyDataRecordFileVM> FileList = new List<ApplyDataRecordFileVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        EntityS.FillModel(FileList, ds.Tables[2]);

        rtn.message = list;
        rtn.message2 = FileList;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}