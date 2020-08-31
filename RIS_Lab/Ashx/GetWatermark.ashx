<%@ WebHandler Language="C#" Class="GetWatermark" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;

public class GetWatermark : IHttpHandler, IRequiresSessionState
{
    /// <summary>
    /// 產生Watermark的圖片
    /// </summary>
    /// <param name="context"></param>
    /// <remarks>
    /// </remarks>
    public void ProcessRequest(HttpContext context)
    {
        const string DefaultFontName = @"微軟正黑體";
        //設定輸出為png檔
        context.Response.ContentType = "image/png";
        int fontSize = 30;
        int bmpWidth = 800;
        int bmpHeight = 400;
        string watermark = @"{0} {1}";
        //建立Bitmap
        Bitmap bmp = new Bitmap(bmpWidth, bmpHeight);
        string watermarks = string.Format(watermark,
                            DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                            AuthServer.GetLoginUser().UserName);
        //建立Graphics
        Graphics canvas = Graphics.FromImage(bmp);
        canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //設定透明的Brush
        SolidBrush watermarkBrush = new SolidBrush(Color.Gray);


        //設定底圖為透明
        SolidBrush whiteBrush = new SolidBrush(Color.Transparent);
        canvas.FillRectangle(whiteBrush, 0, 0, bmp.Width, bmp.Height);

        //因為要由下往上畫，所以將原點設定成下方
        canvas.TranslateTransform(50, bmpHeight - 200);
        //設定浮水印的字型及大小
        Font f = new Font(DefaultFontName, fontSize, FontStyle.Bold);
        //設定旋轉的角度為330
        //canvas.RotateTransform(330);
        //將浮水印畫上去
        canvas.DrawString(watermarks, f, watermarkBrush, fontSize, 0, StringFormat.GenericTypographic);
        //將圖檔輸出去
        bmp.Save(context.Response.OutputStream, ImageFormat.Png);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}