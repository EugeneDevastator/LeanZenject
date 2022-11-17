using System;
using System.Diagnostics;

namespace Zenject
{
    [DebuggerStepThrough]
    public class Kernel : IDisposable, ILateDisposable
    {
        [InjectLocal]
        DisposableManager _disposablesManager = null;

        public virtual void Dispose()
        {
            _disposablesManager.Dispose();
        }

        public virtual void LateDispose()
        {
            _disposablesManager.LateDispose();
        }
    }
}
