using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class AccountCheckVM
{
    public AccountCheckVM()
    {
        RoleIdList = new List<int>();
    }
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ID { get; set; }

    [JsonIgnore]
    public int CheckOrgID { get; set; }

    [JsonProperty(PropertyName = "CON")]
    public string CheckOrgName
    {
        get
        {
            return SystemOrg.GetName(CheckOrgID);
        }
    }

    [JsonIgnore]
    public int CheckYear { get; set; }

    [JsonIgnore]
    public int YearSeason { get; set; }


    [JsonProperty(PropertyName = "CY")]
    public string CheckYearString
    {
        get
        {
            string season = "";
            switch(YearSeason)
            {
                case 1:
                    season = "第一季";
                    break;
                case 2:
                    season = "第二季";
                    break;
                case 3:
                    season = "第三季";
                    break;
                case 4:
                    season = "第四季";
                    break;
                case 5:
                    season = "上半年";
                    break;
                case 6:
                    season = "下半年";
                    break;
            }

            return string.Format("{0} {1}", CheckYear.ToString(), season);
        }
    }


    [JsonProperty(PropertyName = "CN")]
    public int CheckNumber { get; set; }

    [JsonProperty(PropertyName = "BC")]
    public int BeContinue { get; set; }

    [JsonProperty(PropertyName = "BB")]
    public int BeBreak { get; set; }

    [JsonProperty(PropertyName = "BUC")]
    public int BeUnconfirmed { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "AD")]
    public DateTime ApplyDate { get; set; }

    [JsonIgnore]
    public int CheckProgress { get; set; }

    [JsonProperty(PropertyName = "CP")]
    public string CheckProgressString {
        get
        {
            return SystemCode.GetName("AccountM_AccountCheck2_CheckResult", CheckProgress);

        }
    }

    [JsonProperty(PropertyName = "IBC")]
    public int IsBeContinue { get; set; }

    [JsonProperty(PropertyName = "INC")]
    public string IsNotContinueReason { get; set; }

    [JsonIgnore]
    public int OrgID { get; set; }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    [JsonIgnore]
    public int EnableState { get; set; }

    [JsonProperty(PropertyName = "ES")]
    public string EnableStateString
    {
        get
        {
            return SystemCode.GetName("AccountM_EnableState", EnableState);
        }
    }

    [JsonProperty(PropertyName = "LN")]
    public string LoginName { get; set; }

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
}
