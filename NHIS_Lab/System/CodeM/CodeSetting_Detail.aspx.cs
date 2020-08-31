using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;

public partial class System_CodeM_CodeSetting_Detail : BasePage
{
    public new int ID { get; set; }
    public MyPowerVM AddPower { get; set; }
    public SystemCodeCateVM VM { get; set; }
    List<MyPowerVM> list = new List<MyPowerVM>();
    public System_CodeM_CodeSetting_Detail()
    {
       list=  base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AddPower=base.GetPower(list[1]);

        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        ID = GetNumber<int>("i");
        
        if (ID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        DataTable dt = GetDataTable("ConnDB", "dbo.usp_CodeM_xGetSystemCodeByID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@SystemCodeCateID", ID }
                                        });
        VM = new SystemCodeCateVM();
        EntityS.FillModel(VM, dt);
    }

    
}