
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject.Tests.Factories.BindFactoryFive;

namespace Zenject.Tests.Factories
{
    public class TestBindFactoryFive : ZenjectIntegrationTestFixture
    {
        GameObject FooPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestBindFactoryFive/Foo");
            }
        }

        GameObject FooSubContainerPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestBindFactoryFive/FooSubContainer");
            }
        }

        [UnityTest]
        public IEnumerator TestToGameObjectSelf()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>().FromNewComponentOnNewGameObject();

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToGameObjectConcrete()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>().To<Foo>().FromNewComponentOnNewGameObject();

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToMonoBehaviourSelf()
        {
            PreInstall();
            var gameObject = Container.CreateEmptyGameObject("foo");

            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>().FromNewComponentOn(gameObject);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToMonoBehaviourConcrete()
        {
            PreInstall();
            var gameObject = Container.CreateEmptyGameObject("foo");

            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>().To<Foo>().FromNewComponentOn(gameObject);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToPrefabSelf()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>().FromComponentInNewPrefab(FooPrefab).WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToPrefabConcrete()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>().To<Foo>().FromComponentInNewPrefab(FooPrefab).WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToPrefabResourceSelf()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>().FromComponentInNewPrefabResource("TestBindFactoryFive/Foo").WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToPrefabResourceConcrete()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>()
                .To<Foo>().FromComponentInNewPrefabResource("TestBindFactoryFive/Foo").WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToSubContainerPrefabSelf()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>()
                .FromSubContainerResolve().ByNewContextPrefab<FooInstaller>(FooSubContainerPrefab);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToSubContainerPrefabConcrete()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>()
                .To<Foo>().FromSubContainerResolve().ByNewContextPrefab<FooInstaller>(FooSubContainerPrefab);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToSubContainerPrefabResourceSelf()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, Foo, Foo.Factory>().FromSubContainerResolve().ByNewContextPrefabResource<FooInstaller>("TestBindFactoryFive/FooSubContainer");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestToSubContainerPrefabResourceConcrete()
        {
            PreInstall();
            Container.BindFactory<double, int, float, string, char, IFoo, IFooFactory>()
                .To<Foo>().FromSubContainerResolve().ByNewContextPrefabResource<FooInstaller>("TestBindFactoryFive/FooSubContainer");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }
    }
}

