using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class System_CodeM_CodeSetting_Add : BasePage
{
    public int ID = 0;
    bool IsValid = true;
   
    public System_CodeM_CodeSetting_Add()
    {
        base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);


        ID = GetNumber<int>("i");

        if (ID == 0)
        {
            IsValid = false;
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        if (this.IsPostBack == false)
        {


            DataTable dt = GetDataTable("ConnDB", "dbo.usp_CodeM_xGetSystemCodeByID"
                                             , new Dictionary<string, object>()
                                             {
                                              { "@SystemCodeCateID", ID }
                                            });
            SystemCodeCateVM VM = new SystemCodeCateVM();
            EntityS.FillModel(VM, dt);
            lblCate.Text = VM.CodeDescription;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;
        
        int EnumValue = 0;
        string EnumName = PureString(tbName.Text);
        int OrderNumber = 0;

        int.TryParse(tbValue.Text, out EnumValue);
        int.TryParse(tbSort.Text, out OrderNumber);
        
        UserVM user =AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_CodeM_xAddSystemCode"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                { "@SystemCodeCateID", ID },
                                                { "@EnumValue", EnumValue },
                                                { "@EnumName", EnumName },
                                                { "@CanEdit", 0 },
                                                { "@OrderNumber", OrderNumber },
                                                { "@CreatedUserID",  user.ID }
                                            

                                        });

        Chk = (int)OutDict["@Chk"];
        

        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');history.go(-2);</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}