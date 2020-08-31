﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_StockCommonPage_Delete_ListDataOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        string PageType;
        int BatchID = 0;
        int Success = 0;

        PageType = Request.Form["Page"];
        int.TryParse(Request.Form["BatchID"], out BatchID);

        if (BatchID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
        }

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_Vaccine" + PageType + "_xDeleteVaccine" + PageType + "BatchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", BatchID);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();
                Success = (int)sp.Value;

            }
        }

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("Success", Success);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}