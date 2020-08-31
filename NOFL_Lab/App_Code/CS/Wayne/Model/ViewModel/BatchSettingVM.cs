using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RegisterData 的摘要描述
/// </summary>
public class BatchSettingVM
{

    [JsonProperty(PropertyName = "DBVID")]
    public int DefaultBatchVaccineID { get; set; }

    [JsonProperty(PropertyName = "VBID")]
    public int VaccineBatchID { get; set; }


    [JsonProperty(PropertyName = "I")]
    public int VaccineDataID { get; set; }


    [JsonProperty(PropertyName = "BN")]
    public string BatchNumber { get; set; }

    [JsonIgnore]
    public int BatchCate { get; set; }

    [JsonProperty(PropertyName = "BC")]
    public string BatchCateString
    {
        get
        {
            return SystemCode.GetName("VaccineM_BatchType", BatchCate);
        }
    }

    [JsonIgnore]
    public int  PackageStyle { get; set; }

    [JsonProperty(PropertyName = "PS")]
    public string  PackageStyleString
    {
        get
        {
            return SystemCode.GetName("VaccineM_BatchFormDrug", PackageStyle);
        }
    }

    //[JsonProperty(PropertyName = "BV")]
    //public float BatchVolume { get; set; }

    [JsonProperty(PropertyName = "DP")]
    public int DosePer { get; set; }


    [JsonProperty(PropertyName = "SV")]
    public float StorageVolume
    {
        get
        {
            return StorageBottle * DosePer;
        }
    }

    [JsonProperty(PropertyName = "SB")]
    public float StorageBottle { get; set; }

    [JsonProperty(PropertyName = "VD")]
    public DateTime ValidDate { get; set; }


    [JsonIgnore]
    public bool IsDefault { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public string IsDefaultString
    {
        get
        {
            string rtn = "N";
            if(IsDefault==true)
            {
                rtn = "Y";
            }
            return rtn;
        }
    }

}