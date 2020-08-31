using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_PowerM_RolePowerSetting_Add : BasePage
{
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
    public OrgLevelEnum orgLevelEnum
    {
        get
        {
            OrgLevelEnum tmpOrgLevelEnum = default(OrgLevelEnum);
            RadioButton selectLevel = null;
            selectLevel = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            if (selectLevel != null)
            {
                Enum.TryParse(selectLevel.Text, out tmpOrgLevelEnum);
            }

            return tmpOrgLevelEnum;
        }
        set{}

    }

    public System_PowerM_RolePowerSetting_Add()
    {
        base.AddPower("/System/PowerM/RolePowerSetting.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }

    
}