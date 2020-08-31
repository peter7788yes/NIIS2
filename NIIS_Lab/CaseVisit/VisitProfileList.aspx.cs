using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Configuration;

public partial class CaseVisit_VisitProfileList: BasePage
{   public List<MyPowerVM> PowerList = new List<MyPowerVM>(); 
    public bool bAdd = false;
    public bool bEdit = false;

    public CaseVisit_VisitProfileList()
    {
        PowerList = base.AddPower("/CaseVisit/VisitProfileList.aspx"
            , MyPowerEnum.修改
            , MyPowerEnum.新增
              , MyPowerEnum.刪除
            , MyPowerEnum.查詢
              , MyPowerEnum.瀏覽 
            );
           
    }
    //protected string  CountyData = "[]";
    //protected string  TownData = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {

        //foreach (MyPowerVM p in PowerList)
        //    Response.Write(p.myPowerEnum.ToString() + " - " + base.GetPower(p).HasPower.ToString()+"<br/>");
 
    

        ucUserProfileList1.DisplayMode = UseModule.個案訪查維護;



    }
     
     

}