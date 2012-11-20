namespace Pipeline.Pipe
{
    using System;
    using System.Diagnostics;
    using Results;

    public class StepCallInformation
    {
        private readonly Stopwatch watch;
        public Outcome Result { get; private set; }
        public Type StepType { get; private set; }
        public TimeSpan ExecutionTime { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public StepCallInformation(Type stepName)
        {
            StepType = stepName;
            watch = new Stopwatch();
        }

        public void StartStep()
        {
            TimeStamp = DateTime.UtcNow;
            watch.Start();
        }

        public void StopStep(Outcome result)
        {
            Result = result;
            watch.Stop();
            ExecutionTime = watch.Elapsed;
        }
    }
}