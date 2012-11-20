namespace Pipeline.Pipe
{
    using StructureMap.Configuration.DSL;

    public class PipelineRegistry : Registry
    {
        public PipelineRegistry()
        {
            Scan(
                _ =>
                    {
                        _.AssemblyContainingType<PipelineRegistry>();
                        _.ConnectImplementationsToTypesClosing(typeof (IProcess<>));
                    });

            For<IPipelineProcessor>().Use<PipelineProcessor>();
        }
    }
}