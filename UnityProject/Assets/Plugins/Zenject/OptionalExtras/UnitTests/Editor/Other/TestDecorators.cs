using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestDecorators : ZenjectUnitTestFixture
    {
        static int CallCounter;

        public interface ISaveHandler
        {
            void Save();
        }

        public class SaveHandler : ISaveHandler
        {
            public SaveHandler()
            {
                NumInstances++;
            }

            public static int CallCount
            {
                get; set;
            }

            public static int NumInstances
            {
                get; set;
            }

            public void Save()
            {
                CallCount = CallCounter++;
            }
        }

        public class SaveDecorator1 : ISaveHandler
        {
            readonly ISaveHandler _handler;

            public SaveDecorator1(ISaveHandler handler)
            {
                _handler = handler;
                NumInstances++;
            }

            public static int NumInstances
            {
                get; set;
            }

            public static int CallCount
            {
                get; set;
            }

            public void Save()
            {
                CallCount = CallCounter++;
                _handler.Save();
            }
        }

        public class SaveDecorator2 : ISaveHandler
        {
            readonly ISaveHandler _handler;

            public SaveDecorator2(ISaveHandler handler)
            {
                _handler = handler;
            }

            public static int CallCount
            {
                get; set;
            }

            public void Save()
            {
                CallCount = CallCounter++;
                _handler.Save();
            }
        }

        public class Foo
        {
        }

        // TODO - Fix this
        //[Test]
        //public void TestContainerInheritance2()
        //{
            //Container.Bind<ISaveHandler>().To<SaveHandler>().AsSingle();
            //Container.Decorate<ISaveHandler>().With<SaveDecorator1>();

            //var subContainer = Container.CreateSubContainer();
            //subContainer.Decorate<ISaveHandler>().With<SaveDecorator2>();

            //CallCounter = 1;
            //SaveHandler.CallCount = 0;
            //SaveDecorator1.CallCount = 0;
            //SaveDecorator2.CallCount = 0;

            //subContainer.Resolve<ISaveHandler>().Save();

            //Assert.IsEqual(SaveDecorator2.CallCount, 1);
            //Assert.IsEqual(SaveDecorator1.CallCount, 2);
            //Assert.IsEqual(SaveHandler.CallCount, 3);
        //}
    }
}
