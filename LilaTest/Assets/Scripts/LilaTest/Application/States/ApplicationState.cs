using System.Collections.Generic;
using Core;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class ApplicationState : BaseGameState
    {
        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(new WaitForApplicationEnd());

            return steps;
        }
    }
    
    internal class WaitForApplicationEnd : WaitForEventState
    {
        protected override string EndEvent => ApplicationEvents.Application.Stop;
    }
}