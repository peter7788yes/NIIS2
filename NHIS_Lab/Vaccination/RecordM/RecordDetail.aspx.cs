﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_RecordM_RecordDetail : BasePage
{
    public string ApplyRecordAry = "[]";
    public string ApplyHealthAry = "[]";
    public string ApplyEffectAry = "[]";
    public SimpleCaseUserVM VM = new SimpleCaseUserVM();

    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AgeEnglish = "";
    public string AppointmentDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";


        if (Request.HttpMethod.Equals("POST"))
        {

            int.TryParse(Request.Form["c"], out CaseUserID);
            int.TryParse(Request.Form["i"], out RecordDataID);
            VaccineCode = Request.Form["r"] ?? "";
            AppointmentDate = Request.Form["a"] ?? "";
            AgeEnglish = Server.UrlDecode(Request.Form["ae"] ?? "");

            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            AppointmentDate = date.ToShortTaiwanDate();

            if (success == false || CaseUserID == 0 || RecordDataID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetSimpleCaseUserByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            EntityS.FillModel(VM, dt);

           


            dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetApplyRecordByRecordDataID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<ApplyRecordVM> listR = new List<ApplyRecordVM>();
            EntityS.FillModel(listR, dt);
            ApplyRecordAry = JsonConvert.SerializeObject(listR);

            dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetApplyHealthByRecordDataID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<ApplyHealthVM> listH = new List<ApplyHealthVM>();
            EntityS.FillModel(listH, dt);
            ApplyHealthAry = JsonConvert.SerializeObject(listH);



            dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetApplyEffectDetailByRecordDataID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }


            List<ApplyEffectVM> listE = new List<ApplyEffectVM>();
            EntityS.FillModel(listE, dt);
            ApplyEffectAry = JsonConvert.SerializeObject(listE);
        }
        else
        {
            Response.Write("");
            Response.End();
        }


    }

}