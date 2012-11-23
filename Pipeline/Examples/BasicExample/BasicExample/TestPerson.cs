using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicExample.Entity.Person;
using NSubstitute;
using NUnit.Framework;
using Pipeline.Pipe;
using StructureMap;
using Pipeline.Model.Extensions;
using Pipeline.XmlTools;
using Pipeline.Model;

namespace BasicExample
{
    [TestFixture]
    public class TestPerson        
    {
        private IPipelineProcessor processor;

        [TestFixtureSetUp]
        public void SetupOnce()
        {
            ObjectFactory.Initialize((cfg) => cfg.AddRegistry(new ProcessorRegistry()));

            var eventLogger = Substitute.For<IEventLogger>();
            processor = new PipelineProcessor(ObjectFactory.Container, eventLogger);
        }

        [Test]
        public void RegisterPerson()
        {
            var response = processor.ProcessAction(new PersonRegisteredEvent(new Xml("<person />"), new EntityIdentifier("p1")));
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void RegisterPersonIncorrectly()
        {
            var response = processor.ProcessAction(new PersonRegisteredEvent(new Xml("<animal />"), new EntityIdentifier("p1")));
            Assert.AreEqual(403, response.StatusCode);
        }
    }
}
