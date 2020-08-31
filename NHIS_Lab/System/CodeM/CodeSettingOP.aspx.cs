using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_CodeM_CodeSettingOP : BasePage
{

    public System_CodeM_CodeSettingOP()
    {
        base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");


        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeCateList"
                                 , new Dictionary<string, object>()
                                 {
                                       { "@pgNow", pgNow == 0 ? 1 : pgNow},
                                       { "@pgSize", pgSize == 0 ? 10 : pgSize}
                                 });

        List<SystemCodeCateVM> list = new List<SystemCodeCateVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}