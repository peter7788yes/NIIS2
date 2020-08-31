using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Vaccination_RecordM_YellowCard_Add :BasePage
{
    public new int ID = 0;
    public UserVM user { get; set; }
    new bool IsValid = true;

    public Vaccination_RecordM_YellowCard_Add()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        user = AuthServer.GetLoginUser();

        if (Request.HttpMethod.Equals("POST"))
        {

            ID = GetNumber<int>("c");

            if (ID == 0)
            {
                IsValid = false;
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            if (this.IsPostBack == false)
            {
                tbUser.Text = user.UserName;
                tbDate.Text = DateTime.Now.ToShortTaiwanDate();
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
        if (IsValid == false)
            return;
        int Chk = 0;
        string script = "";

        RadioButton selected = MyForm.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
        int ApplyType=0;

        if (selected != null)
        {
            ApplyType=selected.Text == "黃卡" ? 1 : 2;
         }

        //string TaiwanDate =tbDate.Text.Trim();
        //string[] ary = TaiwanDate.Split('/');
        //DateTime ApplyDate = new DateTime(int.Parse(ary[0])+1911,int.Parse(ary[1]),int.Parse(ary[2]));
        DateTime ApplyDate = default(DateTime);
        DateTime.TryParseExact(PureString(tbDate.Text).RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out ApplyDate);


        string ApplyLocation = PureString(tbLocation.Text);

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddApplyYellowCardRecord"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@CaseUserID", ID },
                                                    { "@ApplyDate", ApplyDate },
                                                    { "@ApplyLocation", ApplyLocation },
                                                    { "@CreatedUserID", user.ID},
                                                    { "@ApplyType", ApplyType }
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            string message = string.Format("{0} {1} (登錄者: {2} - {3})",tbDate.Text.Trim() , tbLocation.Text.Trim(), user.UserName, user.OrgName);
            script = "<style>body{display:none;}</style><script>alert('儲存成功');window.opener.changeYellowCardUpdateRecord('" + message + "');window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}