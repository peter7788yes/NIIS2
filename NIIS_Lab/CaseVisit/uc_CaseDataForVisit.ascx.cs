using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseVisit_uc_CaseDataForVisit : System.Web.UI.UserControl
{

    public int iCaseID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (iCaseID!=null)
                  BindData();
    }
    protected void BindData()
    {

        CaseUserProfile c = new CaseUserProfile();
        c.GetProfileWithMother(iCaseID);
        if (c.CaseID > 0)
        {
            ltName.Text = c.ChName;
            ltBirthDate.Text = c.BirthDate;
            ltHouseNo.Text = c.HouseNo;
            ltIdNo.Text = c.IdNo;
            ltGender.Text = c.GenderName;
            ltLang.Text = c.LanguageName;
            ltConAddr.Text = c.ConFullAddress;
            ltMotherName.Text = c.MotherName;
            ltMotherIdNo.Text = c.MotherIdNo;
            ltMotherBirthDate.Text = c.MotherBirthDate;
            ltCap.Text = c.CapacityName;
            ltResAddr.Text = c.ResFullAddress;
            ltRegionName.Text = c.RegionName;
        }
    }


    
}