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

public partial class System_FrequentlyAskedQuestionM_QnAData_GetQnADataOP : BasePage
{
    public System_FrequentlyAskedQuestionM_QnAData_GetQnADataOP()
    {
        base.AddPower("/System/FrequentlyAskedQuestionM/QnAData/QnAData.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int ID;

        int.TryParse(Request.Form["ID"], out ID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_QnAData_xGetQnAData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<QnADataVM> list = new List<QnADataVM>();
        List<CreateInfoVM> list1 = new List<CreateInfoVM>();
        List<ModifyInfoVM> list2 = new List<ModifyInfoVM>();
        List<DocumentFileVM> list3 = new List<DocumentFileVM>();
        HasFileDataVM rtn = new HasFileDataVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(list1, ds.Tables[1]);
        EntityS.FillModel(list2, ds.Tables[2]);
        EntityS.FillModel(list3, ds.Tables[3]);

        rtn.Datalist = list;
        rtn.CreateInfo = list1;
        rtn.ModifyInfo = list2;
        rtn.Filelist = list3;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}