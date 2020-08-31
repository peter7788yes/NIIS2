using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class System_MessageViewM_GetMessageViewOP : BasePage
{
    public System_MessageViewM_GetMessageViewOP()
    {
        base.AddPower("/System/ElectronBulletinM/MessageView/MessageView.aspx", MyPowerEnum.瀏覽);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int CheckID;

        int.TryParse(Request.Form["CheckID"], out CheckID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_NewsPublished_xGetNewsPublishedData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", CheckID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<NewsPublishedVM> list = new List<NewsPublishedVM>();
        List<CreateInfoVM> list1 = new List<CreateInfoVM>();
        List<ModifyInfoVM> list2 = new List<ModifyInfoVM>();
        List<DocumentFileVM> list3 = new List<DocumentFileVM>();
        HasFileDataVM rtn = new HasFileDataVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(list1, ds.Tables[1]);
        EntityS.FillModel(list2, ds.Tables[2]);
        EntityS.FillModel(list3, ds.Tables[3]);

        rtn.Datalist = list;
        rtn.CreateInfo = list1;
        rtn.ModifyInfo = list2;
        rtn.Filelist = list3;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}