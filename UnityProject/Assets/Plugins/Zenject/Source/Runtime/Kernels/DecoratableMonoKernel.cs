namespace Zenject
{
    public interface IDecoratableMonoKernel
    {
        bool ShouldInitializeOnStart();
        void Dispose();
        void LateDispose();
    }

    public class DecoratableMonoKernel : IDecoratableMonoKernel
    {
        [InjectLocal]
        public DisposableManager DisposablesManager { get; protected set; } = null;
        
        
        public virtual bool ShouldInitializeOnStart() => true;

        public void Dispose()
        {
            DisposablesManager.Dispose();
        }

        public void LateDispose()
        {
            DisposablesManager.LateDispose();
        }
    }

    public abstract class BaseMonoKernelDecorator : IDecoratableMonoKernel
    {
        [Inject] 
        protected IDecoratableMonoKernel DecoratedMonoKernel;

        public virtual bool ShouldInitializeOnStart() => DecoratedMonoKernel.ShouldInitializeOnStart();
        public virtual void Dispose() => DecoratedMonoKernel.Dispose();
        public virtual void LateDispose() => DecoratedMonoKernel.LateDispose();
    }
    
}