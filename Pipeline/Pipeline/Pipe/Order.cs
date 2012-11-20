namespace Pipeline.Pipe
{
    using System;
    using System.Collections.Generic;

    public static class Order
    {
        public static readonly IList<Type> RunOrder =
            new List<Type>
                {
                    typeof (Initialise),
                    typeof (ServicePrecondition),
                    typeof (ReadMeta),
                    typeof (ReadEntity),
                    typeof (StateTransition),
                    typeof (EntityPrecondition),
                    typeof (XmlTransform),
                    typeof (PostXmlTransform),
                    typeof (WriteNewVersion),
                    typeof (UpdateMeta),
                    typeof(TriggerEvents),
                    typeof (WriteEvents),
                    typeof (SendResult),
                };


        public interface Initialise : IPipelineStep
        {
        }

        public interface ServicePrecondition : IPipelineStep
        {
        }

        public interface ReadMeta : IPipelineStep
        {
        }

        public interface ReadEntity : IPipelineStep
        {
        }

        public interface StateTransition : IPipelineStep
        {
        }

        public interface EntityPrecondition : IPipelineStep
        {
        }

        public interface XmlTransform : IPipelineStep
        {
        }

        public interface PostXmlTransform : IPipelineStep
        {
        }

        public interface WriteNewVersion : IPipelineStep
        {
        }

        public interface UpdateMeta : IPipelineStep
        {
        }

        public interface TriggerEvents : IPipelineStep
        {
        }
        
        public interface WriteEvents : IPipelineStep
		{
		}
        
        public interface SendResult : IPipelineStep
        {
        }
    }
}