﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;

public partial class Import_EmiM_ImportListOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int pgNow = GetNumber<int>("pgNow");
        int pgSize = GetNumber<int>("pgSize");
        string StartDate = "";
        string EndDate = "";
        string UserID = "";

        StartDate = GetString("StartDate");
        EndDate = GetString("EndDate");
        UserID = GetString("UserID");

        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_ImportEmi_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }

        List<EmiMasterVM> list = new List<EmiMasterVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}