using Newtonsoft.Json;
using System;

/// <summary>
/// UserProfileVM 的摘要描述
/// </summary>
public class UserModifyLogVM
{
    [JsonProperty(PropertyName = "S")]
    public int SID { get; set; }

    [JsonProperty(PropertyName = "CI")]
    public int CaseID { get; set; }

    [JsonIgnore]
    public int ModifyType { get; set; }
     

    [JsonProperty(PropertyName = "K")]
    public string ModifyTypeName
    { get { return Enum.GetName(typeof(UserModifyLogModifyType), ModifyType); } }



     [JsonProperty(PropertyName = "MI")]
    public int ModifyID { get; set; }


    [JsonProperty(PropertyName = "ModifyDate")]
     public string ModifyDate { get; set; }

    [JsonProperty(PropertyName = "D")]
    public string ModifyDateTaiwanYear {
        get {
            DateTime d = Convert.ToDateTime(ModifyDate);
            return  (d .Year -1911 ) .ToString ()+ "-" + d.ToString("MM-dd hh:mm:ss");
             
        
         }
         }

    [JsonProperty(PropertyName = "O")]
    public string ModifyOrg { get; set; }

    [JsonProperty(PropertyName = "U")]
    public string ModifyUserID { get; set; }

    [JsonProperty(PropertyName = "C")]
    public string ModifyColumn { get; set; }
 

    [JsonProperty(PropertyName = "B")]
    public string BeforeModify { get; set; }

    [JsonProperty(PropertyName = "A")]
    public string AfterModify { get; set; }
     

}


public enum UserModifyLogModifyType
{
    新增 = 1, 修改=2 ,刪除 =3 
}