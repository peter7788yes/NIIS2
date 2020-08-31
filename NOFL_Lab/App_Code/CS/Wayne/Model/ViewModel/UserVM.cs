using System;

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
    public int OrgID { get; set; }
}