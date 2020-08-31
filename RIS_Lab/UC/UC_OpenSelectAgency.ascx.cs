using System;

public partial class UC_OpenSelectAgency : BaseUserControl
{
    public string PageUrl = "";
    public string EncryptPageUrl = "";
    public string Suffix = "";
    //public string addlv4AddOrgLevel4 = "N";
    public bool hasFilter = false;
    public int agencyState = 0;
    protected string defaultAgencyName;
    protected int defaultAgencyID;

    public string unOpenSelectAgency
    {
        get
        {
            if (Suffix.Equals(""))
            {
                return "unOpenSelectAgency";
            }
            else
            {
                return string.Format("{0}_{1}", "unOpenSelectAgency", Suffix);
            }
        }
    }

    public string ucAgencyID
    {
        get
        {
            if(Suffix.Equals(""))
            {
                return "ucAgencyID";
            }
            else
            {
                return string.Format("{0}_{1}", "ucAgencyID", Suffix);
            }
        }
    }
    public string ucAgencyName
    {
        get
        {
            if (Suffix.Equals(""))
            {
                return "ucAgencyName";
            }
            else
            {
                return string.Format("{0}_{1}", "ucAgencyName", Suffix);
            }
        }
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        EncryptPageUrl = QueryStringEncryptToolS.Encrypt(PageUrl);
    }
    public void SetName(string name)
    {
        if (!"".Equals(name) && name != null) this.defaultAgencyName = name;
    }
    public void SetID(int id)
    {
        if (id != 0) this.defaultAgencyID = id;
    }
    public int GetID()
    {
        return base.GetNumber<int>(ucAgencyID);
    }

    public string GetName()
    {
        return base.GetString(ucAgencyName);
    }


    //protected bool CheckPower(string pageUrl, MyPowerEnum myPowerEnum)
    //{
    //    UserVM user = AuthServer.GetLoginUser();
    //    bool HasPower = false;
    //    if (user != null)
    //    {
    //        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xCheckPower", sc))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@UserID", user.ID);
    //                cmd.Parameters.AddWithValue("@PageUrl", pageUrl);
    //                cmd.Parameters.AddWithValue("@FunctionIndex", myPowerEnum);
    //                cmd.Parameters.AddWithValue("@ModuleCateID", WebConfigurationManager.AppSettings["ModuleCateID"]);
    //                SqlParameter sp = cmd.Parameters.AddWithValue("@HasPower", HasPower);
    //                sp.Direction = ParameterDirection.Output;

    //                sc.Open();
    //                cmd.ExecuteNonQuery();

    //                HasPower = (bool)sp.Value;

    //            }
    //        }
    //    }
    //    //HasPowerDict[myPowerEnum] = HasPower;
    //    return HasPower;
    //}
}