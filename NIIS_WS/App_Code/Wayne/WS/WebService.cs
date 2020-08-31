using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;

/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用設計的元件，請取消註解下列一行
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld() {
    //    return "Hello World";
    //}

    [WebMethod]
    public int UploadFile(int fileCateID,string contentType, string fileExtension, string displayFileName,int UserID,int OrgID, byte[] data,bool IsReport=false)
    {
        //string folderName="";
        //switch(fileCateID)
        //{
        //  case 1:
        //      folderName = "DocumentM";
        //      break;
		//	case 2:
		//		folderName = "InfoDataM";
		//		break;
		//	case 3:
		//		folderName = "QnADataM";
		//		break;
		//	case 4:
		//		folderName = "VaccineInformationM";
		//		break;
		//	case 5:
		//		folderName = "VaccineInM";
		//		break;
		//	case 6:
		//		folderName = "VaccineOutM";
		//		break;
		//	case 7:
		//		folderName = "VaccineReturnM";
		//		break;
        //}

		string FolderName="";
		using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("Select FolderName from dbo.F_FileCate where ID=@FileCateID", sc))
            {

                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileCateID", fileCateID);
              
                //SqlParameter sp = cmd.Parameters.AddWithValue("@FolderName", FolderName);
                //sp.Direction = ParameterDirection.Output;

                sc.Open();
				FolderName =cmd.ExecuteScalar().ToString();
                //cmd.ExecuteNonQuery();
                //FolderName = sp.Value.ToString();
            }
        }
		
        DateTime date = DateTime.Now;

        string fileFolder = Path.Combine(WebConfigurationManager.AppSettings["AttachmentPath"], FolderName, date.Year.ToString(), date.Month.ToString(), date.Day.ToString());
        Directory.CreateDirectory(fileFolder);
        

        int OutFileInfoID = 0;

        string StorageFileName = string.Format("{0}_{1}.{2}", Guid.NewGuid(),date.ToString("HHmmss") , fileExtension);

        using (FileStream fs = new FileStream(Path.Combine(fileFolder,StorageFileName), System.IO.FileMode.Create, System.IO.FileAccess.Write))
        {
            fs.Write(data, 0, data.Length);
        }
        string Conn = WebConfigurationManager.ConnectionStrings["ConnDB"].ToString();
        if(IsReport==true)
            Conn = WebConfigurationManager.ConnectionStrings["ConnReport"].ToString();
        using (SqlConnection sc = new SqlConnection(Conn))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_FileM_xAddFileInfo", sc))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileCateID", fileCateID);
                cmd.Parameters.AddWithValue("@FileStateID", 1);
                cmd.Parameters.AddWithValue("@MimeType", contentType);
                cmd.Parameters.AddWithValue("@StorageFileName", StorageFileName);
                cmd.Parameters.AddWithValue("@TotalBytes", data.Length);
                cmd.Parameters.AddWithValue("@DisplayFileName", displayFileName);
                cmd.Parameters.AddWithValue("@CreatedUserID", UserID);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@OutFileInfoID", OutFileInfoID);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                OutFileInfoID = (int)sp.Value;
            }
        }

        return OutFileInfoID;
    }
    
}
