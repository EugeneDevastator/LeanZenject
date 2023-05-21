using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
    public class RawContext : IDisposable
    {
        public event Action PreInstall;
        public event Action PostInstall;
        public event Action PreResolve;
        public event Action PostResolve;

        Kernel _kernel;
        DiContainer _container;

        // Need to cache this when auto run is false
        DiContainer _parentContainer;

        bool _hasInstalled;
        private List<IInstaller> _installers;

        public DiContainer Container => _container;
        
        public static RawContext CreateWithInstaller(DiContainer container, IInstaller installer)
        {
            return container.Instantiate<RawContext>(new List<object>(){new List<IInstaller>(){installer}});
        }
        
        [Inject]
        public RawContext(DiContainer parentContainer, List<IInstaller> installers)
        {
            _installers = installers;
            _parentContainer = parentContainer;
            RunInternal();
        }

        void RunInternal()
        {
            Install(_parentContainer);
            ResolveAndStart();
        }

        void Install(DiContainer parentContainer)
        {
            Assert.That(_parentContainer == null || _parentContainer == parentContainer);

            // We allow calling this explicitly instead of relying on the [Inject] event above
            // so that we can follow the two-pass construction-injection pattern in the providers
            if (_hasInstalled)
            {
                return;
            }

            _hasInstalled = true;

            Assert.IsNull(_container);
            _container = parentContainer.CreateSubContainer();

            // Do this after creating DiContainer in case it's needed by the pre install logic
            if (PreInstall != null)
            {
                PreInstall();
            }
            
            _container.IsInstalling = true;

            try
            {
                InstallBindings();
            }
            finally
            {
                _container.IsInstalling = false;
            }

            if (PostInstall != null)
            {
                PostInstall();
            }
        }

        void ResolveAndStart()
        {
            if (PreResolve != null)
            {
                PreResolve();
            }

            _container.ResolveRoots();

            if (PostResolve != null)
            {
                PostResolve();
            }
            
            _kernel = _container.Resolve<Kernel>();
        }

        void InstallBindings()
        {
            
            _container.Bind<RawContext>().FromInstance(this);
            _container.Bind<Kernel>().FromNew().AsSingle()
                .NonLazy();
            
            InstallInstallers();
        }

        private void InstallInstallers()
        {
            foreach (var installer in _installers)
            {
                Assert.IsNotNull(installer,
                    "Found null installer in '{0}'", GetType());

                Container.Inject(installer);

#if ZEN_INTERNAL_PROFILING
                using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
                {
                    installer.InstallBindings();
                }
            }
        }
        
        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}