using System;
using System.Collections.Generic;

public class UserVM
{
    //public string Name { get; set; }
    //public int Age { get; set; }
    //public double Height { get; set; }
    //public decimal Weight { get; set; }
    //public DateTime? DT { get; set; }
    //public bool? IsChecked { get; set; }


    public int ID { get; set; }
	public string  LoginName { get; set; }
	public string LoginPassword { get; set; }
	public string Title { get; set; }
	public bool Sex { get; set; }
	public DateTime CreatedDate { get; set; }
	public bool IsLock { get; set; }
    public int RoleID { get; set; }
    public string MenuJson { get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public DateTime LoginDate { get; set; }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string RocID { get; set; }


    public string OrgName { get; set; }
    public string OrgNameByOrgID { get { return SystemOrg.GetName(OrgID); } }
    public int OrgID { get; set; }
    public bool IsBusiness { get; set; }

    public int CheckState { get; set; }
    public string ApplyReason { get; set; }

    //public string DisplayFileName { get; set; }
    //public int FileInfoID { get; set; }

    public string DisplayFileNames { get; set; }
    public string FileInfoIDs { get; set; }
    public string CheckDescription { get; set; }
    public List<string> DisplayFileNamesList
    {
        get
        {
            if (DisplayFileNames == null)
                return new List<string>();
            return new List<string>(DisplayFileNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public List<string> FileInfoIDsList
    {
        get
        {

            if (FileInfoIDs == null)
                return new List<string>();
            return new List<string>(FileInfoIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}