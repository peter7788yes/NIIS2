using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_ParameterM_BatchSetting_xGetDefaultBatchVaccineOP : BasePage
{
    public Vaccination_ParameterM_BatchSetting_xGetDefaultBatchVaccineOP()
    {
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int VaccineID = GetNumber<int>("i");

        if (VaccineID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_ParameterM_xGetDefaultBatchByVaccineDataID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@VaccineDataID", VaccineID }
                                        });

        List<BatchSettingVM> list = new List<BatchSettingVM>();
        EntityS.FillModel(list, dt);

        List<BatchSettingVM> outList = new List<BatchSettingVM>();
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
                outList[index].StorageBottle += item.StorageBottle;
            }
        }

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(outList));
        Response.End();
    }
}