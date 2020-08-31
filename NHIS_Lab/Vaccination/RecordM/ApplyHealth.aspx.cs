using Newtonsoft.Json;
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


public partial class Vaccination_RecordM_ApplyHealth : BasePage
{
    public string UserName = "";
    public string StateListAry = "[]";
    public string UserAry = "[]";
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";

    public string nowDate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

   

        if (Request.HttpMethod.Equals("POST"))
        {
            nowDate = DateTime.Now.ToShortTaiwanDate();
            int.TryParse(Request.Form["c"], out CaseUserID);
            int.TryParse(Request.Form["i"], out RecordDataID);
            VaccineCode = Request.Form["r"] ?? "";
            AppointmentDate = Request.Form["a"] ?? "";
            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            //AppointmentDate = date.ToShortTaiwanDate();

            if (success == false || CaseUserID == 0 || RecordDataID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


            if (SystemCode.dict.ContainsKey("RecordM_ApplyHealthCate"))
            {
                StateListAry = JsonConvert.SerializeObject(SystemCode.dict["RecordM_ApplyHealthCate"].OrderBy(item => item.OrderNumber));
            }

            var user = AuthServer.GetLoginUser();
            UserName = user.UserName;

            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_AccountM_xGetUserListByOrgID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<UserNameIDVM> list = new List<UserNameIDVM>();

            EntityS.FillModel(list, dt);

            UserAry = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

  
}