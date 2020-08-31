using System;

public partial class UC_UC_OpenSelectSingleOrg : BaseUserControl
{
    public string PageUrl = "";
    public string EncryptPageUrl = "";
    public string Suffix = "";
    public int DefaultID { get; set; }
    public string DefaultName { get; set; }
    public string unOpenSelectOrg
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

    public string ucOrgID
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
    public string ucOrgName
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

    public string callback = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        EncryptPageUrl = QueryStringEncryptToolS.Encrypt(PageUrl);
    }

    public int GetID()
    {
        return base.GetNumber<int>(ucOrgID);
    }

    public string GetName()
    {
        if (this.IsPostBack == false)
        {
            return DefaultName;
        }
        else
        {
            return base.GetString(ucOrgName);
        }
    }

}