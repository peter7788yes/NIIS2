using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_CaseUserRemark_Add : BasePage
{
    public new int ID = 0;
    //public UserVM user { get; set; }

    public Vaccination_RecordM_CaseUserRemark_Add()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);


        if (Request.HttpMethod.Equals("POST"))
        {
            ID = GetNumber<int>("c");
            if (ID == 0)
            {
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            if (this.IsPostBack == false)
            {
                ddlCate.Items.Add(new ListItem("個案姓名、生日、戶籍地址等備註：", "1"));
                ddlCate.Items.Add(new ListItem("聯絡人資料備註：", "2"));
                ddlCate.Items.Add(new ListItem("身分備註：(上傳附件)", "3"));
                ddlCate.Items.Add(new ListItem("父母新資料備註：", "4"));
                ddlCate.Items.Add(new ListItem("死亡資料備註：", "5"));
                ddlCate.Items.Add(new ListItem("其他：", "6"));
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
      
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ID == 0)
        {
            string sc = "<style>body{display:none;}</style><script>alert('資料取得失敗');window.close();</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        int Chk = 0;
        string script = "";

        var user = AuthServer.GetLoginUser();
        string RemarkType = PureString(ddlCate.SelectedItem.Value);
        string CaseRemark = PureString(tbRemark.Text);

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddBCGRecord"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                     { "@CaseUserID", ID },
                                                     { "@CreatedUserID", user.ID },
                                                     { "@RemarkType", RemarkType },
                                                     { "@CaseRemark", CaseRemark }
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            string message = string.Format("{0} {1} {2} (登錄者: {3} - {4})", ddlCate.SelectedItem.Text, DateTime.Now.ToShortTaiwanDate("/"), tbRemark.Text.Trim(), user.UserName, user.RoleName);
            script = "<style>body{display:none;}</style><script>alert('儲存成功');window.opener.changeCaseUserRemark('" + message + "');window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}