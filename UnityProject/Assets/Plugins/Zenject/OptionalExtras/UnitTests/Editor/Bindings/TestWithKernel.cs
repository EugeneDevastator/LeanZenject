using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Bindings
{
    [TestFixture]
    public class TestWithKernel : ZenjectUnitTestFixture
    {
        static int GlobalInitializeCount;

        public class Foo : IInitializable
        {
            public bool WasInitialized
            {
                get; private set;
            }

            public int InitializeCount
            {
                get; private set;
            }

            public void Initialize()
            {
                InitializeCount = ++GlobalInitializeCount;
                WasInitialized = true;
            }
        }

        public class FooFacade
        {
            [Inject]
            public Foo Foo
            {
                get; private set;
            }
        }

        public class FooInstaller : Installer<FooInstaller>
        {
            public override void InstallBindings()
            {
                InstallFoo(Container);
            }
        }

        static void InstallFoo(DiContainer subContainer)
        {
            subContainer.Bind<FooFacade>().AsSingle();
            subContainer.BindInterfacesAndSelfTo<Foo>().AsSingle();
        }

        public class FooKernel : Kernel
        {
        }

        public class Bar : IInitializable
        {
            public int InitializeCount
            {
                get; private set;
            }

            public void Initialize()
            {
                InitializeCount = ++GlobalInitializeCount;
            }
        }
    }
}


