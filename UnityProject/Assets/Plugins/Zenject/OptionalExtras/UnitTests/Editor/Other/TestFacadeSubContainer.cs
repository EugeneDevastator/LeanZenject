using System;
using NUnit.Framework;
using Assert = ModestTree.Assert;

namespace Zenject.Tests.Other
{
    [TestFixture]
    public class TestFacadeSubContainer
    {
        static int NumInstalls;

        [Test]
        public void Test1()
        {
            NumInstalls = 0;

            DisposeTest.WasRun = false;

            var container = new DiContainer();

            container.Bind( typeof(DisposableManager))
                .ToSelf().AsSingle().CopyIntoAllSubContainers();

            // This is how you add ITickables / etc. within sub containers
            container.BindInterfacesAndSelfTo<FooKernel>()
                .FromSubContainerResolve().ByMethod(InstallFoo).AsSingle();

            var disposeManager = container.Resolve<DisposableManager>();

            Assert.That(!DisposeTest.WasRun);

            disposeManager.Dispose();


            Assert.That(DisposeTest.WasRun);
        }

        public void InstallFoo(DiContainer subContainer)
        {
            NumInstalls++;

            subContainer.Bind<FooKernel>().AsSingle();
            subContainer.Bind<IDisposable>().To<DisposeTest>().AsSingle();
        }

        public class FooKernel : Kernel
        {
        }

        public class DisposeTest : IDisposable
        {
            public static bool WasRun;

            public void Dispose()
            {
                WasRun = true;
            }
        }
    }
}


