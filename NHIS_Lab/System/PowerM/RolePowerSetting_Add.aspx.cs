using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class System_PowerM_RolePowerSetting_Add : BasePage
{

    public int RoleCateID { get; set; }

    public string RoleCateName { get; set; }
    


    public string RoleName  
    {
        get
        {
            return tbName.Text.Trim();
        }
    }
    public string RoleDescription
    {
        get
        {
            return  tbDesp.Text.Trim();
        }
    }
    public string RoleLevelName
    {
        get
        {
            string rtn = "";
            RadioButton selectLevel = null;
            selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            if (selectLevel != null)
            {
                rtn = selectLevel.Text;
            }

            return rtn;
        }
        set{}

    }

    public int RoleLevel
    {
        get
        {

            int rtn = 1;
            RadioButton selectLevel = null;
            selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            if (selectLevel != null)
            {
                switch (selectLevel.ID)
                {
                    case "rb1":
                        rtn = 1;
                        break;
                    case "rb2":
                        rtn = 2;
                        break;
                    case "rb3":
                        rtn = 3;
                        break;
                    case "rb4":
                        rtn = 4;
                        break;
                }
            }
            return rtn;

        }
        set { }

    }

    public System_PowerM_RolePowerSetting_Add()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        if (Request.HttpMethod.Equals("POST"))
        {
            int tmpRoleCateID = GetNumber<int>("sc");
            ddlCate.SelectedValue = tmpRoleCateID.ToString();
            RoleCateID = tmpRoleCateID;
            RoleCateName = PureString(ddlCate.SelectedItem.Text);
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        //int OutRoleID = 0;
        //int RoleLevel = 0;

        //RadioButton selectLevel = null;
        //selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        //if (selectLevel != null)
        //{
        //    switch(selectLevel.ID)
        //    {
        //        case "rb1":
        //            RoleLevel = 1;
        //            break;
        //        case "rb2":
        //            RoleLevel = 2;
        //            break;
        //        case "rb3":
        //            RoleLevel = 3;
        //            break;
        //        case "rb4":
        //            RoleLevel = 4;
        //            break;
        //    }
        //}

        //using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        //{
        //    using (SqlCommand cmd = new SqlCommand("dbo.usp_CodeM_xAddSystemCode", sc))
        //    {

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@RoleName", RoleName);
        //        cmd.Parameters.AddWithValue("@RoleLevel", RoleLevel);
        //        cmd.Parameters.AddWithValue("@RoleDescription", RoleDescription);
        //        SqlParameter sp = cmd.Parameters.AddWithValue("@OutRoleID", OutRoleID);
        //        sp.Direction = ParameterDirection.Output;

        //        sc.Open();
        //        cmd.ExecuteNonQuery();

        //        OutRoleID = (int)sp.Value;
        //    }
        //}

        //string script = "";

        //if (OutRoleID > 0)
        //{
        //    Response.Redirect();
        //}
        //else
        //{
        //    script = "<script>alert('儲存失敗');</script>";
        //}

        //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}