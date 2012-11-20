namespace Pipeline.Results
{

    public class Outcome
    {
        /// <summary>
        /// Return a custom result.
        /// </summary>
        /// <param name="pipelineResult"></param>
        public Outcome(PipelineResult pipelineResult)
        {
            PipelineResult = pipelineResult;
        }

        /// <summary>
        /// Return an error with no ETag. Do not use for 200 OK.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errorMessage"></param>
        public Outcome(int statusCode, string errorMessage)
            : this(new PipelineResult(statusCode, errorMessage))
        {
        }

        public PipelineResult PipelineResult { get; private set; }
    }
}