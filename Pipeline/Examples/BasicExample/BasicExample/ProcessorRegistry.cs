using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline.Pipe;
using StructureMap.Configuration.DSL;

namespace BasicExample
{
    class ProcessorRegistry : Registry
    {
        public ProcessorRegistry()
        {
            Scan(
                _ =>
                {
                    _.AssemblyContainingType<ProcessorRegistry>();
                    _.AssemblyContainingType<PipelineRegistry>();
                    _.ConnectImplementationsToTypesClosing(typeof(IProcess<>));
                });
        }
    }
}
