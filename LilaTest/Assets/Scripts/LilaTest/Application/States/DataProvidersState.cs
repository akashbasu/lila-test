using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class DataProvidersState : BaseGameState
    {
        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(new GridUiDataProvider());

            return steps;
        }
    }
    
    
}