using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class CheckFieldVM
{    public int ID { get; set; }
    public string ApplyID { get; set; }  
    public string FieldName { get; set; } 
    public string FieldDiscription { get; set; } 
    public string FileCheck { get; set; } 
    public string CheckLevel { get; set; } 
    public string ViewBefore { get; set; } 
    public string ViewAfter { get; set; }
          public string ValBefore { get; set; }
        public string ValAfter { get; set; }
       public int FileID { get; set; }
       public CheckFieldVM()
       {
           ID = 0;
           ApplyID = "";
           FieldName = "";
           FieldDiscription = "";
           FileCheck = "";
          CheckLevel  = "";
          ViewBefore = "";
          ViewAfter= "";
          ValBefore = "";
          ValAfter = ""; 
           FileID = 0;

       }
}