using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class LiveStorage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string encryptedJsonString = Request["o"] ?? "";
            string decryptedJsonString = QueryStringEncryptToolS.Decrypt(encryptedJsonString);
            DownloadVM VM = JsonConvert.DeserializeObject<DownloadVM>(decryptedJsonString);
            string DownloadValidPeriodSecond = WebConfigurationManager.AppSettings["DownloadValidPeriodSecond"];
            int second = 0;
            int.TryParse(DownloadValidPeriodSecond, out second);

            DateTime maxDate=DateTime.Now.Add(TimeSpan.FromSeconds(second));
            //10秒緩衝
            DateTime minDate = DateTime.Now.Add(TimeSpan.FromSeconds(-10));
            if (VM != null && VM.date != null && DateTime.Compare(VM.date, minDate) >= 0 && DateTime.Compare(VM.date, maxDate) <= 0)
            {
                if (VM.ID > 0)
                {
                    DataTable dt = new DataTable();
                    using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.usp_FileM_xGetFileInfoByID", sc))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", VM.ID);
                            cmd.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                sc.Open();
                                da.Fill(dt);
                            }
                        }
                    }

                    FileInfoVM file = new FileInfoVM();
                    EntityS.FillModel(file, dt);

                    //string folderName = "";
                    //switch (file.FileCateID)
                    //{
                    //    case 1:
                    //        folderName = "DocumentM";
                    //        break;
                    //}
                    string filePath = Path.Combine(WebConfigurationManager.AppSettings["AttachmentPath"], file.FolderName, file.CreatedDate.Year.ToString(), file.CreatedDate.Month.ToString(), file.CreatedDate.Day.ToString(), file.StorageFileName);

					if(File.Exists(filePath))
					{
						Response.ContentType = "application/download";
						Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(file.DisplayFileName));
						Response.TransmitFile(filePath);
						Response.End();
					}
					else
					{
                        throw new HttpException(404, "Not found");
                    }
                }
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }
        catch
        {
            throw new HttpException(404, "Not found");
        }


    }
}