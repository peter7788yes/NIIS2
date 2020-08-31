using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_CertificateM_PrintCertificateOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int pgNow;
        int pgSize;
        int AddrKind;
        string BirthDateS;
        string BirthDateE;

        string CaseName;
        string CaseIdNo;
        string HouseNo;
        string ContactName;
        string ContactIdNo;
        string ContactBirthDate;

        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? "";
        ContactBirthDate = Request.Form["ContactBirthDate"] ?? "";
        if (BirthDateS != "") BirthDateS = (Convert.ToInt32(BirthDateS.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateS.Substring(3, 2) + "/" + BirthDateS.Substring(5, 2);
        if (BirthDateE != "") BirthDateE = (Convert.ToInt32(BirthDateE.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateE.Substring(3, 2) + "/" + BirthDateE.Substring(5, 2);

        if (ContactBirthDate != "") ContactBirthDate = (Convert.ToInt32(ContactBirthDate.Substring(0, 3)) + 1911).ToString() + "/" + ContactBirthDate.Substring(3, 2) + "/" + ContactBirthDate.Substring(5, 2);


        CaseName = Request.Form["CaseName"] ?? "";
        CaseIdNo = Request.Form["CaseIdNo"] ?? "";
        HouseNo = Request.Form["HouseNo"] ?? "";
        ContactName = Request.Form["ContactName"] ?? "";
        ContactIdNo = Request.Form["ContactIdNo"] ?? "";

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["AddrKind"], out AddrKind);




        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetUserList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                cmd.Parameters.AddWithValue("@BirthDateS", BirthDateS);
                cmd.Parameters.AddWithValue("@BirthDateE", BirthDateE);
                cmd.Parameters.AddWithValue("@CaseName", CaseName);
                cmd.Parameters.AddWithValue("@CaseIdNo", CaseIdNo);
                cmd.Parameters.AddWithValue("@HouseNo", HouseNo);
                cmd.Parameters.AddWithValue("@AddrKind", AddrKind);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@ContactIdNo", ContactIdNo);
                cmd.Parameters.AddWithValue("@ContactBirthDate", ContactBirthDate);


                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<UserProfileListVM> list = new List<UserProfileListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}