using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Configuration;

public partial class System_OrgM_OrgManagement_RemoveOP : BasePage
{
    public System_OrgM_OrgManagement_RemoveOP()
    {
        base.AddPower("/System/OrgM/OrgManagement.aspx", MyPowerEnum.刪除);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int ID = GetNumber<int>("i");
       
        if (ID > 0)
        {
            int Chk = 0;
            int OrgID = 0;

            Dictionary<string, object> OutDict = new Dictionary<string, object>() {
                                                                                    { "@Chk", Chk }
                                                                                };

            MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_OrgM_xRemoveOrgDetailByID"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@OrgID", ID }
                                            });

            Chk = (int)OutDict["@Chk"];

            OPVM VM = new OPVM();
            VM.chk = Chk;

            if (Chk>0)
            {
                SystemOrg.Update();
            }
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
            Response.End();
        }
    }
}
