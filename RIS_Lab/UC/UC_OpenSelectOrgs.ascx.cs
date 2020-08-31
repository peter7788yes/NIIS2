using System;

public partial class UC_UC_OpenSelectOrgs : BaseUserControl
{
    public string PageUrl = "";
    public string EncryptPageUrl = "";
    public string Suffix = "";

    public string unOpenSelectOrgs
    {
        get
        {
            if (Suffix.Length == 0)
            {
                return "unOpenSelectOrg";
            }
            else
            {
                return string.Format("{0}_{1}", "unOpenSelectOrg", Suffix);
            }
        }
    }

    public string ucOrgsID
    {
        get
        {
            if (Suffix.Length == 0)
            {
                return "ucOrgID";
            }
            else
            {
                return string.Format("{0}_{1}", "ucOrgID", Suffix);
            }
        }
    }
    public string ucOrgsName
    {
        get
        {
            if (Suffix.Length == 0)
            {
                return "ucOrgName";
            }
            else
            {
                return string.Format("{0}_{1}", "ucOrgName", Suffix);
            }
        }
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        EncryptPageUrl = QueryStringEncryptToolS.Encrypt(PageUrl);
    }

    public int GetID()
    {
        return base.GetNumber<int>(ucOrgsID);
    }

    public string GetName()
    {
        return base.GetString(ucOrgsName);
    }

}