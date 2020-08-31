using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseVisit_VisitCaseList: BasePage
{   public List<MyPowerVM> PowerList = new List<MyPowerVM>(); 
    public bool bAdd = false;
    public bool bEdit = false;

    public CaseVisit_VisitCaseList()
    {
        PowerList = base.AddPower("/CaseVisit/VisitProfileList.aspx"
    , MyPowerEnum.修改
    , MyPowerEnum.新增 
    , MyPowerEnum.瀏覽
    , MyPowerEnum.查詢
     );
           
    }

    public int iCaseID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);
        bAdd = base.GetPower(PowerList[1]).HasPower;
        bEdit = base.GetPower(PowerList[0]).HasPower;


        QS();
        if (iCaseID > 0)
            uc_CaseDataForVisit1.iCaseID = iCaseID;

    }

  
    protected void QS()
    {
        if (Request["i"] != null)
             int.TryParse(Request["i"].ToString(), out iCaseID);
    
    }
}