using Core.IoC;

namespace Core.Command
{
    internal abstract class BaseUiCommand : ICommand
    {
        protected BaseUiCommand()
        {
            Injector.ResolveDependencies(this);
        }

        public abstract void Execute();
    }
}