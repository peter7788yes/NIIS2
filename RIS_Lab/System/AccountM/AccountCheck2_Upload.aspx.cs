using System;

public partial class System_AccountM_AccountCheck2_Upload : BasePage
{
    public System_AccountM_AccountCheck2_Upload()
    {
        base.AddPower("/System/AccountM/AccountCheck2.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {

        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}