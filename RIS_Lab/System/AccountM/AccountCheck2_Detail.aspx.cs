using System;

public partial class System_AccountM_AccountCheck2_Detail : BasePage
{
    public int AccountCheckID = 0;

    public System_AccountM_AccountCheck2_Detail()
    {
        base.AddPower("/System/AccountM/AccountCheck2.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        AccountCheckID = GetNumber<int>("i");
       
        if (Request.HttpMethod.Equals("POST"))
        {
            
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}