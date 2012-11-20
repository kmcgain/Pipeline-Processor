

namespace Pipeline.Results
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using global::Pipeline.XmlTools;
    using global::Pipeline.Model.Extensions;

    public class PipelineResult : ActionResult
    {
        public PipelineResult(int statusCode, Xml xml)
        {
            StatusCode = statusCode;
            Xml = xml;
        }

        public PipelineResult(int statusCode, string errorMessage)
            : this(statusCode, new Xml(XDocument.Parse("<error>{0}</error>".WithParams(errorMessage))))
        {
        }

        protected PipelineResult()
        {
        }

        public int StatusCode { get; set; }
        public Xml Xml { get; set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.Cache.SetNoStore();
            response.Cache.SetNoServerCaching();
            response.Cache.SetCacheability(HttpCacheability.Private);
            response.Cache.SetExpires(new DateTime(1970, 01, 01));
            response.ContentType = "text/xml";
            response.StatusCode = StatusCode;

            response.Write(Xml);
            response.TrySkipIisCustomErrors = true;
        }
    }
}