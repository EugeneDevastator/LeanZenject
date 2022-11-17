using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject.Tests.Factories.BindFactoryOne;

namespace Zenject.Tests.Factories
{
    public class TestBindFactoryOneWithArguments : ZenjectIntegrationTestFixture
    {
        private const string ArgumentValue = "asdf";

        GameObject FooPrefab
        {
            get
            {
                return FixtureUtil.GetPrefab("TestBindFactoryOne/Foo");
            }
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnNewGameObjectSelf()
        {
            PreInstall();
            Container.BindIFactory<Foo>()
                .FromNewComponentOnNewGameObject()
                .WithArguments(ArgumentValue);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromNewComponentOnNewGameObjectConcrete()
        {
            PreInstall();
            Container.BindIFactory<IFoo>()
                .To<Foo>()
                .FromNewComponentOnNewGameObject()
                .WithArguments(ArgumentValue);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestFromComponentInNewPrefabSelf()
        {
            PreInstall();
            Container.BindIFactory<Foo>()
                .FromComponentInNewPrefab(FooPrefab)
                .WithGameObjectName("asdf")
                .WithArguments(ArgumentValue);

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
            Container.BindIFactory<IFoo>()
                .To<Foo>()
                .FromComponentInNewPrefab(FooPrefab)
                .WithGameObjectName("asdf")
                .WithArguments(ArgumentValue);

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
            Container.BindIFactory<Foo>()
                .FromComponentInNewPrefabResource("TestBindFactoryOne/Foo")
                .WithGameObjectName("asdf")
                .WithArguments(ArgumentValue);

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
            Container.BindIFactory<IFoo>().To<Foo>()
                .FromComponentInNewPrefabResource("TestBindFactoryOne/Foo")
                .WithGameObjectName("asdf")
                .WithArguments(ArgumentValue);

            PostInstall();

            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertNumGameObjectsWithName("asdf", 1);
            yield break;
        }

        // Note that unlike the TestBindFactory tests, WithArguments still doesn't work nicely with subcontainers...
    }
}

