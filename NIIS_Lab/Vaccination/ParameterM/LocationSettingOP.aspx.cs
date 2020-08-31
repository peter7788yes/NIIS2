using System;
using System.Collections.Generic;

public partial class VaccinationM_LocationSettingOP : BasePage
{
    public VaccinationM_LocationSettingOP()
    {
        base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");

        int OrgID = GetNumber<int>("oid");
        int AgencyCounty = GetNumber<int>("ac");
        int AgencyTown = GetNumber<int>("at");
        int AgencyVillage = GetNumber<int>("av");
        int AgencyState = GetNumber<int>("as");
        string AgencyName = GetString("an");

        PageCL CL = new PageCL();
        string tbData = CL.GetList(new List<AgencyInfoVM>(),"ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
                                         new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID },
                                              { "@AgencyCounty", AgencyCounty  },
                                              { "@AgencyTown", AgencyTown },
                                              { "@AgencyVillage", AgencyVillage },
                                              { "@AgencyState", AgencyState },
                                              { "@AgencyName", AgencyName },
                                              { "@OrgLevel", 0},
                                              { "@pgNow", pgNow == 0 ? 1 : pgNow },
                                              { "@pgSize", pgSize == 0 ? 10 : pgSize }
                                        });
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(tbData);
        Response.End();

        //DataSet ds = GetDataSet("ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany"
        //                                 , new Dictionary<string, object>()
        //                                 {
        //                                      { "@OrgID", OrgID },
        //                                      { "@AgencyCounty", AgencyCounty  },
        //                                      { "@AgencyTown", AgencyTown },
        //                                      { "@AgencyVillage", AgencyVillage },
        //                                      { "@AgencyState", AgencyState },
        //                                      { "@AgencyName", AgencyName },
        //                                      { "@pgNow", pgNow == 0 ? 1 : pgNow },
        //                                      { "@pgSize", pgSize == 0 ? 10 : pgSize }
        //                                });

        //List<AgencyInfoVM> list = new List<AgencyInfoVM>();
        //PageVM rtn = new PageVM();

        //EntityS.FillModel(list, ds.Tables[0]);
        //EntityS.FillModel(rtn, ds.Tables[1]);
        //rtn.message = list;

        //Response.ContentType = "application/json; charset=utf-8";
        //Response.Write(JsonConvert.SerializeObject(rtn));
        //Response.End();

        //Response.AppendHeader("Content-Encoding", "gzip");
        //GZippedJsonT gzip = new GZippedJsonT();
        //var data = gzip.Compress(JsonConvert.SerializeObject(rtn, Formatting.Indented));
        ////Response.AppendHeader("Content-Length", data.Length.ToString());
        ////Response.ContentType = "application/json; charset=utf-8";
        ////Response.ContentEncoding = Encoding.UTF8;
        //Response.OutputStream.Write(data, 0, data.Length);
        //Response.End();

        //int pgNow;
        //int pgSize;


        //int.TryParse(Request.Form["pgNow"], out pgNow);
        //int.TryParse(Request.Form["pgSize"], out pgSize);

        //DataSet ds = new DataSet();

        //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        //{
        //    using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetAgencyList", sc))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
        //        cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            sc.Open();
        //            da.Fill(ds);
        //        }

        //    }
        //}

        //List<AgencyInfoVM> list = new List<AgencyInfoVM>();
        //PageVM rtn = new PageVM();



        //EntityS.FillModel(list, ds.Tables[0]);
        //EntityS.FillModel(rtn, ds.Tables[1]);
        //rtn.message = list;

        //Response.ContentType = "application/json; charset=utf-8";
        //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        //Response.End();
    }
}