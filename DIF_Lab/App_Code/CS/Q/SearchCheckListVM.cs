using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class SearchCheckListVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

     [JsonProperty(PropertyName = "RoleName")]
    public string RoleName { get; set; }


    [JsonProperty(PropertyName = "UserName")]
     public string UserName { get; set; }

    [JsonProperty(PropertyName = "LoginName")]
    public string LoginName { get; set; }

    [JsonProperty(PropertyName = "SumCount")]
    public string SumCount { get; set; }

    [JsonProperty(PropertyName = "SearchDate")]
    public string SearchDate { get; set; }

    [JsonProperty(PropertyName = "SearchConditions")]
    public string SearchConditions { get; set; }

    [JsonProperty(PropertyName = "SearchReason")]
    public string SearchReason { get; set; }

    [JsonProperty(PropertyName = "SearchKindName")]
    public string SearchKindName { get; set; }

      [JsonProperty(PropertyName = "YearMonth")]
    public string YearMonth { get; set; }

      [JsonProperty(PropertyName = "SearchKind")]
      public string SearchKind { get; set; }
      [JsonProperty(PropertyName = "UserID")]
      public string UserID { get; set; }

      [JsonProperty(PropertyName = "AuditID")]
      public int AuditID { get; set; }

      [JsonProperty(PropertyName = "SearchResult")]
      public string SearchResult { get; set; }
      [JsonProperty(PropertyName = "AuditResult")]
      public int AuditResult { get; set; }

      [JsonProperty(PropertyName = "AuditResultName")]
      public string AuditResultName { get {
          if (AuditResult == 0) return "-";
          else if (AuditResult == 1) return "正常";
          else if (AuditResult == 2) return "異常";
          else return "-";
      
      } }


}