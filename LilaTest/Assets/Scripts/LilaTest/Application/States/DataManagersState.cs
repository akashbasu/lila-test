using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class DataManagersState : BaseGameState
    {
        [Dependency] private readonly IGridDataManager _gridDataManager;
        
        protected override void InstallDependencies()
        {
            DependencyRegistry.RegisterInterface<IGridDataManager, GridDataManager>();
        }

        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(_gridDataManager);

            return steps;
        }
    }
}