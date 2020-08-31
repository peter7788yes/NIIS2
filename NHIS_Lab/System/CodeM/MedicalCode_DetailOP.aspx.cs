using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class CodeM_MedicalCode_DetailOP : BasePage
{
    public CodeM_MedicalCode_DetailOP()
    {
        base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        int OrgID = GetNumber<int>("i");

        DataSet ds = MSDB.GetDataSet("ConnUser", "dbo.usp_CodeM_xGetOrgChangeLogListByID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID }
                                        });

        List<MedicalCodeVM> list = new List<MedicalCodeVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(rtn));
        Response.End();

    }
}