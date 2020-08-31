<%@ WebHandler Language="C#" Class="CheckCodeOP" %>

using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.SessionState;

public class CheckCodeOP : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        AllowHttpMethod(context.Request.HttpMethod,"GET");

        string chkCode = string.Empty;
        Color[] color ={ Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange,Color.Brown, Color.DarkBlue };
        string[] font ={ "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh","PMingLiU", "Impact" };
        char[] character ={'1', '2', '3', '4', '5', '6', '8', '9','0', 'A', 'B', 'C', 'D', 'E',
'F', 'G', 'H','I', 'J', 'K', 'L', 'M', 'N','O', 'P','Q', 'R', 'S', 'T','U','V', 'W', 'X', 'Y','Z','a','b','c','d','e','f','g',
'h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};

        Random rnd = new Random();

        for (int i = 0; i < 4; i++)
        {
            chkCode += character[rnd.Next(character.Length)];
        }

        context.Session["CheckCode"] = chkCode.ToLower();

        //圖片寬高
        Bitmap bmp = new Bitmap(107, 29);
        Graphics g = Graphics.FromImage(bmp);

        g.Clear(Color.White);


        for (int i = 0; i < 5; i++)
        {
            int x1 = rnd.Next(100);
            int y1 = rnd.Next(25);
            int x2 = rnd.Next(100);
            int y2 = rnd.Next(25);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawLine(new Pen(clr), x1, y1, x2, y2);
        }

        for (int i = 0; i < chkCode.Length; i++)
        {
            //隨機選一種字體和顏色
            string fnt = font[rnd.Next(font.Length)];
            Font ft = new Font(fnt, 18, FontStyle.Bold);
            Color clr = color[rnd.Next(color.Length)];

            //從y=3的地方開始繪圖
            //第一個字距離左邊10px，第2個距離30px，第3個距離50px，第4個距離70px
            //等於字和字之間有20px的空間
            //每個字的中心為20,40,60,80
            g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 +10, (float)3);
        }

        //隨機填充100個點
        for (int i = 0; i < 100; i++)
        {
            int x = rnd.Next(bmp.Width);
            int y = rnd.Next(bmp.Height);
            Color clr = color[rnd.Next(color.Length)];
            bmp.SetPixel(x, y, clr);
        }

        context.Response.Buffer = true;
        context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
        context.Response.Expires = 0;
        context.Response.CacheControl = "no-cache";
        context.Response.AppendHeader("Pragma", "No-Cache");

        MemoryStream ms = new MemoryStream();
        try
        {
            bmp.Save(ms, ImageFormat.Png);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Png";
            context.Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            bmp.Dispose();
            g.Dispose();
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

    protected void AllowHttpMethod(string myMethod,params string[] methods)
    {
        bool HasPower = false;

        System.Collections.Generic.List<string> list = new  System.Collections.Generic.List<string>(methods);

        for (int i = 0; i <= methods.Length - 1; i++)
        {
            if (methods[i].Trim().ToUpper().Equals(myMethod))
            {
                HasPower = true;
                break;
            }
        }


        if (HasPower == false)
        {
            throw new HttpException(404, "Not found");
            //Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }
}
