using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;
using Core.UI.Framework;

namespace LilaTest
{
    internal class SystemsState : BaseGameState
    {
        [Dependency] private readonly IUiDataRegistry _uiDataRegistry;
        
        protected override void InstallDependencies()
        {
            DependencyRegistry.RegisterInterface<IUiDataRegistry, UiDataRegistry>();
        }

        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(_uiDataRegistry);

            return steps;
        }
    }
}