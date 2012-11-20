
using Pipeline.XmlTools;
namespace Pipeline.Results
{
    public class SuccessResult : PipelineResult
    {
        public SuccessResult(Xml xml) : base(200, xml)
        {
        }
    }
}