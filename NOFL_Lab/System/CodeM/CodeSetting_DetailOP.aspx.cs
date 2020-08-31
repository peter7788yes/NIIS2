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

public partial class System_CodeM_CodeSetting_DetailOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int ID = 0;
        int pgNow;
        int pgSize;

        int.TryParse(Request.Form["i"], out ID);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xGetEnabledSystemCodeList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemCodeCateID", pgNow == 0 ? 1 : ID);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<SystemCodeVM> list = new List<SystemCodeVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}