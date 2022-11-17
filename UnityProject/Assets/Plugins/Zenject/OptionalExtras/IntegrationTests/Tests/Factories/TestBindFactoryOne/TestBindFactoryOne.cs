
using System.Collections;
using ModestTree;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject.Tests.Factories.BindFactoryOne;

namespace Zenject.Tests.Factories
{
    public class TestBindFactoryOne : ZenjectIntegrationTestFixture
    {
        GameObject FooPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestBindFactoryOne/Foo");
            }
        }

        GameObject FooSubContainerPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestBindFactoryOne/FooSubContainer");
            }
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOn()
        {
            PreInstall();
            var go = new GameObject();

            Container.BindFactory<string, Foo, Foo.Factory>().FromNewComponentOn(go);

            PostInstall();

            var factory = Container.Resolve<Foo.Factory>();

            Assert.IsNull(go.GetComponent<Foo>());
            var foo = factory.Create("asdf");
            Assert.IsEqual(foo.Value, "asdf");
            Assert.IsNotNull(go.GetComponent<Foo>());
            Assert.IsEqual(go.GetComponent<Foo>(), foo);

            var foo2 = factory.Create("zxcv");

            Assert.IsNotEqual(foo2, foo);

            var allFoos = go.GetComponents<Foo>();
            Assert.IsEqual(allFoos.Length, 2);
            Assert.IsEqual(allFoos[0], foo);
            Assert.IsEqual(allFoos[1], foo2);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewScriptableObjectResource()
        {
            PreInstall();
            Container.BindFactory<string, Bar, Bar.Factory>()
                .FromNewScriptableObjectResource("TestBindFactoryOne/Bar");

            PostInstall();

            var factory = Container.Resolve<Bar.Factory>();
            var bar = factory.Create("asdf");
            Assert.IsNotNull(bar);
            Assert.IsEqual(bar.Value, "asdf");
            Assert.IsNotEqual(bar, factory.Create("zxcv"));
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnNewGameObjectSelf()
        {
            PreInstall();
            Container.BindFactory<string, Foo, Foo.Factory>().FromNewComponentOnNewGameObject();

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnNewGameObjectConcrete()
        {
            PreInstall();
            Container.BindFactory<string, IFoo, IFooFactory>().To<Foo>().FromNewComponentOnNewGameObject();

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnSelf()
        {
            PreInstall();
            var gameObject = Container.CreateEmptyGameObject("foo");

            Container.BindFactory<string, Foo, Foo.Factory>().FromNewComponentOn(gameObject);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnConcrete()
        {
            PreInstall();
            var gameObject = Container.CreateEmptyGameObject("foo");

            Container.BindFactory<string, IFoo, IFooFactory>().To<Foo>().FromNewComponentOn(gameObject);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromComponentInNewPrefabSelf()
        {
            PreInstall();
            Container.BindFactory<string, Foo, Foo.Factory>().FromComponentInNewPrefab(FooPrefab).WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromComponentInNewPrefabConcrete()
        {
            PreInstall();
            Container.BindFactory<string, IFoo, IFooFactory>().To<Foo>()
                .FromComponentInNewPrefab(FooPrefab).WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromComponentInNewPrefabResourceSelf()
        {
            PreInstall();
            Container.BindFactory<string, Foo, Foo.Factory>().FromComponentInNewPrefabResource("TestBindFactoryOne/Foo").WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromComponentInNewPrefabResourceConcrete()
        {
            PreInstall();
            Container.BindFactory<string, IFoo, IFooFactory>().To<Foo>()
                .FromComponentInNewPrefabResource("TestBindFactoryOne/Foo").WithGameObjectName("asdf");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromSubContainerResolveByNewPrefabSelf()
        {
            PreInstall();
            Container.BindFactory<string, Foo, Foo.Factory>()
                .FromSubContainerResolve().ByNewContextPrefab<FooInstaller>(FooSubContainerPrefab);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromSubContainerResolveByNewPrefabConcrete()
        {
            PreInstall();
            Container.BindFactory<string, IFoo, IFooFactory>()
                .To<Foo>().FromSubContainerResolve().ByNewContextPrefab<FooInstaller>(FooSubContainerPrefab);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromSubContainerResolveByNewPrefabResourceSelf()
        {
            PreInstall();
            Container.BindFactory<string, Foo, Foo.Factory>()
                .FromSubContainerResolve().ByNewContextPrefabResource<FooInstaller>("TestBindFactoryOne/FooSubContainer");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromSubContainerResolveByNewPrefabResourceConcrete()
        {
            PreInstall();
            Container.BindFactory<string, IFoo, IFooFactory>()
                .To<Foo>().FromSubContainerResolve().ByNewContextPrefabResource<FooInstaller>("TestBindFactoryOne/FooSubContainer");

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }
    }
}

