using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_CodeM_CodeSetting_DetailOP : BasePage
{

    public System_CodeM_CodeSetting_DetailOP()
    {
        base.AddPower("/System/CodeM/CodeSetting_Detail.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int ID = GetNumber<int>("i");
        int EnState = GetNumber<int>("en");
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeList"
                                , new Dictionary<string, object>()
                                {
                                       { "@SystemCodeCateID",  ID },
                                       { "@EnState",  EnState },
                                       { "@pgNow", pgNow == 0 ? 1 : pgNow},
                                       { "@pgSize", pgSize == 0 ? 10 : pgSize}
                                });

      
        List<SystemCodeVM> list = new List<SystemCodeVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}