using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class DataProvidersState : BaseGameState
    {
        private GridUiDataProvider _gridUiDataProvider;

        protected override void InstallDependencies()
        {
            _gridUiDataProvider?.Dispose();
            _gridUiDataProvider = null;
            _gridUiDataProvider = new GridUiDataProvider(); 
        }

        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(_gridUiDataProvider);

            return steps;
        }
    }
}