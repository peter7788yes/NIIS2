using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;

namespace WayneTools
{
    public class CompressS
    {
        public static void CompressCssAndJavascript(HttpApplication app)
        {
            string contentType = app.Response.ContentType;

            if (contentType.Equals("text/html") ||
                contentType.Equals("text/css") ||
                contentType.Equals("application/x-javascript") ||
                contentType.Equals("text/javascript"))
            {
                app.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;

                string acceptEncoding = app.Request.Headers["Accept-Encoding"];

                if (acceptEncoding == null || acceptEncoding.Length == 0) return;

                acceptEncoding = acceptEncoding.ToLower();

                if (acceptEncoding.Contains("gzip"))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "gzip");
                }
                else if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "deflate");
                }
            }
        }

        public static void CompressPage(HttpApplication app)
        {
            string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            Stream prevUncompressedStream = app.Response.Filter;

            if (!(app.Context.CurrentHandler is Page || app.Context.CurrentHandler.GetType().Name == "SyncSessionlessHandler") || app.Request["HTTP_X_MICROSOFTAJAX"] != null)
            {
                return;
            }

            if (acceptEncoding == null || acceptEncoding.Length == 0) return;

            acceptEncoding = acceptEncoding.ToLower();

            if (acceptEncoding.Contains("gzip"))
            {
                app.Response.Filter = new GZipStream(prevUncompressedStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
            {
                app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}