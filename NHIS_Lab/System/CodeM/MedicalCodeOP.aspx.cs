using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using WayneTools;

public partial class CodeM_MedicalCodeOP : BasePage
{
    public CodeM_MedicalCodeOP()
    {
        base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        int OrgID = GetNumber<int>("oid");
        int AgencyCounty = GetNumber<int>("ac");
        int AgencyTown = GetNumber<int>("at");
        int AgencyVillage = GetNumber<int>("av");
        int BusinessState = GetNumber<int>("bs");
        string AgencyName = GetString("an");


        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_CodeM_xGetAgencyListByMany"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID },
                                              { "@AgencyCounty", AgencyCounty  },
                                              { "@AgencyTown", AgencyTown },
                                              { "@AgencyVillage", AgencyVillage },
                                              { "@BusinessState", BusinessState },
                                              { "@AgencyName", AgencyName },
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });

        List<AgencyInfoVM> list = new List<AgencyInfoVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(rtn));
        Response.End();

    }
}