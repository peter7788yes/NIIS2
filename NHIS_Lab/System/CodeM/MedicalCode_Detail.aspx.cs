using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class System_CodeM_MedicalCode_Detail : BasePage
{
    public MedicalCodeVM VM = new MedicalCodeVM();
    public int ID = 0;
    public System_CodeM_MedicalCode_Detail()
    {
        base.AddPower("/System/CodeM/MedicalCode.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

            base.AllowHttpMethod("POST");
            base.DisableTop(true);

            ID = GetNumber<int>("i");

            if (ID == 0)
            {
                string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


            DataTable dt = GetDataTable("ConnDB", "dbo.usp_CodeM_xGetAgencyInfoByID"
                                             , new Dictionary<string, object>()
                                             {
                                              { "@OrgID", ID }
                                            });

            EntityS.FillModel(VM, dt);

    }

}