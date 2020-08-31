using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_CodeM_CodeSetting_Detail : BasePage
{
    public new int ID { get; set; }
    public new MyPowerVM AddPower { get; set; }
    public SystemCodeCateVM VM = new SystemCodeCateVM();
    List<MyPowerVM> list = new List<MyPowerVM>();
    public string tbData = "";

    public System_CodeM_CodeSetting_Detail()
    {
       list = base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        AddPower = base.GetPower(list[1]);

        ID = GetNumber<int>("i");
        
        if (ID == 0)
        {
            string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_CodeM_xGetSystemCodeListByCateID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@SystemCodeCateID", ID }
                                        });
       
        EntityS.FillModel(VM, dt);

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<SystemCodeVM>(), "ConnDB", "dbo.usp_CodeM_xGetEnabledSystemCodeList",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                       { "@SystemCodeCateID",  ID },
        //                                       { "@EnState",  0 },
        //                                       { "@pgNow",1},
        //                                       { "@pgSize", 10}
        //                                });
    }

    
}