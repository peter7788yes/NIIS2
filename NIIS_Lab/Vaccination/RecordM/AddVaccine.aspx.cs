using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WayneEntity;

public partial class Vaccination_RecordM_AddVaccine : BasePage
{
    public int CaseUserID = 0;
    public string tbAry = "[]";

    public Vaccination_RecordM_AddVaccine()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            CaseUserID = GetNumber<int>("c");
            List<string> VaccineList = GetList<string>("a");

            var dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetVaccineList"
                                         , new Dictionary<string, object>()
                                         {
                                             
                                        });

            List<AddVaccineVM> list = new List<AddVaccineVM>();
            EntityS.FillModel(list, dt);

            var listDiff = list.Where(item => (VaccineList.Contains(item.VaccineID) == false));
            if (listDiff.Count() > 0)
            {
                tbAry = JsonConvert.SerializeObject(listDiff);
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

}