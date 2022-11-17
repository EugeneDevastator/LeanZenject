using System;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestDecoratorValidation
    {
        public interface ISaveHandler
        {
            void Save();
        }

        public class SaveHandler : ISaveHandler
        {
            public void Save()
            {
            }
        }

        public class SaveDecorator1 : ISaveHandler
        {
            readonly ISaveHandler _handler;

            public SaveDecorator1(ISaveHandler handler)
            {
                _handler = handler;
            }

            public void Save()
            {
                _handler.Save();
            }
        }

        DiContainer Container
        {
            get; set;
        }

        [SetUp]
        public void Setup()
        {
            Container = new DiContainer(true);
            Container.Settings = new ZenjectSettings(ValidationErrorResponses.Throw);
        }

        public class Foo
        {
            public Foo(ISaveHandler saveHandler)
            {
            }
        }
    }
}

