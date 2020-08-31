using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_RecordM_RegisterData_Detail_FluNotes : BasePage
{
    public new int ID = 0;
    public int FluNotesID = 0;
    public UserVM user { get; set; }
    new bool IsValid = true;

    public Vaccination_RecordM_RegisterData_Detail_FluNotes()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        user = AuthServer.GetLoginUser();

        if (Request.HttpMethod.Equals("POST"))
        {

            ID = GetNumber<int>("c");
            FluNotesID = GetNumber<int>("i");

            if (ID == 0 && FluNotesID == 0 )
            {
                IsValid = false;
                string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


            if (this.IsPostBack == false)
            {
                if (FluNotesID == 0)
                {
                    tbDate.Text = DateTime.Now.ToShortTaiwanDate();
                }
                else
                {
                    DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetFluNotesByID"
                                        , new Dictionary<string, object>()
                                        {
                                                    { "@FluNotesID", FluNotesID }
                                       });

                    RegisterFluNotesVM VM = new RegisterFluNotesVM();
                    EntityS.FillModel(VM, dt);

                    tbDate.Text = VM.InoculationDate.ToShortTaiwanDate();
                }
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
        int StoolCard = 0;
        string ScreeningLocation = PureString(tbLocation.Text);

        string script = "";
      
        RadioButton selected = form1.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);

        if (selected != null)
        {
            switch(selected.ID)
            {
                case "rb1":
                    StoolCard = 1;
                    break;
                case "rb2":
                    StoolCard = 2;
                    break;
                case "rb3":
                    StoolCard = 3;
                    break;
            }
        }

        DateTime CheckDate = default(DateTime);
        DateTime.TryParseExact(PureString(tbDate.Text).RepublicToAD(),
                              "yyyyMMdd",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out CheckDate);



        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xUpdateStoolCard"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@FluNotesID", FluNotesID },
                                                    { "@CheckDate", CheckDate },
                                                    { "@StoolCard", StoolCard },
                                                    { "@ScreeningLocation", ScreeningLocation}
                                        });

        Chk = (int)OutDict["@Chk"];

        if (Chk > 0)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["I"] = FluNotesID;
            dict["CD"] = CheckDate;
            dict["SC"] = SystemCode.GetName("RecordM_RegisterData_Detail_StoolCard", StoolCard);
           
            script = "<style>body{display:none;}</style><script>alert('儲存成功');window.opener.changeStoolCard(" + JsonConvert.SerializeObject(dict) +");window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}