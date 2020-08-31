using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class Vaccination_CertificateM_SignSetting : BasePage
{
    UserVM user = new UserVM();
    public int DefaultOrgID = 0;

    public Vaccination_CertificateM_SignSetting()
    {
        base.AddPower("/Vaccination/CertificateM/SignSetting.aspx", MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        user = AuthServer.GetLoginUser();
        DefaultOrgID = user.OrgID;

        UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/CertificateM/SignSetting.aspx";
        UC_OpenSelectSingleOrg.DefaultID = user.OrgID;
        UC_OpenSelectSingleOrg.DefaultName = user.OrgName;
        UC_OpenSelectSingleOrg.callback = "onSelectSingleOrg();";

        if (this.IsPostBack == false)
        {
            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetCertificateSignByOrgID"
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@OrgID", user.OrgID }
                                            });

            if (dt.Rows.Count > 0)
            {
                CertificateSignVM VM = new CertificateSignVM();
                EntityS.FillModel(VM, dt);
                tbP.Text = VM.PhysicianSignature;
                tbE.Text = VM.EnglishFullTitle;
                tbC.Text = VM.ChineseFullTitle;
                tbD.Text = VM.UnitDirector;
                tbS.Text = VM.UnitStamp;

                UC_OpenSelectSingleOrg.DefaultName = VM.OrgName;
                UC_OpenSelectSingleOrg.DefaultID = VM.OrgID;
                //tbLocation.Text = VM.OrgName;
                //hfLocationID.Value = VM.OrgID.ToString();
            }


        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //int OrgID = 0;
        //int.TryParse(PureString(hfLocationID.Value), out OrgID);

        int OrgID = UC_OpenSelectSingleOrg.GetID();

        if (OrgID == 0)
        {
            string myScript = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", myScript, false);
            return;
        }

        string PhysicianSignature = PureString(tbP.Text);
        string UnitDirector = PureString(tbD.Text);
        string UnitStamp = PureString(tbS.Text);
        string EnglishFullTitle = PureString(tbE.Text);
        string ChineseFullTitle = PureString(tbC.Text);

        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddOrUpdateCertificateSign"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@OrgID", OrgID },
                                                    { "@PhysicianSignature", PhysicianSignature },
                                                    { "@UnitDirector", UnitDirector },
                                                    { "@UnitStamp", UnitStamp },
                                                    { "@EnglishFullTitle", EnglishFullTitle },
                                                    { "@ChineseFullTitle", ChineseFullTitle },
                                                    { "@CreatedUserID", user.ID }
                                        });

        Chk = (int)OutDict["@Chk"];

        string script = "";
        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}