using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pipeline.Results
{
    public class HtmlOutcome : Outcome
    {
        public HtmlOutcome(int statusCode, string errorMessage)
            : base(new HtmlResult(statusCode, errorMessage))
        {
        }
    }
}