using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject.Tests.Factories.PrefabFactory;

namespace Zenject.Tests.Factories
{
    public class TestPrefabFactory : ZenjectIntegrationTestFixture
    {
        string FooPrefabResourcePath
        {
            get { return "TestPrefabFactory/Foo"; }
        }

        GameObject FooPrefab
        {
            get { return FixtureUtil.GetPrefab(FooPrefabResourcePath); }
        }

        string Foo2PrefabResourcePath
        {
            get { return "TestPrefabFactory/Foo2"; }
        }

        GameObject Foo2Prefab
        {
            get { return FixtureUtil.GetPrefab(Foo2PrefabResourcePath); }
        }

        [UnityTest]
        public IEnumerator Test1()
        {
            PreInstall();

            Container.BindFactory<Object, Foo, Foo.Factory>().FromFactory<PrefabFactory<Foo>>();

            PostInstall();
            yield break;
        }
    }
}
