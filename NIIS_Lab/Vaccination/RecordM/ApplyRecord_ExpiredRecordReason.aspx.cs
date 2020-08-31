using System;

public partial class Vaccination_RecordM_ApplyRecord_ExpiredRecordReason : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
    }

    public Vaccination_RecordM_ApplyRecord_ExpiredRecordReason()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}