using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_StudentReRecord_Upload : BasePage
{

    public Vaccination_RecordM_StudentReRecord_Upload()
    {
        base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.上傳);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}