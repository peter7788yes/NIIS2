using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;
using Newtonsoft.Json;
using System.Globalization;

public partial class Vaccination_RecordM_ApplyRecord_AddVaccine : BasePage
{
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";
    public string tbAry = "[]";
    public string Agency = "";
    public int AgencyID = 0;
    UserVM user;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (Request.HttpMethod.Equals("POST"))
        {
            if (this.IsPostBack == false)
            {
               
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
       
    }
}