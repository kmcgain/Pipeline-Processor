using System;
using System.Collections;
using System.Dynamic;
using System.Runtime.Serialization;

namespace Pipeline.Events
{
    using System.Collections.Generic;

    public abstract class Event : IEvent
    {
        public enum EventTypes
        {

        };

        public static readonly Dictionary<EventTypes, string> PossibleEventDescriptions =
            new Dictionary<EventTypes, string>
                {
        
                };
    }
}