using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class Vaccination_RecordM_ApplyRecord_AddVaccine : BasePage
{
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";
    public string tbAry = "[]";
    public string Agency = "";
    public int AgencyID = 0;

    public Vaccination_RecordM_ApplyRecord_AddVaccine()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        string VaccineID = GetString("v");

        if (Request.HttpMethod.Equals("POST"))
        {
            if (this.IsPostBack == false)
            {
                var user = AuthServer.GetLoginUser();
                

                DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetDefaultBatchVaccineByOrgIDWithoutVaccineID"
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@OrgID", user.OrgID },
                                                    { "@VaccineID", VaccineID }
                                            });

                List<DefaultBatchVaccineVM> list = new List<DefaultBatchVaccineVM>();
                EntityS.FillModel(list, dt);

                List<DefaultBatchVaccineVM> outList = new List<DefaultBatchVaccineVM>();
                var indexList = new List<Tuple<int, int>>();
                foreach (var item in list)
                {
                    bool HasValue = false;
                    int index = 0;
                    indexList.ForEach((innerItem) => {
                        if (HasValue == false)
                        {
                            if (innerItem.Item1 == item.VaccineBatchID)
                            {
                                HasValue = true;
                            }
                            else
                            {
                                index++;
                            }
                        }
                    });
                    if (HasValue == false)
                    {
                        outList.Add(item);
                        indexList.Add(Tuple.Create(item.VaccineBatchID, outList.Count - 1));
                    }
                    else
                    {
                        outList[index].Storage += item.Storage;
                    }
                }

                outList.RemoveAll((item) => item.Storage == 0);
                tbAry = JsonConvert.SerializeObject(outList);

            }

        }
        else
        {
            Response.Write("");
            Response.End();
        }

    }

}