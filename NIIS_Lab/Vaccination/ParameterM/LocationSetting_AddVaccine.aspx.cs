using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_ParameterM_LocationSetting_AddVaccine : BasePage
{ 
    public string ListJson { get; set; }

    public Vaccination_ParameterM_LocationSetting_AddVaccine()
    {
        base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            var user = AuthServer.GetLoginUser();

            int AgencyInfoID = GetNumber<int>("i");

            List<int> IDsList = GetList<int>("is");
            //string IDs = Request["is"] ?? "";

            //bool IsValid = false;
            //List<int> IDsList = new List<int>();
            //try
            //{
            //    IDsList = IDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            //                 .ToList<string>()
            //                 .ConvertAll<int>(item => int.Parse(item));

            //    IsValid = true;
            //}
            //catch
            //{
            //}

            if (AgencyInfoID == 0)//|| IsValid==false)
            {
                string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_ParameterM_xGetVaccineByAgencyID"
                                              , new Dictionary<string, object>()
                                              {
                                              { "@AgencyInfoID", AgencyInfoID },
                                             });

            List<AddVaccineVM> list = new List<AddVaccineVM>();
            EntityS.FillModel(list, dt);

            if (IDsList.Count > 0)
            {
                foreach (var item in list)
                {
                    if (IDsList.Contains(item.ID))
                    {
                        item.IsChecked = true;
                    }
                }
            }

            ListJson = JsonConvert.SerializeObject(list);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}