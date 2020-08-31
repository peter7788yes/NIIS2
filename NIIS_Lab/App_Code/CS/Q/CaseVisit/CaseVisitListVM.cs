using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CaseVisitListVM
{

    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "VID")]
    public int VisitID { get; set; }

     [JsonProperty(PropertyName = "C")]
    public int CaseID { get; set; } 
     

    [JsonProperty(PropertyName = "VD")]
    public string VisitDate { get; set; }

    [JsonProperty(PropertyName = "VA")]
    public string VisitAccount { get; set; }

    [JsonProperty(PropertyName = "VAN")]
    public string VisitAccountName { get; set; }

    [JsonProperty(PropertyName = "VT")]
    public string VisitType { get; set; }

    [JsonProperty(PropertyName = "VTN")]
    public string VisitTypeName { get { return (SystemCode.GetName("CaseVisit_VisitType", Convert.ToInt32(VisitType)) ?? ""); } }


    [JsonProperty(PropertyName = "VR")]
    public string VisitReason { get; set; }
    [JsonProperty(PropertyName = "VRN")]
    public string VisitReasonName { get { return (SystemCode.GetName("CaseVisit_VisitReason", Convert.ToInt32(VisitReason))?? "") ; } }

    [JsonProperty(PropertyName = "VRD")]
    public string VisitReasonDetail { get; set; }




    [JsonProperty(PropertyName = "R")]
    public string VisitResult { get; set; }

    [JsonProperty(PropertyName = "RN")]
    public string VisitResultName { get { return (SystemCode.GetName("CaseVisit_VisitResult_Reason_" + VisitReason, Convert.ToInt32(VisitResult)) ?? ""); } }


    [JsonProperty(PropertyName = "VC")]
    public string VacCode { get; set; }
     
     
  
}