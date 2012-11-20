using System;
using System.Web;
using System.Web.Routing;

namespace Pipeline.Results
{
    using System.Web.Mvc;

    public class HtmlResult : PipelineResult
    {
        public HtmlResult(int statusCode, string html) : base(statusCode, null)
        {
            Html = html;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            if (StatusCode == 404 && HttpContext.Current != null)
            {
                var requestUrl = (context.HttpContext.Request != null && context.HttpContext.Request.Url != null)
                                     ? HttpUtility.UrlEncode(context.HttpContext.Request.Url.AbsolutePath)
                                     : null;
                HttpContext.Current.Server.TransferRequest("~/Home/Error404" + (requestUrl != null ? "?resourceUrl=" + requestUrl : ""));
            }
            if (StatusCode == 401 && HttpContext.Current != null)
            {
                HttpContext.Current.Server.TransferRequest("~/Home/Error401");
            }

            response.ContentType = "text/html";
            response.StatusCode = StatusCode;

            response.Write(Html);
        }

        public string Html { get; set; }

    }
}