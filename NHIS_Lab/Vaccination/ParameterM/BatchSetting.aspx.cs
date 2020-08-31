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

public partial class Vaccination_ParameterM_BatchSetting : BasePage
{
    public string tbData1 { get; set; }
    public string tbData2 { get; set; }

    public Vaccination_ParameterM_BatchSetting()
    {
        base.powerLogicType = PowerLogicType.AND;
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx",MyPowerEnum.查詢,MyPowerEnum.新增,MyPowerEnum.修改,MyPowerEnum.刪除);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        
    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}