using System.Collections.Generic;
using Core.GameStateMachine;
using Core.IoC;

namespace LilaTest
{
    internal class ConfigState : BaseGameState
    {
        [Dependency] private readonly IConfigResourceProvider _configResourceProvider;
        [Dependency] private readonly IDeterministicRandomProvider _deterministicRandomProvider;
        
        protected override void InstallDependencies()
        {
            DependencyRegistry.RegisterInterface<IConfigResourceProvider, ConfigResourceProvider>();
            DependencyRegistry.RegisterInterface<IDeterministicRandomProvider, DeterministicRandomProvider>();
        }

        protected override Queue<IInitializable> GetSteps()
        {
            var steps = new Queue<IInitializable>();
            
            steps.Enqueue(_configResourceProvider);
            steps.Enqueue(_deterministicRandomProvider);

            return steps;
        }
    }
}