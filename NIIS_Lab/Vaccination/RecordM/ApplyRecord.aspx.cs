using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WayneEntity;
using Newtonsoft.Json;
using System.Globalization;
using System.Web;
using System.Linq;
using System.IO;

public partial class Vaccination_RecordM_ApplyRecord : BasePage
{
    public int UpdateUID = 0;
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string SystemRecordVaccineCode = "";
    public int SystemRecordVaccineID = 0;
    public string AppointmentDate = "";
    public string tbAry = "[]";
    public string Agency = "";
    public int AgencyID = 0;
    UserVM user;
    new bool IsValid = true;

    public Vaccination_RecordM_ApplyRecord()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {

            CaseUserID = GetNumber<int>("c");
            RecordDataID = GetNumber<int>("i");
            SystemRecordVaccineCode = GetString("r");
            SystemRecordVaccineID = GetNumber<int>("ri");
            AppointmentDate = GetString("a");
            AppointmentDate = AppointmentDate.Length == 0 ? GetString("aa") ?? "" : AppointmentDate;
            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            AppointmentDate = date.ToShortTaiwanDate();
            UpdateUID = GetNumber<int>("uu");

            user = AuthServer.GetLoginUser();

            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetDefaultBatchVaccineByOrgIDVaccineID"
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@OrgID", user.OrgID },
                                                    { "@VaccineID", SystemRecordVaccineCode.Split('-')[0]}
                                    });

            List<DefaultBatchVaccineVM> list = new List<DefaultBatchVaccineVM>();
            EntityS.FillModel(list, dt);

            if (list.Count > 0)
            {
                tbAry = JsonConvert.SerializeObject(list);
            }

            Agency = user.OrgName;
            AgencyID = user.OrgID;

            if (this.IsPostBack == false)
            {
                lblVC.Text = SystemRecordVaccineCode;
                lblAD.Text = AppointmentDate;
                //hfc.Value = CaseUserID.ToString();
                //hfi.Value = RecordDataID.ToString();
                //hfr.Value = SystemRecordVaccineCode;
                //hfa.Value = AppointmentDate;

                if (UpdateUID == 0)
                {
                    if (success == false || CaseUserID == 0 || RecordDataID == 0)
                    {
                        IsValid = false;
                        string script = "<style>body{display:none;}</style><script>alert('資料取得失敗');window.close();</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                        return;
                    }
                }

                tbDate.Text = DateTime.Now.ToShortTaiwanDate();


                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_ReSignReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_ReSignReason"];

                    ddlReason1.Items.Add(new ListItem("請選擇", ""));

                    foreach (var item in codes)
                    {
                        ddlReason1.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_ReInoculationReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_ReInoculationReason"];

                    ddlReason2.Items.Add(new ListItem("請選擇",""));

                    foreach (var item in codes)
                    {
                        ddlReason2.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_EarlyLateReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_EarlyLateReason"];

                    ddlReason3.Items.Add(new ListItem("請選擇", ""));

                    foreach (var item in codes)
                    {
                        ddlReason3.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

                if (UpdateUID > 0)
                {
                    dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetApplyRecordByID"
                                            , new Dictionary<string, object>()
                                            {
                                                  { "@ID", UpdateUID }
                                            });

                    ApplyRecordVM VM = new ApplyRecordVM();
                    EntityS.FillModel(VM, dt);

                    ddlReason1.SelectedValue = VM.ReSignReason.ToString();
                    ddlReason2.SelectedValue = VM.ReInoculationReason.ToString();
                    ddlReason3.SelectedValue = VM.EarlyLateReason.ToString();

                    var ary = VM.ReasonString.Split(',');
                    if (ary.Length > 0)
                        tbReason1.Text = ary[0];
                    if (ary.Length > 1)
                        tbReason2.Text = ary[1];
                    if (ary.Length > 0)
                        tbReason3.Text = ary[2];

                    cbSI.Checked = VM.SpecialInoculation;

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
        int OrgID = 0;
        int VaccineBatchID = 0;
        
        List<string> ReasonStringList = new List<string>();
        ReasonStringList.Add(PureString(tbReason1.Text));
        ReasonStringList.Add(PureString(tbReason2.Text));
        ReasonStringList.Add(PureString(tbReason3.Text));
        string ReasonString = string.Join(",", ReasonStringList);

        int ResignReason = 0;
        int ReinoculationReason = 0;
        int EarlyLateReason = 0;

        int.TryParse(ddlReason1.SelectedValue, out ResignReason);
        int.TryParse(ddlReason2.SelectedValue, out ReinoculationReason);
        int.TryParse(ddlReason3.SelectedValue, out EarlyLateReason);

        OrgID = GetNumber<int>("hfAgencyID");
        VaccineBatchID = GetNumber<int>("SelectVacc");
     
        //int.TryParse(PureString(hfi.Value) ?? "0", out RecordDataID);
        //int.TryParse(PureString(hfc.Value) ?? "0", out CaseUserID);

        DateTime InoculationDate = DateTime.Now;

        //DateTime.TryParse(Request.Form["AD"], out AssessmentDate);
        DateTime.TryParseExact((PureString(tbDate.Text) ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                               "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out InoculationDate);

        if(DateTime.Equals(InoculationDate, DateTime.MinValue) ==true)
            InoculationDate = DateTime.Now;

        int OutFileInfoID = 0;
        bool UploadFileSuccess = true;
        List<int> OutFileInfoID_List = new List<int>();

        if (tbFile.HasFile == true)
        {
            HttpFileCollection multipleFiles = Request.Files;
            for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
            {

                //string fileName = Path.GetFileName(uploadedFile.FileName);
                //if (uploadedFile.ContentLength > 0)
                //{
                //    uploadedFile.SaveAs(Server.MapPath("~/Files/") + fileName);
                //    Label1.Text += fileName + "Saved <BR>";
                //}

                HttpPostedFile uploadedFile = multipleFiles[fileCount];
                string extension = "";
                //string[] ary = tbFile.FileName.Split('.');
                string[] ary = uploadedFile.FileName.Split('.');
                if (ary.Length > 1)
                {
                    extension = ary.Last();
                }

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadedFile.ContentLength);
                }

                NIIS_WS.WebServiceSoapClient WS = new NIIS_WS.WebServiceSoapClient();
                string contentType = tbFile.PostedFile.ContentType;
                OutFileInfoID = WS.UploadFile(1, contentType, extension, uploadedFile.FileName, user.ID, user.OrgID, fileData);

                if (OutFileInfoID < 1)
                {
                    UploadFileSuccess = false;
                    break;
                }
                else
                {
                    OutFileInfoID_List.Add(OutFileInfoID);
                }
            }
        }

        string script = "";
        bool HasUpdate = false;
        int Chk = 0;

        if (UploadFileSuccess == true)
        {
            string OutFileInfoIDs = string.Join(",", OutFileInfoID_List.Select(x => x.ToString()).ToArray());

            Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@HasUpdate", HasUpdate }, { "@Chk", Chk } };

            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddOrUpdateApplyRecord"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                    { "@ApplyRecordID", UpdateUID },
                                                    { "@RecordDataID", RecordDataID },
                                                    { "@CaseUserID", CaseUserID },
                                                    { "@InoculationDate", InoculationDate },
                                                    { "@OrgID", OrgID },
                                                    { "@CreateType", 1 },
                                                    { "@VaccineBatchID", VaccineBatchID },
                                                    { "@CreatedUserID", user.ID },
                                                    { "@ResignReason", ResignReason },
                                                    { "@ReinoculationReason", ReinoculationReason},
                                                    { "@EarlyLateReason", EarlyLateReason },
                                                    { "@ReasonString", ReasonString },
                                                    { "@FileInfoIDs", OutFileInfoIDs },
                                                    { "@SpecialInoculation",cbSI.Checked},
                                                    { "@SystemRecordVaccineID",SystemRecordVaccineID}
                                            });

            HasUpdate = (bool)OutDict["@HasUpdate"];
            Chk = (int)OutDict["@Chk"];
        }

        if (UploadFileSuccess && Chk > 0)
        {
            string AddApplyRecord = " null ";
            if (HasUpdate == true)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("RID", RecordDataID);
                dict.Add("VB", VaccineBatchID);
                dict.Add("ID", InoculationDate);
                dict.Add("ON", user.OrgName);
                dict.Add("CD", DateTime.Now);
                AddApplyRecord = JsonConvert.SerializeObject(dict);
            }
            script = "<style>body{display:none;}</style><script>alert('儲存成功');window.opener.opener.AddApplyRecord(" + AddApplyRecord + ");window.close();</script>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}