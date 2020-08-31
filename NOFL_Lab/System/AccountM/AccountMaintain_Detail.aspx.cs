using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using WayneEntity;
using System.Web.Configuration;

public partial class System_AccountM_AccountMaintain_Detail : BasePage
{

    public AccountDetailVM VM = default(AccountDetailVM);
    public string ApplyDate = "";

    public System_AccountM_AccountMaintain_Detail()
    {
        base.AddPower("/System/AccountM/AccountMaintain.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (Request.HttpMethod.Equals("POST"))
        {
            int UserID = 0;
            int.TryParse(Request.Form["i"], out UserID);

            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_AccountM_xGetAccountDetailByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            VM = new AccountDetailVM();
            EntityS.FillModel(VM, dt);
            ApplyDate= VM.ApplyDate.ToShortTaiwanDate();

        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}