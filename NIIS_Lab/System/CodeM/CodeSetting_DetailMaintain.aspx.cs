using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using WayneEntity;

public partial class System_CodeM_CodeSetting_DetailMaintain : BasePage
{
    public int CateID { get; set; }
    public new int ID { get; set; }
    public MyPowerVM UpdatePower { get; set; }
    public SystemCodeVM VM = new SystemCodeVM();
    List<MyPowerVM> list = new List<MyPowerVM>();
    public new bool IsValid=true;

    public System_CodeM_CodeSetting_DetailMaintain()
    {
        list = base.AddPower("/System/CodeM/CodeSetting.aspx", MyPowerEnum.瀏覽, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);


        UpdatePower = base.GetPower(list[1]);

        CateID = GetNumber<int>("ci");
        ID = GetNumber<int>("i");

        if (ID == 0 || CateID == 0)
        {
            IsValid = false;
            string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        if (this.IsPostBack == false)
        {
            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_CodeM_xGetSystemCodeByID"
                                             , new Dictionary<string, object>()
                                             {
                                              { "@SystemCodeID", ID }
                                            });

            EntityS.FillModel(VM, dt);
            lblCate.Text = VM.CodeDescription;
            lblValue.Text = VM.EnumValue.ToString();
            tbName.Text = VM.EnumName;
            if (VM.IsEnabled)
            {
                rb1.Checked = true;
            }
            else
            {
                rb2.Checked = true;
            }
        }

    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;

        int Chk = 0;
       
        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_CodeM_xDeleteSystemCodeByID"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@SystemCodeID", ID }
                                         });

        Chk = (int)OutDict["@Chk"];

        string script = "";

        if (Chk > 0)
        {
            script = string.Format("{0}<script>alert('儲存成功');location.href='{1}#{2}';</script>",
                            "<style>body{display:none;}</style>"
                            , "/System/CodeM/CodeSetting_Detail.aspx?i="+CateID
                            , HttpUtility.HtmlDecode(GetString("hash") ?? "")
                          );

            SystemCode.Update();
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);



    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid == false)
            return;
        bool IsEnabled = false;
        string EnumName = PureString(tbName.Text);
        if (rb1.Checked)
            IsEnabled = true;


        UserVM user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_CodeM_xUpdateSystemCode"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                { "@SystemCodeID", ID },
                                                { "@EnumName", EnumName },
                                                { "@IsEnabled", IsEnabled },
                                        });

        Chk = (int)OutDict["@Chk"];

        string script = "<style>body{display:none;}</style>";
        if (Chk > 0)
        {
            script = string.Format("{0}<script>alert('儲存成功');location.href='{1}#{2}';</script>",
                           "<style>body{display:none;}</style>"
                           , "/System/CodeM/CodeSetting_Detail.aspx?i=" + CateID 
                           , HttpUtility.HtmlDecode(GetString("hash") ?? "")
                         );
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}