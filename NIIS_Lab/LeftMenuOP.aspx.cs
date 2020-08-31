using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;

public partial class LeftMenuOP : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        string Rtn = "";
        UserVM user = AuthServer.GetLoginUser();

        if (user == null)
        {
            return;
        }

        if (user.MenuJson != null && user.MenuJson.Trim().Length==0 == false)
        {
            Rtn = user.MenuJson;
        }
        else
        {
            Rtn = user.MenuJson;
            user.MenuJson = Rtn;
            AuthServer.SetLoginUser(user);
        }

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Rtn);
        Response.End();

        //string json = JsonConvert.SerializeObject(rtn);
        //byte[] bytes = Encoding.UTF8.GetBytes(json);
        //string base64 = Convert.ToBase64String(bytes);
        
        //Response.ContentType = "text/plain; charset=utf-8";
        //Response.Write(base64);
        //GZippedJsonT gzip = new GZippedJsonT();
        //Response.BinaryWrite(gzip.Compress(JsonConvert.SerializeObject(rtn)));

        //MemoryStream ms = new MemoryStream();
        //using (BsonWriter writer = new BsonWriter(ms))
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.Serialize(writer, new  { x="x", y="y", z="z" });
        //}
        ////Response.Write(ms.ToArray());
        //string data = Convert.ToBase64String(ms.ToArray());

        //byte[] data2 = Convert.FromBase64String(data);

        //MemoryStream ms2 = new MemoryStream(data2);
        //object xx;
        //using (BsonReader reader = new BsonReader(ms2))
        //{
        //    JsonSerializer serializer = new JsonSerializer();

        //    xx = serializer.Deserialize(reader);

        //}
        //Response.Write(data);
        //Response.End();

    }

    private string GetMenu(UserVM user)
    {
        ModuleMenuVM lastNode = null;
        List<ModuleMenuVM> list = new List<ModuleMenuVM>();
        List<ModuleMenuVM> Rtn = new List<ModuleMenuVM>();

        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xGetModuleMenu", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", user.ID);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }
       
        EntityS.FillModel(list, dt);
        foreach (ModuleMenuVM item in list)
        {
            if (item.PID == 0)
            {
                Rtn.Add(item);
                lastNode = item;
            }
            else
            {
                if (lastNode != null)
                {
                    if (lastNode.ID == item.PID)
                    {
                        lastNode.Children.Add(item);
                    }
                    else
                    {
                        GenMenuRecursive(item, lastNode);
                    }
                }
            }
        }
       
        return JsonConvert.SerializeObject(Rtn);
    }

    private void GenMenuRecursive(ModuleMenuVM nowNode, ModuleMenuVM innerNode)
    {
        ModuleMenuVM myParent = innerNode.Children.Find(x => nowNode.PID == x.ID);
        if (myParent != null)
        {
            myParent.Children.Add(nowNode);
        }
        else
        {
            foreach (ModuleMenuVM item in innerNode.Children)
            {
                GenMenuRecursive(nowNode, item);
            }
        }
    }
}