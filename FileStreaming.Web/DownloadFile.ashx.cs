using System;
using System.IO;
using System.Web;

namespace FileStreaming.Web
{
    public class DownloadFile : IHttpHandler
    {
        private const string FilesDirectory = @"\Files\";

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString == null || context.Request.QueryString["reportid"] == null)
            {
                return;
            }

            var reportId = int.Parse(context.Request.QueryString["reportid"]);

            var filename = ConvertReportIdToFileName(reportId);

            var fullPath = Path.Combine(HttpContext.Current.Server.MapPath(FilesDirectory), filename);

            var contentType = GetContentType(filename);

            using (var reader = new StreamReader(fullPath))
            {
                var result = reader.ReadToEnd();
                context.Response.ContentType = contentType;
                context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename));
                context.Response.Write(result);
            }
        }

        private string ConvertReportIdToFileName(int reportId)
        {
            switch (reportId)
            {
                case 1:
                    return @"report1.xlsx";
                case 2:
                    return @"report2.docx";
                case 3:
                    return @"report3.pdf";
                default:
                    throw new ArgumentException("Unknown ReportId");
            }
        }

        private string GetContentType(string file)
        {
            var fileExtension = file.Substring(file.Length - 4, 4).Replace(".", "");

            switch (fileExtension)
            {
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                case "pdf":
                    return "application/pdf";

                case "json":
                    return "text/json";

                case "xml":
                    return "text/xml";

                default:
                    return "unknown";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}