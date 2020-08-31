using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class ElementaryRecordVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "I")]
    public int ElementaryRecordID { get; set; }

    
    [JsonProperty(PropertyName = "SID")]
    public int ElementarySchoolID { get; set; }

    [JsonIgnore]
    public string SchoolCode { get; set; }

    [JsonIgnore]
    public string SchoolName { get; set; }

    [JsonProperty(PropertyName = "SN")]
    public string ElementarySchoolName
    {
        get
        {
           return SystemElementarySchool.GetName(ElementarySchoolID);
        }
    }
    
    [JsonProperty(PropertyName = "AY")]
    public int AdmissionYear { get; set; }

    [JsonProperty(PropertyName = "N")]
    public int StudentNumber { get; set; }

    [JsonIgnore]
    public int StudentYear { get; set; }

    [JsonProperty(PropertyName = "SY")]
    public string StudentNumberString {
        get
        {
            string rtn = "";

            switch(StudentYear)
            {
                case 1:
                    rtn = "新生";
                    break;
                case 2:
                    rtn = "二年級";
                    break;

            }
            return rtn;
        }
    }

    [JsonProperty(PropertyName = "HC")]
    public int HasYellowCardNumber { get; set; }

    [JsonProperty(PropertyName = "UN")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "SD")]
    public DateTime SignDate { get; set; }




}