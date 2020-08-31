using Newtonsoft.Json;
using System;

public class UserLoginVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonIgnore]
    public int SystemPowerCateID { get; set; }

    [JsonProperty(PropertyName = "SP")]
    public string SystemPowerCateName {
        get
        {
            return SystemPowerCate.GetName(SystemPowerCateID);
        }
    }

    [JsonProperty(PropertyName = "I")]
    public string LoginIP { get; set; }

    public DateTime LoginDate { get; set; }

    public DateTime LogoutDate { get; set; }

    [JsonProperty(PropertyName = "LID")]
    public DateTime? LoginDateNull {
        get
        {
            if(DateTime.Compare(LoginDate,DateTime.MinValue)==0)
            {
                return null;
            }
            else
            {
                return LoginDate;
            }
        }
    }

    [JsonProperty(PropertyName = "LOD")]
    public DateTime? LogoutDateNull {
        get
        {
            if (DateTime.Compare(LogoutDate, DateTime.MinValue) == 0)
            {
                return null;
            }
            else
            {
                return LogoutDate;
            }
        }
    }

    [JsonProperty(PropertyName = "NIU")]
    public bool NowInUse
    {
        get
        {
            if (SID==1 && LogoutDateNull == null && LoginDateNull.Value!=null && LoginDateNull.Value.Date == DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}