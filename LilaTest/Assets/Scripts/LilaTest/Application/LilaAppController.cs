using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class LilaAppController : GameStateController
    {
        protected override void EnqueueOneTimeStates(Queue<IInitializable> steps)
        {
            steps.Enqueue(new BootState());
        }

        protected override void EnqueueLoopableStates(Queue<IInitializable> steps)
        {
            steps.Enqueue(new ConfigState());
            steps.Enqueue(new SystemsState());
            steps.Enqueue(new DataManagersState());
            steps.Enqueue(new DataProvidersState());
            steps.Enqueue(new ApplicationState());
        }
    }
}