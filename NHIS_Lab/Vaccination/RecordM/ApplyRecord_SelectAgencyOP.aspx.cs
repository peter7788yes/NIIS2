﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class VaccinationM_ApplyRecord_SelectAgencyOP : BasePage
{


    int OrgID = 0;
    int AgencyCounty = 0;
    int AgencyTown = 0;
    int AgencyVillage = 0;
    int AgencyState = 0;
    string AgencyName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");


        int pgNow;
        int pgSize;

        int.TryParse(Request.Form["ac"], out AgencyCounty);
        int.TryParse(Request.Form["at"], out AgencyTown);
        int.TryParse(Request.Form["av"], out AgencyVillage);
        int.TryParse(Request.Form["as"], out AgencyState);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        AgencyName = Request.Form["an"] ?? "";

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetAgencyListByMany", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgencyCounty", AgencyCounty);
                cmd.Parameters.AddWithValue("@AgencyTown", AgencyTown);
                cmd.Parameters.AddWithValue("@AgencyVillage", AgencyVillage);
                cmd.Parameters.AddWithValue("@AgencyState", AgencyState);
                cmd.Parameters.AddWithValue("@AgencyName", AgencyName);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }

        List<AgencyInfoVM> list = new List<AgencyInfoVM>();
        PageVM rtn = new PageVM();

      

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}