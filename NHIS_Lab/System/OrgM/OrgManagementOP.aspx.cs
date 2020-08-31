using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

public partial class System_OrgM_OrgManagementOP : BasePage
{

    public System_OrgM_OrgManagementOP()
    {
        base.AddPower("/System/OrgM/OrgManagement.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int ID = GetNumber<int>("i");

        if (ID > 0)
        {
            DataSet ds = MSDB.GetDataSet("ConnUser", "dbo.usp_OrgM_xGetOrgDetailByID"
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@OrgID", ID }
                                            });

            OrgManagementVM VM = new OrgManagementVM();
            List<OrgAllowIPVM> list = new List<OrgAllowIPVM>();
            WayneEntity.EntityS.FillModel(VM, ds.Tables[0]);
            WayneEntity.EntityS.FillModel(list, ds.Tables[1]);
            VM.OrgAllowIPList = list;

            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(JsonConvert.SerializeObject(VM));
            Response.End();
        }
    }
}
