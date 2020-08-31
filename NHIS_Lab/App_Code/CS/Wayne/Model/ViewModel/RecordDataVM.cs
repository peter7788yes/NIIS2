using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class RecordDataVM
{
    [JsonIgnore]
    public DateTime BirthDate { get; set; }

    public int DeltaDays { get; set; }

    public RecordDataVM(DateTime BirthDate)
    {
        this.BirthDate = BirthDate;
    }

    /// <summary>
    /// SystemYCardVM
    /// </summary>
    /// 
    [JsonProperty(PropertyName = "YMID")]
    public int YCardMID { get; set; }

    [JsonProperty(PropertyName = "AE")]
    public string AgeEngilsh { get; set; }

    [JsonIgnore]
    public string DoseID { get; set; }

    [JsonProperty(PropertyName = "P")]
    public int Period { get; set; }



    /// <summary>
    /// RecordUserDataVM
    /// </summary>
    [JsonProperty(PropertyName = "RID")]
    public int RecordDataID { get; set; }

    [JsonProperty(PropertyName = "SRVID")]
    public int SystemRecordVaccineID { get; set; }

    [JsonProperty(PropertyName = "SRVC")]
    public string SystemRecordVaccineCode {
        get
        {
            if (DoseID!=null && DoseID.Equals("") == false)
            {
                return DoseID;
            }
            else
            {
                return SystemRecordVaccine.GetName(SystemRecordVaccineID);
            }
        }
    }

    [JsonIgnore]
    public DateTime? InoculationDate { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public DateTime? InoculationDateOut {
        get
        {
            if (InoculationDate != null && DateTime.Equals(InoculationDate,new DateTime(2099,1,1,1,1,1,0)))
            {
                return null;
            }
            else
            {
                return InoculationDate;
            }
        }
    }

    //[JsonProperty(PropertyName = "OID")]
    //public DateTime? OrderbyInoculationDate
    //{
    //    get
    //    {
    //        if (InoculationDate == null)
    //        {
    //            return DateTime.MaxValue;
    //        }
    //        else
    //        {
    //            return InoculationDate;
    //        }
    //    }
    //}

    [JsonProperty(PropertyName = "CD")]
    public DateTime? CreatedDate { get; set; }

    [JsonProperty(PropertyName = "OI")]
    public int OrgID { get; set; }

    [JsonIgnore]
    public int CreateType { get; set; }

    [JsonProperty(PropertyName = "CT")]
    public string CreateTypeString {
        get
        {
         
                return SystemCode.GetName("RecordM_RegisterData_CreateType", CreateType);
          
        }
    }

    [JsonIgnore]
    public int VaccineBatchID { get; set; }

    [JsonProperty(PropertyName = "VB")]
    public string VaccineBatchIDString {
        get
        {
            string rtn = "";
            if(VaccineBatchID>0)
            {
                rtn = VaccineBatchID.ToString();
            }
            return "";
        }
    }


    /// <summary>
    /// /////////////////////////////////////////
    /// </summary>

    [JsonProperty(PropertyName = "OAD")]
    public DateTime? OriginAppointmentDate
    {
        get
        {

            if (AppointmentDate != null && IsRule == true)
            {
                return BirthDate.AddDays(Period);
            }
            else
            {
                return null;
            }
        }
    }

    [JsonProperty(PropertyName = "AD")]
    public DateTime? AppointmentDate
    {
        get {

            if (IsRule==true)
            {
                return BirthDate.AddDays(Period).AddDays(DeltaDays);
            }
            else
            {
                return null;
            }
        }
    }

    [JsonProperty(PropertyName = "ON")]
    public string OrgName
    {
        get
        {
            return SystemOrg.GetName(OrgID);
        }
    }

    //[JsonProperty(PropertyName = "IE")]
    //public bool IsExpired { get; set; }

    //[JsonProperty(PropertyName = "IEY")]
    //public bool IsEarly { get; set; }

    //[JsonProperty(PropertyName = "IV")]
    //public bool IsValid { get; set; }

    ////[JsonProperty(PropertyName = "CC")]
    ////public int ColorType { get; set; }

    [JsonProperty(PropertyName = "CC")]
    public int ColorType
    {
        get
        {
            int color = 0;
            if (InoculationDateOut != null && OriginAppointmentDate != null && IsRule == true)
            {
                int flag = DateTime.Compare((DateTime)InoculationDateOut, (DateTime)OriginAppointmentDate);

                if (flag < 0)
                {
                    color = 2;
                }
                else
                {
                    flag = DateTime.Compare((DateTime)InoculationDateOut, ((DateTime)OriginAppointmentDate).AddDays(90));

                    if (flag > 0)
                        color = 1;
                }

            }

            return color;

        }
    }

    [JsonProperty(PropertyName = "IR")]
    public bool IsRule { get; set; }

}