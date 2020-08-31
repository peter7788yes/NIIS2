using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class UserInfoVM
{
    public UserInfoVM()
    {
        RoleIdList = new List<int>();
    }

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonProperty(PropertyName = "L")]
    public string LoginName { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string UserName { get; set; }

    //[JsonIgnore]
    //public int OrgID { get; set; }

    //[JsonProperty(PropertyName = "ON")]
    //public string OrgName {
    //    get
    //    {
    //        return SystemOrg.GetName(OrgID);
    //    }
    //}

    [JsonProperty(PropertyName = "ON")]
    public string OrgAgencyName { get; set; }

    //[JsonProperty(PropertyName = "IE")]
    //public bool IsEnabled { get; set; }

    [JsonProperty(PropertyName = "IE")]
    public int EnableState { get; set; }

    [JsonProperty(PropertyName = "C")]
    public int CheckState { get; set; }

    [JsonIgnore]
    public DateTime LastLoginDate { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime ApplyDate { get; set; }

    [JsonProperty(PropertyName = "N")]
    public string NoLoginTime
    {
        get
        {
            string rtn = "";

            if (LastLoginDate != default(DateTime))
            {
                TimeSpan ts = DateTime.Now.Subtract(LastLoginDate);
                int dd = ts.Days;
                int HH = ts.Hours;
                int mm = ts.Minutes;

                if (dd > 0)
                {
                    rtn = string.Format("{0}日{1}時{2}分", dd, HH, mm);
                }
                else if (HH > 0)
                {
                    rtn = string.Format("{0}時{1}分", HH, mm);
                }
                else
                {
                    rtn = string.Format("{0}分", mm);
                }
            }
            return rtn;

        }
    }


    [JsonIgnore]
    public List<int> RoleIdList { get; set; }

    [JsonProperty(PropertyName = "RN")]
    public List<string> RoleNameList
    {
        get
        {
            List<string> list = new List<string>();
            foreach (var item in RoleIdList)
            {
                list.Add(SystemRole.GetName(item));
            }
            return list;
        }
    }

    [JsonIgnore]
    public int SystemPowerString { get; set; }

    [JsonProperty(PropertyName = "SP")]
    public string SystemPowerStringName
    {

        get
        {
            List<string> rtnList = new List<string>();
            string BinaryStr = Convert.ToString(SystemPowerString, 2);
            var ary = BinaryStr.ToCharArray();
            Array.Reverse(ary);


            int i = 1;
            foreach (var item in ary)
            {
                if (item.Equals('1'))
                {
                    string name = SystemPowerCate.GetName(i);
                    if (name.Length > 0)
                        rtnList.Add(name);
                }
                i++;
            }

            return string.Join(",", rtnList);

        }
    }

}