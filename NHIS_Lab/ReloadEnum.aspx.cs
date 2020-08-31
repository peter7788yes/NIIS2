using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ReloadEnum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //long Process_MemoryStart = 0;
        //long Process_MemoryEnd = 0;
        //System.Diagnostics.Process MyProcess = System.Diagnostics.Process.GetCurrentProcess();
        //Process_MemoryStart = MyProcess.PrivateMemorySize64;

        //Response.Write(Process_MemoryStart);
            //Console.WriteLine(Process_MemoryStart);

        //Console.WriteLine(GC.GetTotalMemory(true).ToString());

        SystemCode.Update();
        SystemRole.Update();
        SystemOrg.Update();
        SystemAreaCode.Update();
        SystemRecordVaccine.Update();
        SystemYCard.Update();
        SystemElementarySchool.Update();
        //Process_MemoryEnd = MyProcess.PrivateMemorySize64;
        //Response.Write(Process_MemoryEnd);
        //Console.WriteLine(Process_MemoryEnd);
        //HomeUrlVM vm =new HomeUrlVM();
        //vm.PageUrl = "/System/CodeM/CodeSetting.aspx";
        //vm.date = DateTime.Now;
        //string s = JsonConvert.SerializeObject(vm);
        //s=QueryStringEncryptToolS.Encrypt(s);
        //s = HttpUtility.UrlEncode(s);
        //Response.Write(s);
        //Response.End();

        //HomeUrlSecret secret = new HomeUrlSecret();
        //secret.RedirectUrl = "Home";

        //string s=JsonConvert.SerializeObject(secret);

        //UrlParameterEncryptT EncryptT = new UrlParameterEncryptT();
        //UrlParameterDecryptT DecryptT = new UrlParameterDecryptT();

        //s = EncryptT.GetOutSecretJsonWithEncrypt(secret);
        //string ss = s;
        //int cc = s.Length;
        //Response.Write(s);
        //Response.Write("<br/>");
        //Response.Write(s.Length);
        //s = HttpUtility.UrlEncode(s);
        //Response.Write("<br/>");
        //Response.Write(s);
        //Response.Write("<br/>");
        //Response.Write(s.Length);
        //Response.Write("<br/>");
        //string a = Request["o"]??"";
        //int b = 0;
        //int.TryParse(Request["c"] ?? "0", out b);
        //HomeUrlSecret secret2 = new HomeUrlSecret();
        //secret2 = DecryptT.GetUrlSecret<HomeUrlSecret>(ss, TimeSpan.FromDays(1), cc);
        //if(secret2.IsValid==true)
        //    Response.Write("2Checksum驗證成功");
        //else
        //    Response.Write("2Checksum驗證失敗");
        //Response.End();
    }


}